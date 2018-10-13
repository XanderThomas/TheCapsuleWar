using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageTrigger : MonoBehaviour {

    public float damageOnHit;
    public bool doingDamage;
    public string targetTag;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(targetTag))
        {
            other.GetComponent<ArmyUnit>().TakeDamage(damageOnHit);
        }
    }

}
