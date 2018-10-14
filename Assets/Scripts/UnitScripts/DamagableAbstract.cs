using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamagableAbstract : MonoBehaviour {

    [Header("DamagableAbstract Settings")]

    [SerializeField, Tooltip("The maximum health points this unit can have, the amount it will start with")]
    protected float maxHP;
    [SerializeField, Tooltip("The speed this unit will advance towards the enemy in m/s")]
    protected float moveSpeed;

    protected float hp;



    private void Awake()
    {
        hp = maxHP;
    }

    /// <summary>
    /// Reduces hp by amt and clamps hp from 0 to maxHP
    /// </summary>
    /// <param name="amt">Quantity of damage to take, or negative value to heal</param>
    public virtual void TakeDamage(float amt)
    {
        hp -= amt;

        if (hp < 0)
            hp = 0;
        else if (hp > maxHP)
            hp = maxHP;
    }

}
