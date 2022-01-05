using UnityEngine;
using VRQualityTesting.Scripts.PickAndPlaceMenu;
using VRQualityTesting.Scripts.Utility;

namespace VRQualityTesting.Scripts.PickAndPlace
{
    public class TimerInitializer : MonoBehaviour
    {
        [SerializeField] private Timer timer;

        private void Awake() => InitializeTimer();

        private void InitializeTimer()
        {
            timer.TimeLeft = Settings.GetFloat(PickAndPlaceKeys.RoundDuration);
        }
    }
}