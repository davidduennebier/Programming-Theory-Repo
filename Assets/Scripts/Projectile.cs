using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;

    [SerializeField] private float projectileSpeed = 50.0f;

    private int m_damage = 25;
    public int damage
    { 
        get { return m_damage; }
        set
        {
            if (value < 0)
            {
                Debug.LogError("Damage can't be negative.");
            }
            else
            { 
                m_damage = value;
            }
        }
    }

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
        // Bullet bewegen
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
}
