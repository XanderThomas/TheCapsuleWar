using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A DamagableAbstract component which will cause the game to end when it dies
/// </summary>
public class DamagableGoal : DamagableAbstract {

    [SerializeField, Tooltip("Is this the player's goal?")]
    private bool playerGoal;
    [SerializeField, Tooltip("A link to the scene's GameOverManager")]
    private GameOverManager gameOverManager;



    /// <summary>
    /// Reduces hp by amt and clamps hp from 0 to maxHP, and causes game to end if hp==0
    /// </summary>
    /// <param name="amt">Quantity of damage to take, or negative value to heal</param>
    public override void TakeDamage(float amt)
    {
        base.TakeDamage(amt);
        
        if (hp == 0)
            gameOverManager.EndGame(!playerGoal);
    }

}
