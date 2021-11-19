using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceCircle : MonoBehaviour
{
    public void InstanceInCircle(int numberOfObjects, float radius)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            GameObject go = Instantiate(gameObject, pos, rot);
            go.transform.localScale *= 0.5f;
            
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.mass *= 0.5f;
        }
    }
}
