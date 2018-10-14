using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    [SerializeField]
    private UIManager uiManager;



	public void EndGame(bool playerWins)
    {
        uiManager.DisplayGameOver(playerWins);
        //Eventually this will also return the player to the menu or whatever comes next
    }

}
