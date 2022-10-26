using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : Unit
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Collision detected.");
            Destroy(other.gameObject);
            Health--;

            Debug.Log(Health);

            if (Health == 0)
            {
                //GameManager.GameOver();
                Debug.Log("Game Over.");
            }
        }
    }
}
