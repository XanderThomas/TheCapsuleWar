using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableGoal : DamagableAbstract {

    [SerializeField]
    private bool playerGoal;
    [SerializeField]
    private GameOverManager gameOverManager;



    public override void TakeDamage(float amt)
    {
        base.TakeDamage(amt);

        if (hp == 0)
            gameOverManager.EndGame(!playerGoal);
    }

}
