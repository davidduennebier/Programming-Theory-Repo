using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    // declare Variables

    [SerializeField] private int m_Health;

    public int Health
    { 
        get { return m_Health; }
        set {
            if (value < 0)
            {
                m_Health = 0;
            }
            else 
            {
                m_Health = value;
            }
        }
    }
}
