using System;
using System.Collections.Generic;
using VRQualityTesting.Scripts.Core;
using VRQualityTesting.Scripts.MainMenu;
using VRQualityTesting.Scripts.Utility;

namespace VRQualityTesting.Scripts.PickAndPlace
{
    public class Session : ISession
    {
        private const string TimestampFormat = "HH:mm:ss:fff";

        public string StudyID => Settings.GetString(MainMenuKeys.StudyID);
        public string ParticipantID => Settings.GetString(MainMenuKeys.ParticipantID);
        public GameTitle GameTitle => GameTitle.PickAndPlace;

        // private readonly int _shotsFiredWithLeftHand;
        // private readonly int _shotsFiredWithRightHand;
        // private readonly List<TargetHit> _hits;

        public Session()
        {
            // _shotsFiredWithLeftHand = shotsFiredWithLeftHand;
            // _shotsFiredWithRightHand = shotsFiredWithRightHand;
            // _hits = hits;
        }

        public List<string> GeneralInformation
        {
            get
            {
                // var hits = _hits.Count;
                // var hitsByRightHand = _hits.Count(hit => hit.HandSide == HandSide.Right);
                // var hitsByLeftHand = hits - hitsByRightHand;
                // var totalShotsFired = _shotsFiredWithLeftHand + _shotsFiredWithRightHand;

                // return new List<string>
                // {
                //     "# Round results",
                //     $"Total shots fired: {totalShotsFired}",
                //     "",
                //     $"Right hand shots fired: {_shotsFiredWithRightHand}",
                //     "",
                //     $"Left hand shots fired: {_shotsFiredWithLeftHand}",
                //     "",
                //     $"Duration: {Settings.GetFloat(PickAndPlaceKeys.RoundDuration).ToString(CultureInfo.InvariantCulture)}",

                //     $"{Environment.NewLine}# Tab settings",
                //     $"Field name: {Settings.GetFloat(PickAndPlaceKeys.FieldName).ToString(CultureInfo.InvariantCulture)}",
                // };
                return new List<string>
                {
                    "Dummy",
                };
            }
        }

        public List<string> DetailedInformation =>
            throw new NotImplementedException("Return a list of lines that represent detailed round results.");
    }
}
