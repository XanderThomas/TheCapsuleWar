using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager for a team's resource stats
/// </summary>
public class ResourceManager : MonoBehaviour {

    [SerializeField, Tooltip("The current amount of resources stored")]
    private ResourceTypes resources;
    [SerializeField, Tooltip("The maximum amount of resources stored")]
    private ResourceTypes resourceCapacity;
    [SerializeField, Tooltip("The amount of each resource generated every interval")]
    private ResourceTypes productionRate;
    [SerializeField, Tooltip("The time in seconds between resource increases\nRecommend leave at 1")]
    private float productionInterval = 1f;

    //Public accessors for privately managed variables & structs
    public ResourceTypes Resources { get { return resources; } }
    public ResourceTypes ResourceCapacity { get { return resourceCapacity; } }
    public ResourceTypes ProductionRate { get { return productionRate; } }
    public float ProductionInterval { get { return productionInterval; } }



    private void Awake()
    {
        //Use InvokeRepeating to add resources regularly throughout game
        InvokeRepeating("ProduceResources", productionInterval, productionInterval);
    }

    /// <summary>
    /// Add resources equal to productionRate
    /// </summary>
    private void ProduceResources()
    {
        AlterResourceStore(productionRate);
    }

    /// <summary>
    /// Subtract resources if they are available
    /// </summary>
    /// <param name="amt">The amount of resources to attempt to subtract</param>
    /// <returns>True if successful, false if insufficient resources to spend</returns>
    public bool SpendResources(ResourceTypes amt)
    {
        //Check if there are enough resources to spend
        if (amt.gold > resources.gold
         || amt.wood > resources.wood
         || amt.iron > resources.iron)
            return false;
        else
        {
            //Remove resources equal to amt
            AlterResourceStore(-amt);
            return true;
        }
    }

    /// <summary>
    /// Add and/or remove resources and clamp all their values from 0 to the appropriate resourceCapacity value
    /// </summary>
    /// <param name="amt">The amount of resources to add and/or remove</param>
    private void AlterResourceStore(ResourceTypes amt)
    {
        //Add and/or remove resources
        resources += amt;

        //Clamp all resource values from 0 to the appropriate resourceCapacity value
        if (resources.gold > resourceCapacity.gold)
            resources.gold = resourceCapacity.gold;
        else if (resources.gold < 0)
            resources.gold = 0;

        if (resources.wood > resourceCapacity.wood)
            resources.wood = resourceCapacity.wood;
        else if (resources.wood < 0)
            resources.wood = 0;

        if (resources.iron > resourceCapacity.iron)
            resources.iron = resourceCapacity.iron;
        else if (resources.iron < 0)
            resources.iron = 0;
    }

}
