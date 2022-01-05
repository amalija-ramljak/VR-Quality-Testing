using UnityEngine;
using Random = UnityEngine.Random;

namespace VRQualityTesting.Scripts.PickAndPlace
{
    public class ObjectSpawnerDeprecated : MonoBehaviour
    {
        public float ObjectMinDistance { get; set; }
        public float ObjectMaxDistance { get; set; }
        public float ObjectMaxRotationOffset { get; set; }
        public float ObjectMinSize { get; set; }
        public float ObjectMaxSize { get; set; }
        public float ObjectMinAngleHeight { get; set; }
        public float ObjectMaxAngleHeight { get; set; }

        public float GoalDistance { get; set; }
        public float GoalRotationOffset { get; set; }
        public float GoalSize { get; set; }
        public float GoalHeight { get; set; }

        //public float GoalMinDistance { get; set; }
        //public float GoalMaxDistance { get; set; }
        //public float GoalMaxRotationOffset { get; set; }
        //public float GoalMinSize { get; set; }
        //public float GoalMaxSize { get; set; }
        //public float GoalMinAngleHeight { get; set; }
        //public float GoalMaxAngleHeight { get; set; }

        public float ObstacleMinDistance { get; set; }
        public float ObstacleMaxDistance { get; set; }
        public int ObstacleMinCount { get; set; }
        public int ObstacleMaxCount { get; set; }
        public float ObstacleMinSize { get; set; }
        public float ObstacleMaxSize { get; set; }

        public bool UseObjectTypeSquare { get; set; }
        public bool UseObjectTypeCylinder { get; set; }
        public bool UseObjectTypeSphere { get; set; }

        [SerializeField] private GameObject objectSquarePrefab;
        [SerializeField] private GameObject objectCylinderPrefab;
        [SerializeField] private GameObject objectSpherePrefab;
        [SerializeField] private GameObject obstaclePrefab;
        [SerializeField] private GameObject goalPrefab;
        private float _currentDuration;

        private bool flag = false;

        private void Start()
        {
            SpawnObject();
            SpawnGoal();
        }

        public void OnTargetHit()
        {
            flag = false;
        }

        private void Update()
        {
            if (flag == false)
            {
                SpawnObject();
            }
        }

        private void SpawnObject()
        {
            flag = true;

            var objectPrefab = Instantiate(objectSquarePrefab, GetRandomObjectPosition(), Quaternion.identity);
            objectPrefab.transform.localScale = GetRandomObjectSize();
            objectPrefab.transform.LookAt(2 * objectPrefab.transform.position);
        }


        private void SpawnGoal()
        {
            var goal = Instantiate(goalPrefab, GetGoalPosition(), Quaternion.identity);
            goal.transform.localScale = GetGoalSize();
        }

        private Vector3 GetRandomObjectPosition()
        {
            var radius = Random.Range(ObjectMinDistance, ObjectMaxDistance);

            var angleOffset = ObjectMaxRotationOffset * Mathf.Deg2Rad;
            var angle = Random.Range(-angleOffset, +angleOffset);

            var x = radius * Mathf.Cos(angle);
            //var y = Random.Range(ObjectMinAngleHeight, ObjectMaxAngleHeight);
            var y = Random.Range(0, 5);
            var z = radius * Mathf.Sin(angle);

            return new Vector3(x, y, z);
        }

        private Vector3 GetRandomObjectSize()
        {
            var size = Random.Range(ObjectMinSize, ObjectMaxSize);
            return new Vector3(size, size, size);
        }

        //private Vector3 GetRandomGoalPosition()
        //{
        //    var radius = Random.Range(GoalMinDistance, GoalMaxDistance);

        //    var angleOffset = GoalMaxRotationOffset * Mathf.Deg2Rad;
        //    var angle = Random.Range(-angleOffset, +angleOffset);

        //    var x = radius * Mathf.Cos(angle);
        //    //var y = Random.Range(GoalMinAngleHeight, GoalMaxAngleHeight);
        //    var y = Random.Range(2, 5);
        //    var z = radius * Mathf.Sin(angle);

        //    return new Vector3(x, y, z);
        //}

        //private Vector3 GetRandomGoalSize()
        //{
        //    var size = Random.Range(GoalMinSize, GoalMaxSize);
        //    return new Vector3(size, 0.5f, size);
        //}

        private Vector3 GetGoalPosition()
        {
            var angleOffset = GoalRotationOffset * Mathf.Deg2Rad;

            var x = GoalDistance * Mathf.Cos(angleOffset);
            var y = GoalHeight;
            var z = GoalDistance * Mathf.Sin(angleOffset);

            return new Vector3(x, y, z);
        }

        private Vector3 GetGoalSize()
        {
            return new Vector3(GoalSize, 1, GoalSize);
        }
    }
}
