using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : Unit
{

    [SerializeField] private GameObject[] healthIndicator;

    public void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Debug.Log("Collision detected.");
            Destroy(collider.gameObject);

            Health--;
            healthIndicator[Health].SetActive(false);

            Debug.Log(Health);

            if (Health == 0)
            {
                //GameManager.GameOver();
                Debug.Log("Game Over.");
            }
        }
    }
}
