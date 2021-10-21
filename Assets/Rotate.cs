using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private int rotation = 1;
    
    void Update ()
    {
        transform.Rotate (0,0,rotation*Time.deltaTime);
    }

}
