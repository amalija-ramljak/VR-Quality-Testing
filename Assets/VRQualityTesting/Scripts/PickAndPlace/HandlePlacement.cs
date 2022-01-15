using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRQualityTesting.Scripts.PickAndPlace
{
    public class HandlePlacement : MonoBehaviour
    {
        private const string collisionTag = "Target";

        private ObjectSpawner spawner;

        private void Awake() {
            spawner = GameObject.Find("ObjectSpawner").GetComponent<ObjectSpawner>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == collisionTag)
            {
                spawner.handlePlacement();
            }
        }
    }
}
