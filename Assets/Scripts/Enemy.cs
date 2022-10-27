using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for Canvas und Healthbars
using UnityEngine.UI;

// Nav Mesh Navigation
using UnityEngine.AI;

public class Enemy : Unit
{

    [Header("Unity Stuff")]
    [SerializeField] private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject healthBar;

    // Start is called before the first frame update
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();

    }

    void Move()
    {
        navMeshAgent.destination = movePositionTransform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            currentHealth -= other.GetComponent<Projectile>().damage;
            float percent = (float)currentHealth / (float)startHealth;
            healthBar.GetComponent<Image>().fillAmount = percent;

            GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectInstance, 2.0f);
            Destroy(other.gameObject);

            if (currentHealth <= 0)
            {
                GameObject deathInstance = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
                Destroy(effectInstance, 2.0f);
                Destroy(gameObject);
            }
        }
    }
}
