using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller script which handles combat units' hp, movement & attack animations
/// </summary>
public class ArmyUnit : MonoBehaviour {

    [Header("Settings")]

    [SerializeField, Tooltip("The maximum health points this unit can have, the amount it will start with")]
    private float maxHP;
    [SerializeField, Tooltip("The speed this unit will advance towards the enemy in m/s")]
    private float moveSpeed;
    [SerializeField, Tooltip("The distance from the enemy line this unit will stop at and begin attacking, in meters")]
    private float attackRange;

    [Header("Links")]

    [SerializeField, Tooltip("The programmed animator which will play while attacking")]
    private ProgAnimatorAbstract attackAnimator;
    [SerializeField, Tooltip("This unit's weapon's script")]
    private WeaponAbstract weapon;

    [HideInInspector]
    public ArmyManager allyManager;
    [HideInInspector]
    public ArmyManager enemyManager;

    public float distFromSpawn { get; private set; }
    public float distFromEnemySpawn { get; private set; }

    //Either 1f or -1f to move the unit towards either the left or right base
    private float moveDir;
    private float hp;



    /// <summary>
    /// To be called by the script which creates an ArmyUnit instance immediately or before frame end
    /// </summary>
    public void Initialize()
    {
        distFromSpawn = 0f;
        hp = maxHP;
        
        weapon.targetTag = enemyManager.TeamUnitTag;
        
        //Set move direction and distance from enemy spawn based on whether the unit is left or right of the enemy spawn
        Vector3 pos = transform.position;
        if (enemyManager.unitStartX > pos.x)
        {
            distFromEnemySpawn = enemyManager.unitStartX - pos.x;
            moveDir = 1f;
        }
        else
        {
            distFromEnemySpawn = pos.x - enemyManager.unitStartX;
            moveDir = -1f;
        }
    }

    /// <summary>
    /// Frame-by-frame update method for ArmyUnit
    /// </summary>
    /// <param name="dt">Delta time</param>
    public void ManagerUpdate(float dt)
    {
        if(distFromEnemySpawn > enemyManager.armyFront + attackRange)
        {
            float moveDist = moveSpeed * dt;

            transform.position += new Vector3(moveDist * moveDir, 0f, 0f);
            distFromSpawn += moveDist;
            distFromEnemySpawn -= moveDist;

            if(attackAnimator)
                attackAnimator.Stop();
        }
        else
        {
            if(attackAnimator)
                attackAnimator.Play();
        }
    }

    /// <summary>
    /// Causes the ArmyUnit to take damage and die if hp falls to or below 0
    /// </summary>
    /// <param name="amt">Quantity of damage to take, or negative value to heal</param>
    public void TakeDamage(float amt)
    {
        hp -= amt;

        if (hp <= 0)
        {
            attackAnimator.Stop();
            allyManager.KillUnit(this);
        }
        else if (hp > maxHP)
            hp = maxHP;
    }

}
