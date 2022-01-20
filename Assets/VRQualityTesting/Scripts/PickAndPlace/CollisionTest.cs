using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRQualityTesting.Scripts.PickAndPlace
{
    public class CollisionTest : MonoBehaviour
    {
        private int intersectionCount = 0;

        public int getIntersectionCount() {
            return intersectionCount;
        }

        private void OnTriggerEnter(Collider other)
        {
            onEnterEvent(other.gameObject.tag);
        }

        private void OnCollisionEnter(Collision other)
        {
            onEnterEvent(other.gameObject.tag);
        }

        private void onEnterEvent(string tag = "")
        {
            if (tag == "CollisionTest") intersectionCount++;
        }


        private void OnTriggerExit(Collider other)
        {
            onExitEvent(other.gameObject.tag);
        }

        private void OnCollisionExit(Collision other)
        {
            onExitEvent(other.gameObject.tag);
        }

        private void onExitEvent(string tag = "")
        {
            if (tag == "CollisionTest") intersectionCount--;
        }
    }
}