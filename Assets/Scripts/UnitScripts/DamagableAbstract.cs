using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for things which take damage
/// </summary>
public abstract class DamagableAbstract : MonoBehaviour {

    [Header("DamagableAbstract Settings")]

    [SerializeField, Tooltip("The maximum health points this unit can have, the amount it will start with")]
    protected float maxHP;

    //The variable for current HP
    protected float hp;

    //The property for externally accessing current HP
    public float HP { get { return hp; } }



    private void Awake()
    {
        //Set current HP to max HP when the object is created
        hp = maxHP;
    }

    /// <summary>
    /// Reduces hp by amt and clamps hp from 0 to maxHP
    /// </summary>
    /// <param name="amt">Quantity of damage to take, or negative value to heal</param>
    public virtual void TakeDamage(float amt)
    {
        hp -= amt;

        //Clamp current HP to the range 0 to maxHP
        if (hp < 0)
            hp = 0;
        else if (hp > maxHP)
            hp = maxHP;
    }

}
