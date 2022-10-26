using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private GameObject PlayerTarget = null;

    [SerializeField] private GameObject MouseIndicator;
    [SerializeField] private GameObject ObjectToSpawn;
    [SerializeField] private float spawnCooldownTime;
    private bool spawnCooldown;

    private Vector3 screenPosition;
    private Vector3 worldPosition;

    Plane plane = new Plane(Vector3.down, 0.1f);
    
    /*
    [SerializeField] private int maxDistance = 100;
    // hier kann einer oder mehrere Layer festgelegt werden, die von der Maus registriert werden sollen
    public LayerMask layersToHit;
    */

    // Start is called before the first frame update
    void Start()
    {
        MouseIndicator.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        // ========================== Mouse und Indikator Bewegung ==========================

        // Übersetzung von ScreenPosition in WorldPosition via Raycast
        screenPosition = Input.mousePosition;
        // send out a ray from the screen position (camera near clip plane) to the equivalent point along the cameras frustum
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
        }
        MouseIndicator.transform.position = worldPosition;
        

        // Functionality when left mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // TODO: if mouseOver object --> playertarget = GameObject

            // TODO: if mouseNotOver object (else) --> playerTarget = null
        }

        // Functionality when right mouse button is clicked
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            // TODO: if PlayerTarget == null
            // Spawn ObjectToSpawn at mousePosition
            if (PlayerTarget == null)
                SpawnObject();
        }
    }

    void SpawnObject()
    {
        if (!spawnCooldown)
        {
            spawnCooldown = true;
            Instantiate(ObjectToSpawn, worldPosition, ObjectToSpawn.transform.rotation);
            StartCoroutine("SpawnCooldown");
        }
    }

    private IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(spawnCooldownTime);
        spawnCooldown = false;
    }
}
