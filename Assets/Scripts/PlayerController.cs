using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Selection")]
    private GameObject PlayerTarget = null;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private Transform _selection;

    [Header("Mouse Indicator")]
    [SerializeField] private GameObject MouseIndicator;

    [Header("Objects to Spawn")]
    [SerializeField] private GameObject[] ObjectToSpawn;
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

        // if mouse hovers an object
        // something should happen
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag("Tower") || selection.CompareTag("Enemy"))
            { 
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    defaultMaterial = selectionRenderer.material;
                    selectionRenderer.material = highlightMaterial;
                }
                _selection = selection;
            }
        }
        


        // Functionality when left mouse button is clicked
        // hier wird ein Gameobject als Playertarget gesetzt
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (selection.CompareTag("Tower") || selection.CompareTag("Enemy"))
                {
                    PlayerTarget = selection.gameObject;
                    Debug.Log(PlayerTarget);

                    if (selection.name == "TowerA" || selection.name == "TowerA(Clone)")
                    {
                        Vector3 posT = PlayerTarget.transform.position;
                        Destroy(PlayerTarget);
                        Instantiate(ObjectToSpawn[1], posT, ObjectToSpawn[1].transform.rotation);
                    }
                    else if (selection.name == "TowerB" || selection.name == "TowerB(Clone)")
                    {
                        Vector3 posT = PlayerTarget.transform.position;
                        Destroy(PlayerTarget);
                        Instantiate(ObjectToSpawn[2], posT, ObjectToSpawn[2].transform.rotation);
                    }
                }
                else
                {
                    PlayerTarget = null;
                }
                    
            }
            // TODO: if mouseNotOver object (else) --> playerTarget = null
        }

        // Functionality when right mouse button is clicked
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            // Spawn ObjectToSpawn at mousePosition
                SpawnObject();
        }
    }

    void SpawnObject()
    {
        if (!spawnCooldown)
        {
            spawnCooldown = true;
            Instantiate(ObjectToSpawn[0], (worldPosition + ObjectToSpawn[0].transform.position), ObjectToSpawn[0].transform.rotation);
            StartCoroutine("SpawnCooldown");
            Debug.Log("Spawned new tower.");
        }
    }

    private IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(spawnCooldownTime);
        spawnCooldown = false;
    }

    public void HandleSelection()
    {

    }
}
