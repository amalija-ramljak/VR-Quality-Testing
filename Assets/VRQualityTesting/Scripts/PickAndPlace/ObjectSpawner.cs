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
        private GameObject proxy_obj;

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
            Instantiate(goalPrefab, new Vector3(goalDistance, goalHeight, 0f), Quaternion.identity, this.transform);
            this.transform.Rotate(new Vector3(0f, UnityEngine.Random.Range(-goalRotationOffset, goalRotationOffset), 0f));
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
            // distance
            float x = UnityEngine.Random.Range(objectMinDistance, objectMaxDistance);
            float y = UnityEngine.Random.Range(objectMinHeight, objectMaxHeight);

            // x distance, y height, z will be changed by rotation
            Vector3 obj_spawn_position = new Vector3(x, y, 0f);

            var shape = pickShape();

            objects.Add(new PAPObject(obj_spawn_position, shape));

            proxy_obj = Instantiate(getShapePrefabFromEnum(shape), obj_spawn_position, Quaternion.identity, objectParent);
        }

        private void spawnObstacles()
        {
            var clutter = new List<PAPObstacle>();
            var obj_Collider = proxy_obj.GetComponent<Collider>();
            Collider obstacle_Collider;
            GameObject proxy = null;
            Vector3 position;
            float randObstacleSize;

            bool newLoop;
            int randObstacleCount = UnityEngine.Random.Range(obstacleMinCount, obstacleMaxCount + 1);
            for (int i = 0; i < randObstacleCount; i++)
            {
                newLoop = true;
                do
                {
                    if (!newLoop) Destroy(proxy);
                    newLoop = false;
                    do
                    {
                        position = proxy_obj.transform.position + UnityEngine.Random.insideUnitSphere * UnityEngine.Random.Range(obstacleMinDistance, obstacleMaxDistance);
                    } while (Vector3.Distance(position, proxy_obj.transform.position) < obstacleMinDistance);

                    proxy = Instantiate(obstaclePrefab, position, Quaternion.identity, objectParent);
                    obstacle_Collider = proxy.GetComponent<Collider>();
                } while (obstacle_Collider.bounds.Intersects(obj_Collider.bounds));

                randObstacleSize = UnityEngine.Random.Range(obstacleMinSize, obstacleMaxSize);
                proxy.transform.localScale = new Vector3(randObstacleSize, randObstacleSize, randObstacleSize);

                obstacle_pocetneKordinate.Add(position);
                ProxyList.Add(proxy);

                clutter.Add(new PAPObstacle(obstacle_pocetneKordinate[i], obstacle_pocetneKordinate[i]));
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

            for (int element = 0; element < ProxyList.Count; element++)
            {
                Destroy(ProxyList[element]);
            }

            Destroy(proxy_obj);
            obstacle_ukupnaUdaljenostOdPocPozicije(obstacle_pocetneKordinate, obstacle_trenutneKordinate);
            obstacle_kordinatnaUdaljenostOdPocPozicije(obstacle_pocetneKordinate, obstacle_trenutneKordinate);

            var clutter = new List<PAPObstacle>();
            for (int i = 0; i < obstacle_pocetneKordinate.Count; i++)
            {
                clutter.Add(new PAPObstacle(obstacle_pocetneKordinate[i], obstacle_trenutneKordinate[i]));
            }
            objects[objects.Count - 1].setClutter(clutter);
            objects[objects.Count - 1].setPlaced(true);

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

        public static enum Shape
        {
            square,
            sphere,
            cylinder,
        }

        private ObjectSpawner.Shape pickShape()
        {
            var options = new List<Shape> { };
            if (useObjectTypeCylinder) options.Add(ObjectSpawner.Shape.cylinder);
            if (useObjectTypeSquare) options.Add(ObjectSpawner.Shape.square);
            if (useObjectTypeSphere) options.Add(ObjectSpawner.Shape.sphere);

            if (options.Count > 1) return options[UnityEngine.Random.Range(0, options.Count)];
            else if (options.Count) return options[0];
            else return Shape.square;
        }

        private GameObject getShapePrefabFromEnum(ObjectSpawner.Shape shape)
        {
            switch (shape)
            {
                case Shape.square: return squarePrefab;
                case Shape.cylinder: return cylinderPrefab;
                case Shape.sphere: return spherePrefab;
                default: return squarePrefab;
            }
        }

        public void PublishReport() => SessionPublisher.Publish(new Session(objects), ".txt", ".txt");
    }
}