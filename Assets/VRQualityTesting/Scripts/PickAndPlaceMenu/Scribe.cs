using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRQualityTesting.Scripts.Utility; // Settings.

namespace VRQualityTesting.Scripts.PickAndPlaceMenu
{
    public class Scribe : MonoBehaviour
    {
        // Objects tab
        [Header("Objects")]
        [SerializeField] private TMP_InputField objectMinDistanceField;
        [SerializeField] private TMP_InputField objectMaxDistanceField;
        [SerializeField] private TMP_InputField objectMaxRotationOffsetField;
        [SerializeField] private TMP_InputField objectMinSizeField;
        [SerializeField] private TMP_InputField objectMaxSizeField;
        [SerializeField] private Toggle useObjectTypeSquareField;
        [SerializeField] private Toggle useObjectTypeCylinderField;
        [SerializeField] private Toggle useObjectTypeSphereField;

        // Obstacles tab
        [Header("Obstacles")]
        [SerializeField] private TMP_InputField obstacleMinDistanceField;
        [SerializeField] private TMP_InputField obstacleMaxDistanceField;
        [SerializeField] private TMP_InputField obstacleMinCountField;
        [SerializeField] private TMP_InputField obstacleMaxCountField;
        [SerializeField] private TMP_InputField obstacleMaxSizeField;
        [SerializeField] private TMP_InputField obstacleMinSizeField;

        // Goals tab
        [Header("Goals")]
        [SerializeField] private TMP_InputField goalMinDistanceField;
        [SerializeField] private TMP_InputField goalMaxDistanceField;
        [SerializeField] private TMP_InputField goalMaxRotationOffsetField;
        [SerializeField] private TMP_InputField goalMinSizeField;
        [SerializeField] private TMP_InputField goalMaxSizeField;

        // Round tab
        [Header("Round")]
        [SerializeField] private TMP_InputField roundDurationField;

        private void Start() => InitializeSettings();

        private void InitializeSettings()
        {
            InitializeObjectsSettings();
            InitializeGoalsSettings();
            InitializeObstaclesSettings();
            InitializeRoundSettings();
        }

        private void InitializeObjectsSettings()
        {
            objectMinDistanceField.text = Settings.GetFloat(PickAndPlaceKeys.ObjectMinDistance, defaultValue: 1f).ToString(CultureInfo.InvariantCulture);
            objectMaxDistanceField.text = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxDistance, defaultValue: 2f).ToString(CultureInfo.InvariantCulture);

            objectMaxRotationOffsetField.text = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxRotationOffset, defaultValue: 180f).ToString(CultureInfo.InvariantCulture);

            objectMinSizeField.text = Settings.GetFloat(PickAndPlaceKeys.ObjectMinSize, defaultValue: 1f).ToString(CultureInfo.InvariantCulture);
            objectMaxSizeField.text = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxSize, defaultValue: 2f).ToString(CultureInfo.InvariantCulture);

            useObjectTypeSquareField.isOn = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeSquare, defaultValue: true);
            useObjectTypeCylinderField.isOn = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeCylinder, defaultValue: true);
            useObjectTypeSphereField.isOn = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeSphere, defaultValue: true);
        }

        private void InitializeGoalsSettings()
        {
            goalMinDistanceField.text = Settings.GetFloat(PickAndPlaceKeys.GoalMinDistance, defaultValue: 1f).ToString(CultureInfo.InvariantCulture);
            goalMaxDistanceField.text = Settings.GetFloat(PickAndPlaceKeys.GoalMaxDistance, defaultValue: 2f).ToString(CultureInfo.InvariantCulture);

            goalMaxRotationOffsetField.text = Settings.GetFloat(PickAndPlaceKeys.GoalMaxRotationOffset, defaultValue: 180f).ToString(CultureInfo.InvariantCulture);

            goalMinSizeField.text = Settings.GetFloat(PickAndPlaceKeys.GoalMinSize, defaultValue: 1f).ToString(CultureInfo.InvariantCulture);
            goalMaxSizeField.text = Settings.GetFloat(PickAndPlaceKeys.GoalMaxSize, defaultValue: 2f).ToString(CultureInfo.InvariantCulture);
        }

        private void InitializeObstaclesSettings()
        {
            obstacleMinDistanceField.text = Settings.GetFloat(PickAndPlaceKeys.ObstacleMinDistance, defaultValue: 1f).ToString(CultureInfo.InvariantCulture);
            obstacleMaxDistanceField.text = Settings.GetFloat(PickAndPlaceKeys.ObstacleMaxDistance, defaultValue: 2f).ToString(CultureInfo.InvariantCulture);

            obstacleMinCountField.text = Settings.GetInt(PickAndPlaceKeys.ObstacleMinCount, defaultValue: 0).ToString(CultureInfo.InvariantCulture);
            obstacleMaxCountField.text = Settings.GetInt(PickAndPlaceKeys.ObstacleMaxCount, defaultValue: 0).ToString(CultureInfo.InvariantCulture);

            obstacleMinSizeField.text = Settings.GetFloat(PickAndPlaceKeys.ObstacleMinSize, defaultValue: 1f).ToString(CultureInfo.InvariantCulture);
            obstacleMaxSizeField.text = Settings.GetFloat(PickAndPlaceKeys.ObstacleMaxSize, defaultValue: 2f).ToString(CultureInfo.InvariantCulture);
        }

        private void InitializeRoundSettings()
        {
            roundDurationField.text = Settings.GetFloat(PickAndPlaceKeys.RoundDuration, defaultValue: 60f).ToString(CultureInfo.InvariantCulture);
        }

        public void SaveSettings()
        {
            SaveObjectsSettings();
            SaveGoalsSettings();
            SaveObstaclesSettings();
            SaveRoundSettings();
        }

        private void SaveObjectsSettings()
        {
            Settings.SetFloat(PickAndPlaceKeys.ObjectMinDistance, float.Parse(objectMinDistanceField.text, CultureInfo.InvariantCulture));
            Settings.SetFloat(PickAndPlaceKeys.ObjectMaxDistance, float.Parse(objectMaxDistanceField.text, CultureInfo.InvariantCulture));

            Settings.SetFloat(PickAndPlaceKeys.ObjectMaxRotationOffset, float.Parse(objectMaxRotationOffsetField.text, CultureInfo.InvariantCulture));
            
            Settings.SetFloat(PickAndPlaceKeys.ObjectMinSize, float.Parse(objectMinSizeField.text, CultureInfo.InvariantCulture));
            Settings.SetFloat(PickAndPlaceKeys.ObjectMaxSize, float.Parse(objectMaxSizeField.text, CultureInfo.InvariantCulture));

            Settings.SetBool(PickAndPlaceKeys.UseObjectTypeSquare, useObjectTypeSquareField.isOn);
            Settings.SetBool(PickAndPlaceKeys.UseObjectTypeCylinder, useObjectTypeCylinderField.isOn);
            Settings.SetBool(PickAndPlaceKeys.UseObjectTypeSphere, useObjectTypeSphereField.isOn);
        }

        private void SaveGoalsSettings()
        {
            Settings.SetFloat(PickAndPlaceKeys.GoalMinDistance, float.Parse(goalMinDistanceField.text, CultureInfo.InvariantCulture));
            Settings.SetFloat(PickAndPlaceKeys.GoalMaxDistance, float.Parse(goalMaxDistanceField.text, CultureInfo.InvariantCulture));

            Settings.SetFloat(PickAndPlaceKeys.GoalMaxRotationOffset, float.Parse(goalMaxRotationOffsetField.text, CultureInfo.InvariantCulture));

            Settings.SetFloat(PickAndPlaceKeys.GoalMinSize, float.Parse(goalMinSizeField.text, CultureInfo.InvariantCulture));
            Settings.SetFloat(PickAndPlaceKeys.GoalMaxSize, float.Parse(goalMaxSizeField.text, CultureInfo.InvariantCulture));
        }

        private void SaveObstaclesSettings()
        {
            Settings.SetFloat(PickAndPlaceKeys.ObstacleMinDistance, float.Parse(obstacleMinDistanceField.text, CultureInfo.InvariantCulture));
            Settings.SetFloat(PickAndPlaceKeys.ObstacleMaxDistance, float.Parse(obstacleMaxDistanceField.text, CultureInfo.InvariantCulture));

            Settings.SetInt(PickAndPlaceKeys.ObstacleMinCount, int.Parse(obstacleMinCountField.text, CultureInfo.InvariantCulture));
            Settings.SetInt(PickAndPlaceKeys.ObstacleMaxCount, int.Parse(obstacleMaxCountField.text, CultureInfo.InvariantCulture));
            
            Settings.SetFloat(PickAndPlaceKeys.ObstacleMinSize, float.Parse(obstacleMinSizeField.text, CultureInfo.InvariantCulture));
            Settings.SetFloat(PickAndPlaceKeys.ObstacleMaxSize, float.Parse(obstacleMaxSizeField.text, CultureInfo.InvariantCulture));
        }

        private void SaveRoundSettings()
        {
            Settings.SetFloat(PickAndPlaceKeys.RoundDuration, float.Parse(roundDurationField.text, CultureInfo.InvariantCulture));
        }
    }
}
