using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyUnit : MonoBehaviour {

    [SerializeField]
    private float maxHP;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private ProgAnimatorAbstract attackAnimator;
    [SerializeField]
    private WeaponAbstract weapon;

    [HideInInspector]
    public ArmyManager allyManager;
    [HideInInspector]
    public ArmyManager enemyManager;

    public float distFromSpawn { get; private set; }
    public float distFromEnemySpawn { get; private set; }

    private float moveDir;
    private float hp;



    public void Initialize()
    {
        distFromSpawn = 0f;
        hp = maxHP;

        weapon.targetTag = enemyManager.TeamUnitTag;

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

    public void TakeDamage(float amt)
    {
        hp -= amt;

        if (hp <= 0)
        {
            attackAnimator.Stop();
            allyManager.KillUnit(this);
        }
    }

}
