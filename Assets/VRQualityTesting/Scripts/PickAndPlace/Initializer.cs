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
            objectSpawner.ObjectMinDistance = Settings.GetFloat(PickAndPlaceKeys.ObjectMinDistance);
            objectSpawner.ObjectMaxDistance = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxDistance);
            objectSpawner.ObjectMaxRotationOffset = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxRotationOffset);
            objectSpawner.ObjectMinSize = Settings.GetFloat(PickAndPlaceKeys.ObjectMinSize);
            objectSpawner.ObjectMaxSize = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxSize);
            objectSpawner.UseObjectTypeSquare = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeSquare);
            objectSpawner.UseObjectTypeCylinder = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeCylinder);
            objectSpawner.UseObjectTypeSphere = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeSphere);

            //objectSpawner.GoalMinDistance = Settings.GetFloat(PickAndPlaceKeys.GoalMinDistance);
            //objectSpawner.GoalMaxDistance = Settings.GetFloat(PickAndPlaceKeys.GoalMaxDistance);
            //objectSpawner.GoalMaxRotationOffset = Settings.GetFloat(PickAndPlaceKeys.GoalMaxRotationOffset);
            //objectSpawner.GoalMinSize = Settings.GetFloat(PickAndPlaceKeys.GoalMinSize);
            //objectSpawner.GoalMaxSize = Settings.GetFloat(PickAndPlaceKeys.GoalMaxSize);

            objectSpawner.GoalDistance = Settings.GetFloat(PickAndPlaceKeys.GoalDistance);
            objectSpawner.GoalRotationOffset = Settings.GetFloat(PickAndPlaceKeys.GoalRotationOffset);
            objectSpawner.GoalSize = Settings.GetFloat(PickAndPlaceKeys.GoalSize);
            objectSpawner.GoalHeight = Settings.GetFloat(PickAndPlaceKeys.GoalHeight);

            objectSpawner.ObstacleMinDistance = Settings.GetFloat(PickAndPlaceKeys.ObstacleMinDistance);
            objectSpawner.ObstacleMaxDistance = Settings.GetFloat(PickAndPlaceKeys.ObstacleMaxDistance);
            objectSpawner.ObstacleMinCount = Settings.GetInt(PickAndPlaceKeys.ObstacleMinCount);
            objectSpawner.ObstacleMaxCount = Settings.GetInt(PickAndPlaceKeys.ObstacleMaxCount);
            objectSpawner.ObstacleMinSize = Settings.GetFloat(PickAndPlaceKeys.ObstacleMinSize);
            objectSpawner.ObstacleMaxSize = Settings.GetFloat(PickAndPlaceKeys.ObstacleMaxSize);
        }
    }
}
