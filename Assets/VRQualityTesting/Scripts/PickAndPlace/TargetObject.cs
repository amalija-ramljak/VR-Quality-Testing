using System;
using UnityEngine;

namespace VRQualityTesting.Scripts.PickAndPlace
{
    public class TargetObject : MonoBehaviour
    {
        public DateTime BirthTimestamp { get; } = DateTime.Now;
        private SessionReporter _sessionReporter;

        private void Awake()
        {
            // _sessionReporter = FindObjectOfType<SessionReporter>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            // _sessionReporter.OnBoxCollision(this, collision);
        }
    }
}