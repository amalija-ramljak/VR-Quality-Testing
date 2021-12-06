using UnityEngine;
using System.Collections;

public class DistanceFromCenter : MonoBehaviour
{
    public Transform other;
    
    void Update() {
        
    }

    void OnCollisionEnter(Collision collision) {
        if (other)
        {
            float dist = Vector3.Distance(other.position, transform.position);
            print("Distance to other: " + dist);
        }
    }

    void OnCollisionExit(Collision collision) {
        if (other)
        {
            float dist = Vector3.Distance(other.position, transform.position);
            print("Ćao");
        }
    }
}