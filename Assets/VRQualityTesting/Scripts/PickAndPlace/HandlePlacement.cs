using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRQualityTesting.Scripts.PickAndPlace
{
    public class HandlePlacement : MonoBehaviour
    {
        private const string collisionTag = "Target";

        public ObjectSpawner spawner;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == collisionTag)
            {
                Debug.Log("Placed");
                spawner.handlePlacement();
            }
        }
    }
}
