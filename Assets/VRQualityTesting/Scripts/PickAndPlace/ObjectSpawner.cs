using UnityEngine;
using Random = UnityEngine.Random;

namespace VRQualityTesting.Scripts.PickAndPlace
{
    public class ObjectSpawner : MonoBehaviour
    {
        public float ObjectMinDistance { get; set; }
        public float ObjectMaxDistance { get; set; }
        public float ObjectMaxRotationOffset { get; set; }
        public float ObjectMinSize { get; set; }
        public float ObjectMaxSize { get; set; }
        public float GoalMinDistance { get; set; }
        public float GoalMaxDistance { get; set; }
        public float GoalMaxRotationOffset { get; set; }
        public float GoalMinSize { get; set; }
        public float GoalMaxSize { get; set; }
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

        private void Start() => SpawnObjects();

        public void OnTargetHit()
        {
            // if (DurationBetweenSpawns > 0) return;
            // _targetsHitSinceLastSpawn++;

            // if (_targetsHitSinceLastSpawn == SpawnCount)
            // {
            //     SpawnTargets();
            //     _targetsHitSinceLastSpawn = 0;
            // }
        }

        private void Update()
        {
            // if (DurationBetweenSpawns <= 0) return;
            // _currentDuration += Time.deltaTime;

            // if (_currentDuration >= DurationBetweenSpawns)
            // {
            //     SpawnTargets();
            //     _currentDuration = 0;
            // }
        }

        private void SpawnObjects()
        {
            // for (var i = 0; i < SpawnCount; i++)
            // {
            //     var target = Instantiate(targetPrefab, GetRandomTargetPosition(), Quaternion.identity);
            //     target.transform.localScale = GetRandomTargetSize();
            //     target.transform.LookAt(2 * target.transform.position);

            //     if (Random.value < MovingProbability)
            //     {
            //         target.transform.GetComponent<Target>().Velocity = Random.Range(MinVelocity, MaxVelocity);
            //         target.transform.GetComponent<Target>().Offset = Random.Range(MinOffset, MaxOffset);
            //     }
            // }
        }

        // private Vector3 GetRandomTargetPosition()
        // {
            // var radius = Random.Range(MinDistance, MaxDistance);

            // var minAngle = -SpawnAngle / 2 * Mathf.Deg2Rad;
            // var maxAngle = +SpawnAngle / 2 * Mathf.Deg2Rad;
            // var angle = Random.Range(minAngle, maxAngle);

            // var x = radius * Mathf.Cos(angle);
            // var y = Random.Range(MinHeight, MaxHeight);
            // var z = radius * Mathf.Sin(angle);

            // return new Vector3(x, y, z);
        // }

        // private Vector3 GetRandomTargetSize()
        // {
        //     var size = Random.Range(MinSize, MaxSize);
        //     return new Vector3(size, size, 1);
        // }
    }
}
