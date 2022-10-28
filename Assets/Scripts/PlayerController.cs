using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // ENCAPSULATION - only prompted once at game start
    private string m_PlayerName = "David Setter";
    public string PlayerName 
    { 
        get { return m_PlayerName; }
        set {
            if (value.Length == 0)
            {
                Debug.LogError("Please enter a player name");
            }
            else
            { 
                m_PlayerName = value;
            }
        }
    }

    [Header("Mouse Selection")]
    private GameObject PlayerTarget = null;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private Transform _selection;

    private Ray ray;
    private RaycastHit hit;

    [Header("Mouse Indicator")]
    [SerializeField] private GameObject MouseIndicator;

    [Header("Objects to Spawn")]
    [SerializeField] private GameObject[] ObjectToSpawn;
    [SerializeField] private float spawnCooldownTime;
    private bool spawnCooldown;
    private bool _bMouseOverObject;
    private bool _bIsOverObject;

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

        // ENCAPSULATION - Prompt
        Debug.Log("Playername: " + PlayerName);
    }


    // Update is called once per frame
    void Update()
    {
        MouseMovement();


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandleSelection();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            // Spawn ObjectToSpawn at mousePosition
            if (!_bIsOverObject)
            {
                if (GameManager.Instance.GetCurrency() >= ObjectToSpawn[0].GetComponent<Tower>().buyPrice)
                    SpawnObject();
                else
                    Debug.Log("Not enough currency.");
            }
            else
            {
                Destroy(_selection.gameObject);
                _bIsOverObject = false;
                GameManager.Instance.AddCurrency(_selection.gameObject.GetComponent<Tower>().worth);
            }
        }
    }

    // ABSTRACTION
    void SpawnObject()
    {
        if (!spawnCooldown)
        {
            spawnCooldown = true;
            Instantiate(ObjectToSpawn[0], (worldPosition + ObjectToSpawn[0].transform.position), ObjectToSpawn[0].transform.rotation);
            StartCoroutine("SpawnCooldown");
            Debug.Log("Spawned new tower.");
            GameManager.Instance.AddCurrency(-ObjectToSpawn[0].GetComponent<Tower>().buyPrice);
        }
    }

    private IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(spawnCooldownTime);
        spawnCooldown = false;
    }

    // ABSTRACTION
    void MouseMovement()
    {
        // Übersetzung von ScreenPosition in WorldPosition via Raycast
        screenPosition = Input.mousePosition;
        // send out a ray from the screen position (camera near clip plane) to the equivalent point along the cameras frustum
        ray = Camera.main.ScreenPointToRay(screenPosition);
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
            _bMouseOverObject = false;
            _selection = null;
            _bIsOverObject = false;
        }

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
                    _bMouseOverObject = true;
                }
                _selection = selection;
                _bIsOverObject = true;
            }
        }
    }

    // ABSTRACTION
    public void HandleSelection()
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
                    if (GameManager.Instance.GetCurrency() >= PlayerTarget.GetComponent<Tower>().upgradePrice)
                    {
                        GameManager.Instance.AddCurrency(-PlayerTarget.GetComponent<Tower>().upgradePrice);
                        Vector3 posT = PlayerTarget.transform.position;
                        Destroy(PlayerTarget);
                        Instantiate(ObjectToSpawn[1], posT, ObjectToSpawn[1].transform.rotation);
                    }
                    else 
                    {
                        Debug.Log("Not enough currency.");
                    }
                }
                else if (selection.name == "TowerB" || selection.name == "TowerB(Clone)")
                {
                    if (GameManager.Instance.GetCurrency() >= PlayerTarget.GetComponent<Tower>().upgradePrice)
                    {
                        GameManager.Instance.AddCurrency(-PlayerTarget.GetComponent<Tower>().upgradePrice);
                        Vector3 posT = PlayerTarget.transform.position;
                        Destroy(PlayerTarget);
                        Instantiate(ObjectToSpawn[2], posT, ObjectToSpawn[2].transform.rotation);
                    }
                    else
                    {
                        Debug.Log("Not enough currency.");
                    }
                }
            }
            else
            {
                PlayerTarget = null;
                _bIsOverObject = false;
            }

        }

        // Functionality when right mouse button is clicked
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            // Spawn ObjectToSpawn at mousePosition
            if (!_bMouseOverObject)
                SpawnObject();
            else
            {
                Destroy(_selection.gameObject);
                _bMouseOverObject = false;
            }
        }
    }
}
