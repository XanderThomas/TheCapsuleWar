using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour {

    [SerializeField]
    private ArmyManager armyManager;
    [SerializeField]
    private ResourceManager resourceManager;
    [SerializeField]
    private ArmyManager enemyArmyManager;



    private void Update()
    {
        armyManager.BuyUnit(0);
    }

}
