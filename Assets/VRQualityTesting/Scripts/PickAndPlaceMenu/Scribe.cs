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
        [SerializeField] private TMP_InputField minTranslationField;
        [SerializeField] private TMP_InputField maxTranslationField;
        [SerializeField] private TMP_InputField minRotationField;
        [SerializeField] private TMP_InputField maxRotationField;
        [SerializeField] private TMP_InputField objectCountField;
        [SerializeField] private Toggle useObjectTypeSquareField;
        [SerializeField] private Toggle useObjectTypeCylinderField;
        [SerializeField] private Toggle useObjectTypeSphereField;
        // [SerializeField] private TMP_Dropdown weaponTypeDropdown;

        // Round tab
        [SerializeField] private TMP_InputField roundDurationField;

        private void Start() => InitializeSettings();

        private void InitializeSettings()
        {
            InitializeObjectsSettings();
            InitializeRoundSettings();
        }

        private void InitializeObjectsSettings()
        {
            minTranslationField.text = Settings.GetFloat(PickAndPlaceKeys.MinTranslation, defaultValue: 0f).ToString(CultureInfo.InvariantCulture);
            maxTranslationField.text = Settings.GetFloat(PickAndPlaceKeys.MaxTranslation, defaultValue: 0f).ToString(CultureInfo.InvariantCulture);
            minRotationField.text = Settings.GetFloat(PickAndPlaceKeys.MinRotation, defaultValue: 0f).ToString(CultureInfo.InvariantCulture);
            maxRotationField.text = Settings.GetFloat(PickAndPlaceKeys.MaxRotation, defaultValue: 0f).ToString(CultureInfo.InvariantCulture);

            objectCountField.text = Settings.GetInt(PickAndPlaceKeys.ObjectCount, defaultValue: 0).ToString(CultureInfo.InvariantCulture);

            useObjectTypeSquareField.isOn = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeSquare, defaultValue: true).ToString(CultureInfo.InvariantCulture);
            useObjectTypeCylinderField.isOn = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeCylinder, defaultValue: true).ToString(CultureInfo.InvariantCulture);
            useObjectTypeSphereField.isOn = Settings.GetBool(PickAndPlaceKeys.UseObjectTypeSphere, defaultValue: true).ToString(CultureInfo.InvariantCulture);
        }

        private void InitializeRoundSettings()
        {
            roundDurationField.text = Settings.GetFloat(PickAndPlaceKeys.RoundDuration, defaultValue: 60f).ToString(CultureInfo.InvariantCulture);
        }

        public void SaveSettings()
        {
            SaveObjectsSettings();
            SaveRoundSettings();
        }

        private void SaveObjectsSettings()
        {
            Settings.SetFloat(PickAndPlaceKeys.MinTranslation, float.Parse(minTranslationField.text, CultureInfo.InvariantCulture));
            Settings.SetFloat(PickAndPlaceKeys.MaxTranslation, float.Parse(maxTranslationField.text, CultureInfo.InvariantCulture));
            Settings.SetFloat(PickAndPlaceKeys.MinRotation, float.Parse(minRotationField.text, CultureInfo.InvariantCulture));
            Settings.SetFloat(PickAndPlaceKeys.MaxRotation, float.Parse(maxRotationField.text, CultureInfo.InvariantCulture));

            Settings.SetInt(PickAndPlaceKeys.ObjectCount, int.Parse(objectCountField.text, CultureInfo.InvariantCulture));

            Settings.SetBool(PickAndPlaceKeys.UseObjectTypeSquare, useObjectTypeSquareField.isOn);
            Settings.SetBool(PickAndPlaceKeys.UseObjectTypeCylinder, useObjectTypeCylinderField.isOn);
            Settings.SetBool(PickAndPlaceKeys.UseObjectTypeSphere, useObjectTypeSphereField.isOn);
        }

        private void SaveRoundSettings()
        {
            Settings.SetFloat(PickAndPlaceKeys.RoundDuration, float.Parse(roundDurationField.text, CultureInfo.InvariantCulture));
        }
    }
}
