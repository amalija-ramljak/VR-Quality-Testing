using System;
using System.Collections.Generic;
using UnityEngine;
using VRQualityTesting.Scripts.Core;
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
        // private CustomLogger logger;
        void Start()
        {
            // logger = new CustomLogger(@"C:\Users\tyco_\Documents\faks\VR-Quality-Testing\Debug.log");
            spawnGoal();
            spawnNewPlacement();
        }

        private void spawnGoal()
        {
            this.transform.rotation = Quaternion.identity;
            goal_obj = Instantiate(goalPrefab, new Vector3(goalDistance, goalHeight, 0f), Quaternion.identity, this.transform);
            goal_obj.transform.localScale = new Vector3(goalSize, 0.25f, goalSize);
            this.transform.Rotate(new Vector3(0f, goalRotationOffset, 0f));
            Physics.SyncTransforms();
        }

        private void spawnNewPlacement()
        {
            objectParent.rotation = Quaternion.identity;
            spawnObject();
            spawnObstacles();
            objectParent.Rotate(new Vector3(0f, UnityEngine.Random.Range(-objectMaxRotationOffset, objectMaxRotationOffset), 0f));
            Physics.SyncTransforms();
        }

        private void spawnObject()
        {
            Vector3 obj_spawn_position = new Vector3(0, 0, 0);
            Shape shapeType = pickShape();
            GameObject shape = getShapePrefabFromEnum(shapeType);
            var randScale = UnityEngine.Random.Range(objectMinSize, objectMaxSize);

            proxy_obj = Instantiate(shape, obj_spawn_position, Quaternion.identity, objectParent);
            proxy_obj.transform.localScale *= randScale;
            float z, y;
            do
            {
                // distance
                z = UnityEngine.Random.Range(objectMinDistance, objectMaxDistance);
                y = UnityEngine.Random.Range(objectMinHeight, objectMaxHeight);

                // z distance, y height, x will be changed by rotation
                obj_spawn_position = new Vector3(0f, y, z);
                proxy_obj.transform.position = obj_spawn_position;

                Physics.SyncTransforms();
            } while (checkIntersections(proxy_obj)
                    || Vector3.Distance(obj_spawn_position, objectParent.position) > objectMaxDistance);

            proxy_obj.tag = "Target";
            enableRigidbody(proxy_obj);

            objects.Add(new PAPObject(obj_spawn_position, randScale, shapeType));
        }

        private void spawnObstacles()
        {
            var clutter = new List<PAPObstacle>();
            GameObject proxy = null;
            Vector3 position = new Vector3(0, 0, 0);
            float randObstacleSize;

            int attempts = 0;
            int randObstacleCount = UnityEngine.Random.Range(obstacleMinCount, obstacleMaxCount + 1);
            for (int i = 0; i < randObstacleCount; i++)
            {
                randObstacleSize = UnityEngine.Random.Range(obstacleMinSize, obstacleMaxSize);
                proxy = Instantiate(obstaclePrefab, position, Quaternion.identity, objectParent);
                proxy.transform.localScale *= randObstacleSize;
                do
                {
                    position = proxy_obj.transform.position + UnityEngine.Random.insideUnitSphere * UnityEngine.Random.Range(obstacleMinDistance, obstacleMaxDistance);

                    proxy.transform.position = position;
                    Physics.SyncTransforms();
                    attempts++;
                } while (checkIntersections(proxy, true, true, true) && attempts < 10);

                ProxyList.Add(proxy);

                clutter.Add(new PAPObstacle(position, position, randObstacleSize));
            }

            foreach (var clutterElement in ProxyList)
            {
                enableRigidbody(clutterElement);
            }

            objects[objects.Count - 1].setClutter(clutter);
        }

        public void handlePlacement()
        {
            objects[objects.Count - 1].DeathTimestamp = DateTime.Now;

            var newClutter = new List<PAPObstacle>();
            var oldClutter = objects[objects.Count - 1].clutter;
            for (int i = 0; i < ProxyList.Count; i++)
            {
                newClutter.Add(new PAPObstacle(oldClutter[i].initialCoords, ProxyList[i].transform.position, oldClutter[i].size));
                Destroy(ProxyList[i]);
            }
            objects[objects.Count - 1].setClutter(newClutter);
            objects[objects.Count - 1].setPlaced(true);

            ProxyList.Clear();
            Destroy(proxy_obj);

            spawnNewPlacement();
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

        private Bounds getBounds(GameObject obj)
        {
            return getCollider(obj).bounds;
        }

        private Collider getCollider(GameObject obj)
        {
            return obj.GetComponent<Collider>();
        }


        private void enableRigidbody(GameObject obj)
        {
            obj.GetComponent<Rigidbody>().isKinematic = false;
        }

        public void PublishReport() => SessionPublisher.Publish(new Session(objects), ".txt", ".txt");
    }
}