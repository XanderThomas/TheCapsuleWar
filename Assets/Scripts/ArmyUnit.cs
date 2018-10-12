using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyUnit : MonoBehaviour {

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float attackRange;

    [HideInInspector]
    public ArmyManager allyManager;
    [HideInInspector]
    public ArmyManager enemyManager;

    public float distFromSpawn { get; private set; }
    public float distFromEnemySpawn { get; private set; }

    private float moveDir;



    private void Start()
    {
        distFromSpawn = 0f;

        Vector3 pos = transform.position;
        if(enemyManager.unitStartX > pos.x)
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
        }
    }

}
