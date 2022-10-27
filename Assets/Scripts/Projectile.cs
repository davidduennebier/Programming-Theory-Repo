using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;

    [SerializeField] private float projectileSpeed = 50.0f;
    [SerializeField] private GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        // falls enemy verschwindet bevor die Kugel den enemy erreicht
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // die Richtung und Distanz ermitteln:
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = projectileSpeed * Time.deltaTime;

        // ein eleganter Weg um eine Collision herum
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2.0f);
        Destroy(gameObject);
    }
}
