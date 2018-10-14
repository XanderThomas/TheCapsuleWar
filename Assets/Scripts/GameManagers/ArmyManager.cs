using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceManager))]
public class ArmyManager : MonoBehaviour {

    [System.Serializable]
    public struct ArmySpawn
    {
        public ResourceTypes cost;
        public GameObject prefab;
    }

    [SerializeField]
    private ArmySpawn[] spawnableUnits;
    [SerializeField]
    private Transform spawn;
    [SerializeField]
    private GameObject tempTestUnitPrefab;
    [SerializeField]
    private ArmyManager enemyArmyManager;
    [SerializeField]
    private float spawnZRange;
    [SerializeField]
    private string teamUnitTag;
    [SerializeField]
    private ResourceManager resourceManager;

    //armyFront is distance from spawn, not a global space position
    public float armyFront { get; private set; }
    //unitStartX is a global space position
    public float unitStartX { get; private set; }
    public string TeamUnitTag { get { return teamUnitTag; } }

    private List<ArmyUnit> units = new List<ArmyUnit>();



    

    private void Awake()
    {
        unitStartX = spawn.position.x;

#if DEBUG
        if (!resourceManager)
            Debug.LogError("ArmyManager " + gameObject.name + " resourceManager not set!");
#endif
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

    public void KillUnit(ArmyUnit unit)
    {
        units.Remove(unit);
        Destroy(unit.gameObject);
    }

    /// <summary>
    /// BuyUnit but without a return so it's selectable in the Unity Inspector
    /// </summary>
    /// <param name="idx">Index of unit in spawnableUnits array</param>
    public void BuyUnitUI(int idx)
    {
        BuyUnit(idx);
    }

    public bool BuyUnit(int idx)
    {
        if (resourceManager.SpendResources(spawnableUnits[idx].cost))
        {
            SpawnUnit(idx);

            return true;
        }
        else
            return false;
    }

    private void SpawnUnit(int idx)
    {
        GameObject unit = Instantiate(spawnableUnits[idx].prefab);

        Vector3 spawnPos = spawn.position;
        spawnPos.z += Random.Range(-spawnZRange, spawnZRange);

        unit.transform.position = spawnPos;
        unit.transform.rotation = spawn.rotation;
        unit.tag = teamUnitTag;

        ArmyUnit script = unit.GetComponent<ArmyUnit>();
        script.allyManager = this;
        script.enemyManager = enemyArmyManager;

        script.Initialize();

        units.Add(script);
    }

}
