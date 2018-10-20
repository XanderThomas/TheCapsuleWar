using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageTrigger : WeaponAbstract {

    public float damageOnHit;
    public bool doingDamage;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(targetTag))
        {
            other.GetComponent<DamagableAbstract>().TakeDamage(damageOnHit);
        }
    }

}
