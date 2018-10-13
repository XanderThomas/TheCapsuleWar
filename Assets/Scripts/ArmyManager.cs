using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour {

    [SerializeField]
    private Transform spawn;
    [SerializeField]
    private GameObject tempTestUnitPrefab;
    [SerializeField]
    private ArmyManager enemyArmyManager;
    [SerializeField]
    private float spawnZRange;
    [SerializeField]
    private string enemyTeamUnitTag;

    //armyFront is distance from spawn, not a global space position
    public float armyFront { get; private set; }
    //unitStartX is a global space position
    public float unitStartX { get; private set; }
    public string EnemyTeamUnitTag { get { return enemyTeamUnitTag; } }

    private List<ArmyUnit> units = new List<ArmyUnit>();





    private void Awake()
    {
        unitStartX = spawn.position.x;

        InvokeRepeating("TEMP_SpawnUnit", 0.1f, 2f);
    }

    private void TEMP_SpawnUnit()
    {
        GameObject unit = Instantiate(tempTestUnitPrefab);
        Vector3 spawnPos = spawn.position;
        spawnPos.z += Random.Range(-spawnZRange, spawnZRange);
        unit.transform.position = spawnPos;
        unit.transform.rotation = spawn.rotation;

        ArmyUnit script = unit.GetComponent<ArmyUnit>();
        script.allyManager = this;
        script.enemyManager = enemyArmyManager;

        units.Add(script);
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        float furthestUnitDist = 0f;

        for(int i = 0; i < units.Count; ++i)
        {
            units[i].ManagerUpdate(dt);
            if (units[i].distFromSpawn > furthestUnitDist)
                furthestUnitDist = units[i].distFromSpawn;
        }

        armyFront = furthestUnitDist;
    }

}
