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
            InitializeObjectSpawner();
        }

        private void InitializeTimer()
        {
            timer.TimeLeft = Settings.GetFloat(PickAndPlaceKeys.RoundDuration);
        }

        private void InitializeObjectSpawner()
        {
            objectSpawner.objectMinDistance = Settings.GetFloat(PickAndPlaceKeys.ObjectMinDistance);
            objectSpawner.objectMaxDistance = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxDistance);
            objectSpawner.objectMinHeight = Settings.GetFloat(PickAndPlaceKeys.ObjectMinHeight);
            objectSpawner.objectMaxHeight = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxHeight);
            objectSpawner.objectMaxRotationOffset = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxRotationOffset);
            objectSpawner.objectMinSize = Settings.GetFloat(PickAndPlaceKeys.ObjectMinSize);
            objectSpawner.objectMaxSize = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxSize);

            objectSpawner.useObjectTypeSquare = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeSquare);
            objectSpawner.useObjectTypeCylinder = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeCylinder);
            objectSpawner.useObjectTypeSphere = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeSphere);

            objectSpawner.obstacleMinDistance = Settings.GetFloat(PickAndPlaceKeys.ObstacleMinDistance);
            objectSpawner.obstacleMaxDistance = Settings.GetFloat(PickAndPlaceKeys.ObstacleMaxDistance);
            objectSpawner.obstacleMinSize = Settings.GetFloat(PickAndPlaceKeys.ObstacleMinSize);
            objectSpawner.obstacleMaxSize = Settings.GetFloat(PickAndPlaceKeys.ObstacleMaxSize);
            objectSpawner.obstacleMinCount = Settings.GetInt(PickAndPlaceKeys.ObstacleMinCount);
            objectSpawner.obstacleMaxCount = Settings.GetInt(PickAndPlaceKeys.ObstacleMaxCount);

            objectSpawner.goalDistance = Settings.GetFloat(PickAndPlaceKeys.GoalDistance);
            objectSpawner.goalRotationOffset = Settings.GetFloat(PickAndPlaceKeys.GoalRotationOffset);
            objectSpawner.goalSize = Settings.GetFloat(PickAndPlaceKeys.GoalSize);
            objectSpawner.goalHeight = Settings.GetFloat(PickAndPlaceKeys.GoalHeight);
        }
    }
}
