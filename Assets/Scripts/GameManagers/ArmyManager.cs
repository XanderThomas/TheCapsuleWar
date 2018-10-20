using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a team's current units & the list of units it can spawn
/// </summary>
[RequireComponent(typeof(ResourceManager))]
public class ArmyManager : MonoBehaviour {

    //Struct for a unit spawn type & cost
    [System.Serializable]
    public struct ArmySpawn
    {
        public ResourceTypes cost;
        public GameObject prefab;
    }

    [SerializeField, Tooltip("A list of all the unit types this army can spawn")]
    private ArmySpawn[] spawnableUnits;
    [SerializeField, Tooltip("The position & rotation spawned units will start at")]
    private Transform spawn;
    [SerializeField, Tooltip("The ArmyManager for the enemy team")]
    private ArmyManager enemyArmyManager;
    [SerializeField, Tooltip("The length of the line alone which units will be spawned")]
    private float spawnZRange;
    [SerializeField, Tooltip("The tag given to spawned units' GameObjects")]
    private string teamUnitTag;
    [SerializeField, Tooltip("This team's ResourceManager")]
    private ResourceManager resourceManager;

    //armyFront is distance from spawn, not a global space position
    /// <summary>
    /// The furthest absolute Z-axis distance any unit of this army currently is from spawn
    /// </summary>
    public float armyFront { get; private set; }
    /// <summary>
    /// The global X position units are spawned at
    /// </summary>
    public float unitStartX { get; private set; }
    /// <summary>
    /// The tag given to spawned units' GameObjects
    /// </summary>
    public string TeamUnitTag { get { return teamUnitTag; } }
    
    private List<ArmyUnit> units = new List<ArmyUnit>();



    

    private void Awake()
    {
        //Set the publicly accessible property for unit starting position
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

        //Update all units & determine the distance of the furthest from spawn
        for(int i = 0; i < units.Count; ++i)
        {
            units[i].ManagerUpdate(dt);
            if (units[i].distFromSpawn > furthestUnitDist)
                furthestUnitDist = units[i].distFromSpawn;
        }
        
        armyFront = furthestUnitDist;
    }

    /// <summary>
    /// Remove unit from internal list & destroy its gameobject
    /// </summary>
    /// <param name="unit"></param>
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

    /// <summary>
    /// Attempt to spawn unit & spend appropriate resources
    /// </summary>
    /// <param name="idx">Index of the unit to spawn in spawnableUnits array</param>
    /// <returns></returns>
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

    /// <summary>
    /// Create and initialize a unit
    /// </summary>
    /// <param name="idx">Index of the unit to spawn in spawnableUnits array</param>
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
