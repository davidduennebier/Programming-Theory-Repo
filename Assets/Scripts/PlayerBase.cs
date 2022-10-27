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

            if(currentHealth > 0)
                currentHealth--;
            healthIndicator[currentHealth].SetActive(false);

            Debug.Log(currentHealth);

            if (currentHealth == 0)
            {
                //GameManager.GameOver();
                Debug.Log("Game Over.");
            }
        }
    }
}
