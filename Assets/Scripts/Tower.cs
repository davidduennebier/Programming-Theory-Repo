using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Unit
{
    [SerializeField] private float AttackSpeed;
    [SerializeField] private float AttackRange;
    [SerializeField] private int Damage;

    private bool isUpgradable;

    void SearchForTarget()
    { 

        // TODO:
        // if Target == null;
        // suche nach target in Range
        // wenn target in Range
        // SetTarget();
    }
    void SetTarget()
    { 
        // TODO:
        // Target = new Target;
    }

    void LookAtTarget(GameObject target)
    { 
        // TODO: lookAtFunktion einfügen
    }

    IEnumerator Attack(GameObject target)
    {
        // target.Health -= Damage;
        yield return new WaitForSeconds(AttackSpeed);
    }
}
