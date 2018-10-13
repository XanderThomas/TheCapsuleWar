﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    [SerializeField, Tooltip("The current amount of resources stored")]
    private ResourceTypes resources;
    [SerializeField, Tooltip("The maximum amount of resources stored")]
    private ResourceTypes resourceCapacity;
    [SerializeField, Tooltip("The amount of each resource generated every interval")]
    private ResourceTypes productionRate;
    [SerializeField, Tooltip("The time in seconds between resource increases\nRecommend leave at 1")]
    private float productionInterval = 1f;



    private void Awake()
    {
        InvokeRepeating("ProduceResources", productionInterval, productionInterval);
    }

    private void ProduceResources()
    {
        AlterResourceStore(productionRate);
    }

    private void AlterResourceStore(ResourceTypes amt)
    {
        resources += amt;

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