using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoBox : MonoBehaviour
{ 
    // Update is called once per frame
    void Update()
    {
        Quaternion direction = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        transform.rotation = direction;
    }
}
