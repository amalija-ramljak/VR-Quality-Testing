using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRQualityTesting.Scripts.Utility; // Settings.

namespace VRQualityTesting.Scripts.PickAndPlaceMenu
{
    public class Scribe : MonoBehaviour
    {
        [SerializeField] private TMP_InputField minTranslationField;
        [SerializeField] private TMP_InputField maxTranslationField;
        [SerializeField] private TMP_InputField minRotationField;
        [SerializeField] private TMP_InputField maxRotationField;
        [SerializeField] private TMP_InputField objectCount;
        [SerializeField] private Toggle useObjectTypeSquare;
        [SerializeField] private Toggle useObjectTypeCylinder;
        [SerializeField] private Toggle useObjectTypeSphere;
        // [SerializeField] private TMP_Dropdown weaponTypeDropdown;

        private void Start() => InitializeSettings();

        private void InitializeSettings()
        {
            InitializeTabSettings();
        }

        private void InitializeTabSettings()
        {
            // <fieldName>Field.text = Settings.GetFloat(PickAndPlaceKeys.<FieldName>, defaultValue: <num>f).ToString(CultureInfo.InvariantCulture);
        }

        public void SaveSettings()
        {
            SaveTabSettings();
        }

        private void SaveTabSettings()
        {
            // Settings.SetFloat(PickAndPlaceKeys.<FieldName>, float.Parse(<fieldName>Field.text, CultureInfo.InvariantCulture));
        }
    }
}
