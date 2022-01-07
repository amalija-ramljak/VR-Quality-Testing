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
        [SerializeField] private TMP_InputField objectMinHeightField;
        [SerializeField] private TMP_InputField objectMaxHeightField;
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

        //// Goals tab
        //[Header("Goals")]
        //[SerializeField] private TMP_InputField goalMinDistanceField;
        //[SerializeField] private TMP_InputField goalMaxDistanceField;
        //[SerializeField] private TMP_InputField goalMaxRotationOffsetField;
        //[SerializeField] private TMP_InputField goalMinSizeField;
        //[SerializeField] private TMP_InputField goalMaxSizeField;

        // Goals tab
        [Header("Goals")]
        [SerializeField] private TMP_InputField goalDistanceField;
        [SerializeField] private TMP_InputField goalRotationOffsetField;
        [SerializeField] private TMP_InputField goalSizeField;
        [SerializeField] private TMP_InputField goalHeightField;

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

            objectMinHeightField.text = Settings.GetFloat(PickAndPlaceKeys.ObjectMinHeight, defaultValue: 1f).ToString(CultureInfo.InvariantCulture);
            objectMaxHeightField.text = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxHeight, defaultValue: 2f).ToString(CultureInfo.InvariantCulture);

            objectMaxRotationOffsetField.text = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxRotationOffset, defaultValue: 180f).ToString(CultureInfo.InvariantCulture);

            objectMinSizeField.text = Settings.GetFloat(PickAndPlaceKeys.ObjectMinSize, defaultValue: 1f).ToString(CultureInfo.InvariantCulture);
            objectMaxSizeField.text = Settings.GetFloat(PickAndPlaceKeys.ObjectMaxSize, defaultValue: 2f).ToString(CultureInfo.InvariantCulture);

            useObjectTypeSquareField.isOn = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeSquare, defaultValue: true);
            useObjectTypeCylinderField.isOn = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeCylinder, defaultValue: true);
            useObjectTypeSphereField.isOn = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeSphere, defaultValue: true);
        }

        private void InitializeGoalsSettings()
        {
            goalDistanceField.text = Settings.GetFloat(PickAndPlaceKeys.GoalDistance, defaultValue: 1f).ToString(CultureInfo.InvariantCulture);

            goalRotationOffsetField.text = Settings.GetFloat(PickAndPlaceKeys.GoalRotationOffset, defaultValue: 0f).ToString(CultureInfo.InvariantCulture);

            goalSizeField.text = Settings.GetFloat(PickAndPlaceKeys.GoalSize, defaultValue: 1f).ToString(CultureInfo.InvariantCulture);
            goalHeightField.text = Settings.GetFloat(PickAndPlaceKeys.GoalHeight, defaultValue: 1.5f).ToString(CultureInfo.InvariantCulture);
        }

        private void InitializeObstaclesSettings()
        {
            obstacleMinDistanceField.text = Settings.GetFloat(PickAndPlaceKeys.ObstacleMinDistance, defaultValue: 2f).ToString(CultureInfo.InvariantCulture);
            obstacleMaxDistanceField.text = Settings.GetFloat(PickAndPlaceKeys.ObstacleMaxDistance, defaultValue: 3f).ToString(CultureInfo.InvariantCulture);

            obstacleMinCountField.text = Settings.GetInt(PickAndPlaceKeys.ObstacleMinCount, defaultValue: 5).ToString(CultureInfo.InvariantCulture);
            obstacleMaxCountField.text = Settings.GetInt(PickAndPlaceKeys.ObstacleMaxCount, defaultValue: 10).ToString(CultureInfo.InvariantCulture);

            obstacleMinSizeField.text = Settings.GetFloat(PickAndPlaceKeys.ObstacleMinSize, defaultValue: 0.5f).ToString(CultureInfo.InvariantCulture);
            obstacleMaxSizeField.text = Settings.GetFloat(PickAndPlaceKeys.ObstacleMaxSize, defaultValue: 1.5f).ToString(CultureInfo.InvariantCulture);
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

            Settings.SetFloat(PickAndPlaceKeys.ObjectMinHeight, float.Parse(objectMinHeightField.text, CultureInfo.InvariantCulture));
            Settings.SetFloat(PickAndPlaceKeys.ObjectMaxHeight, float.Parse(objectMaxHeightField.text, CultureInfo.InvariantCulture));

            Settings.SetFloat(PickAndPlaceKeys.ObjectMaxRotationOffset, float.Parse(objectMaxRotationOffsetField.text, CultureInfo.InvariantCulture));
            
            Settings.SetFloat(PickAndPlaceKeys.ObjectMinSize, float.Parse(objectMinSizeField.text, CultureInfo.InvariantCulture));
            Settings.SetFloat(PickAndPlaceKeys.ObjectMaxSize, float.Parse(objectMaxSizeField.text, CultureInfo.InvariantCulture));

            Settings.SetBool(PickAndPlaceKeys.UseObjectTypeSquare, useObjectTypeSquareField.isOn);
            Settings.SetBool(PickAndPlaceKeys.UseObjectTypeCylinder, useObjectTypeCylinderField.isOn);
            Settings.SetBool(PickAndPlaceKeys.UseObjectTypeSphere, useObjectTypeSphereField.isOn);
        }

        private void SaveGoalsSettings()
        {
            Settings.SetFloat(PickAndPlaceKeys.GoalDistance, float.Parse(goalDistanceField.text, CultureInfo.InvariantCulture));

            Settings.SetFloat(PickAndPlaceKeys.GoalRotationOffset, float.Parse(goalRotationOffsetField.text, CultureInfo.InvariantCulture));

            Settings.SetFloat(PickAndPlaceKeys.GoalSize, float.Parse(goalSizeField.text, CultureInfo.InvariantCulture));
            Settings.SetFloat(PickAndPlaceKeys.GoalHeight, float.Parse(goalHeightField.text, CultureInfo.InvariantCulture));
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
