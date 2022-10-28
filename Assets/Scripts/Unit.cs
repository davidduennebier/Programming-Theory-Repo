using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // copied from tutorial
    protected GameObject m_Target;

    // declare Variables
    [SerializeField] public int worth;
    [SerializeField] protected int startHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = startHealth;
    }
}
