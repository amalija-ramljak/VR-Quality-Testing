using BNG;
using UnityEngine;
using VRQualityTesting.Scripts.Core;
using VRQualityTesting.Scripts.Utility;
using VRQualityTesting.Scripts.PickAndPlaceMenu;

namespace VRQualityTesting.Scripts.PickAndPlace
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private Timer timer;
        [SerializeField] private ObjectSpawner objectSpawner;

        private void Awake()
        {
            InitializeTimer();
            InitializeTargetSpawner();
        }

        private void InitializeTimer()
        {
            timer.TimeLeft = Settings.GetFloat(PickAndPlaceKeys.RoundDuration);
        }

        private void InitializeTargetSpawner()
        {
            // targetSpawner.MinDistance = Settings.GetFloat(ShooterKeys.MinDistance);
            // targetSpawner.MaxDistance = Settings.GetFloat(ShooterKeys.MaxDistance);
            // targetSpawner.MinHeight = Settings.GetFloat(ShooterKeys.MinHeight);
            // targetSpawner.MaxHeight = Settings.GetFloat(ShooterKeys.MaxHeight);
            // targetSpawner.SpawnAngle = Settings.GetFloat(ShooterKeys.SpawnAngle);
            // targetSpawner.SpawnCount = Settings.GetInt(ShooterKeys.SpawnCount);
            // targetSpawner.DurationBetweenSpawns = Settings.GetFloat(ShooterKeys.DurationBetweenSpawns);
            // targetSpawner.MinSize = Settings.GetFloat(ShooterKeys.MinSize);
            // targetSpawner.MaxSize = Settings.GetFloat(ShooterKeys.MaxSize);

            // targetSpawner.MovingProbability = Settings.GetFloat(ShooterKeys.MovingProbability);
            // targetSpawner.MinVelocity = Settings.GetFloat(ShooterKeys.MinVelocity);
            // targetSpawner.MaxVelocity = Settings.GetFloat(ShooterKeys.MaxVelocity);
            // targetSpawner.MinOffset = Settings.GetFloat(ShooterKeys.MinOffset);
            // targetSpawner.MaxOffset = Settings.GetFloat(ShooterKeys.MaxOffset);
        }
    }
}
