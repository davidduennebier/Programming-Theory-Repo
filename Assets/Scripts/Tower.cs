using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// INHERITANCE - Tower derives from Unit
public class Tower : Unit
{
    [Header("Attributes")]

    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackCountdown;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectileSpawnPoint;
    [SerializeField] public int buyPrice;
    [SerializeField] public int upgradePrice;

    private bool isUpgradable;
    private Vector3 shootFromPosition;
    private Transform target;

    private void Awake()
    {
        // Updates turrets only twice a second, not every frame, saves a lot of performance
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            { 
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= attackRange)
        {
            target = nearestEnemy.transform;
        }
        else 
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
            return;
        else
        {
            float tarX = target.transform.position.x;
            float tarZ = target.transform.position.z;
            Vector3 lookPosition = new Vector3(tarX, transform.position.y, tarZ);

            // POLYMORPHISM - overriding the LookAt()-Function which could either just take Position or a Position and worldUp Vector3.up
            transform.LookAt(lookPosition);
        }

        if (attackCountdown <= 0f)
        {
            Attack();
            attackCountdown = 1f / attackSpeed;
        }

        attackCountdown -= Time.deltaTime;
    }

    // ABSTRACTION
    void Attack()
    {
        GameObject projectileGO = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, projectilePrefab.transform.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null)
            projectile.Seek(target);
    }
}
