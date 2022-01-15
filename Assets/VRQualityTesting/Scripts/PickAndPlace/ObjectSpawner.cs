using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRQualityTesting.Scripts.Core;
using VRQualityTesting.Scripts.PickAndPlaceMenu;
using VRQualityTesting.Scripts.Utility;

namespace VRQualityTesting.Scripts.PickAndPlace
{
    public class ObjectSpawner : MonoBehaviour
    {
        #region Inspector Variables
        [SerializeField] private GameObject obj;
        [SerializeField] private GameObject squarePrefab;
        [SerializeField] private GameObject cylinderPrefab;
        [SerializeField] private GameObject spherePrefab;
        [SerializeField] private GameObject obstaclePrefab;
        [SerializeField] private GameObject goalPrefab;
        [SerializeField] Transform objectParent;
        #endregion
        private List<GameObject> ProxyList = new List<GameObject>();
        private GameObject goal_obj = null;
        private GameObject proxy_obj = null;

        private List<Vector3> obstacle_pocetneKordinate = new List<Vector3>();
        private List<Vector3> obstacle_trenutneKordinate = new List<Vector3>();
        int broj_Dotaknutih_Kocaka = 0;

        private List<PAPObject> objects = new List<PAPObject>();

        #region Settings Variables
        [HideInInspector] public float objectMinDistance { get; set; }
        [HideInInspector] public float objectMaxDistance { get; set; }
        [HideInInspector] public float objectMinHeight { get; set; }
        [HideInInspector] public float objectMaxHeight { get; set; }
        [HideInInspector] public float objectMaxRotationOffset { get; set; }
        [HideInInspector] public float objectMinSize { get; set; }
        [HideInInspector] public float objectMaxSize { get; set; }
        [HideInInspector] public bool useObjectTypeSquare { get; set; }
        [HideInInspector] public bool useObjectTypeCylinder { get; set; }
        [HideInInspector] public bool useObjectTypeSphere { get; set; }
        [HideInInspector] public float obstacleMinDistance { get; set; }
        [HideInInspector] public float obstacleMaxDistance { get; set; }
        [HideInInspector] public float obstacleMinSize { get; set; }
        [HideInInspector] public float obstacleMaxSize { get; set; }
        [HideInInspector] public int obstacleMinCount { get; set; }
        [HideInInspector] public int obstacleMaxCount { get; set; }
        [HideInInspector] public float goalDistance { get; set; }
        [HideInInspector] public float goalRotationOffset { get; set; }
        [HideInInspector] public float goalSize { get; set; }
        [HideInInspector] public float goalHeight { get; set; }
        #endregion

        void Start()
        {
            spawnGoal();
            spawnNewPlacement();
        }

        private void spawnGoal()
        {
            this.transform.rotation = Quaternion.identity;
            goal_obj = Instantiate(goalPrefab, new Vector3(goalDistance, goalHeight, 0f), Quaternion.identity, this.transform);
            goal_obj.transform.localScale = new Vector3(goalSize, 0.25f, goalSize);
            this.transform.Rotate(new Vector3(0f, goalRotationOffset, 0f));
        }

        private void spawnNewPlacement()
        {
            objectParent.rotation = Quaternion.identity;
            spawnObject();
            spawnObstacles();
            objectParent.Rotate(new Vector3(0f, UnityEngine.Random.Range(-objectMaxRotationOffset, objectMaxRotationOffset), 0f));
        }

        private void spawnObject()
        {
            Vector3 obj_spawn_position;
            Shape shapeType = pickShape();
            GameObject shape = getShapePrefabFromEnum(shapeType);
            var randScale = UnityEngine.Random.Range(objectMinSize, objectMaxSize);
            float z, y;
            do
            {
                if (proxy_obj != null) Destroy(proxy_obj);
                // distance
                z = UnityEngine.Random.Range(objectMinDistance, objectMaxDistance);
                y = UnityEngine.Random.Range(objectMinHeight, objectMaxHeight);

                // z distance, y height, x will be changed by rotation
                obj_spawn_position = new Vector3(0f, y, z);

                proxy_obj = Instantiate(shape, obj_spawn_position, Quaternion.identity, objectParent);
            proxy_obj.transform.localScale = new Vector3(randScale, randScale, randScale);
            } while (checkIntersections(proxy_obj)
                    || Vector3.Distance(obj_spawn_position, this.transform.position) > objectMaxDistance);

            objects.Add(new PAPObject(obj_spawn_position, randScale, shapeType));
        }

        private void spawnObstacles()
        {
            var clutter = new List<PAPObstacle>();
            GameObject proxy = null;
            Vector3 position;
            float randObstacleSize;

            bool newLoop;
            int randObstacleCount = UnityEngine.Random.Range(obstacleMinCount, obstacleMaxCount + 1);
            for (int i = 0; i < randObstacleCount; i++)
            {
                randObstacleSize = UnityEngine.Random.Range(obstacleMinSize, obstacleMaxSize);
                newLoop = true;
                do
                {
                    if (!newLoop) Destroy(proxy);
                    newLoop = false;
                    position = proxy_obj.transform.position + UnityEngine.Random.insideUnitSphere * UnityEngine.Random.Range(obstacleMinDistance, obstacleMaxDistance);

                    proxy = Instantiate(obstaclePrefab, position, Quaternion.identity, objectParent);
                    proxy.transform.localScale = new Vector3(randObstacleSize, randObstacleSize, randObstacleSize);
                } while (checkIntersections(proxy, true, true, true));


                obstacle_pocetneKordinate.Add(position);
                ProxyList.Add(proxy);

                clutter.Add(new PAPObstacle(obstacle_pocetneKordinate[i], obstacle_pocetneKordinate[i], randObstacleSize));
            }

            objects[objects.Count - 1].setClutter(clutter);
        }

        public void handlePlacement()
        {
            objects[objects.Count - 1].DeathTimestamp = DateTime.Now;

            foreach (Transform t in objectParent.GetComponentsInChildren(typeof(Transform)).Skip(1))
            {
                obstacle_trenutneKordinate.Add(t.position);
            }

            var clutter = new List<PAPObstacle>();
            var oldClutter = objects[objects.Count - 1].clutter;
            for (int i = 0; i < ProxyList.Count; i++)
            {
                Destroy(ProxyList[i]);
                clutter.Add(new PAPObstacle(obstacle_pocetneKordinate[i], obstacle_trenutneKordinate[i], oldClutter[i].size));
            }
            objects[objects.Count - 1].setClutter(clutter);
            objects[objects.Count - 1].setPlaced(true);

            Destroy(proxy_obj);
            obstacle_ukupnaUdaljenostOdPocPozicije(obstacle_pocetneKordinate, obstacle_trenutneKordinate);
            obstacle_kordinatnaUdaljenostOdPocPozicije(obstacle_pocetneKordinate, obstacle_trenutneKordinate);

            spawnNewPlacement();
        }

        private void obstacle_ukupnaUdaljenostOdPocPozicije(List<Vector3> poc_kord, List<Vector3> tren_kord)
        {
            broj_Dotaknutih_Kocaka = 0;
            for (int i = 0; i < poc_kord.Count; i++)
            {
                float dist = Vector3.Distance(poc_kord[i], tren_kord[i]);
                //Debug.Log("OBS UDALJENOST" + dist);
                if (dist != 0) { broj_Dotaknutih_Kocaka++; }
            }
        }
        private void obstacle_kordinatnaUdaljenostOdPocPozicije(List<Vector3> poc_kord, List<Vector3> tren_kord)
        {
            broj_Dotaknutih_Kocaka = 0;
            for (int i = 0; i < poc_kord.Count; i++)
            {
                float x = poc_kord[i].x - tren_kord[i].x;
                //Debug.Log("XXX" + x);
                float y = poc_kord[i].y - tren_kord[i].y;
                //Debug.Log("YYY" + y);
                float z = poc_kord[i].z - tren_kord[i].z;
                //Debug.Log("ZZZ" + z);
                if (x != 0 || y != 0 || z != 0) { broj_Dotaknutih_Kocaka++; }
            }
        }

        public enum Shape
        {
            square,
            sphere,
            cylinder,
        }

        private Shape pickShape()
        {
            var options = new List<Shape> { };
            if (useObjectTypeCylinder) options.Add(Shape.cylinder);
            if (useObjectTypeSquare) options.Add(Shape.square);
            if (useObjectTypeSphere) options.Add(Shape.sphere);

            if (options.Count > 1) return options[UnityEngine.Random.Range(0, options.Count)];
            else if (options.Count == 1) return options[0];
            else return Shape.square;
        }

        private GameObject getShapePrefabFromEnum(Shape shape)
        {
            switch (shape)
            {
                case Shape.square: return squarePrefab;
                case Shape.cylinder: return cylinderPrefab;
                case Shape.sphere: return spherePrefab;
                default: return squarePrefab;
            }
        }

        private bool checkIntersections(GameObject newObject, bool checkGoal = true, bool checkObject = false, bool checkObstacles = false)
        {
            bool hasIntersection = false;
            var objectBounds = getBounds(newObject);

            if (checkGoal)
            {
                hasIntersection |= objectBounds.Intersects(getBounds(goal_obj));
            }

            if (!hasIntersection && checkObject)
            {
                hasIntersection |= objectBounds.Intersects(getBounds(proxy_obj));
            }

            if (!hasIntersection && checkObstacles)
            {
                foreach (GameObject clutterElement in ProxyList)
                {
                    hasIntersection |= objectBounds.Intersects(getBounds(clutterElement));
                    if (hasIntersection) break;
                }
            }

            return hasIntersection;
        }

        private Collider getCollider(GameObject obj)
        {
            return obj.GetComponent<Collider>();
        }

        private Bounds getBounds(GameObject obj)
        {
            return getCollider(obj).bounds;
        }

        public void PublishReport() => SessionPublisher.Publish(new Session(objects), ".txt", ".txt");
    }
}