using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [Header("Start Values")]
    [SerializeField] private int currency = 100; // default overwritten in Inspector

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // ABSTRACTION AND ENCAPSULATION
    public void AddCurrency(int value)
    {
        Instance.currency += value;
        Debug.Log(Instance.currency);
    }

    // ABSTRACTION AND ENCAPSULATION
    public int GetCurrency()
    {
        return currency;
    }

    public enum GameState { 
    
    }
}
