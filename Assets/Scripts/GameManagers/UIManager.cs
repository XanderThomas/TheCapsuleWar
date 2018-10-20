using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [Header("Data sources")]

    [SerializeField, Tooltip("The ResourceManager script for the player's resources")]
    private ResourceManager playerResourceManager;

    [Header("Settings")]

    [SerializeField, Tooltip("The text to display if the player wins")]
    private string playerWinText;
    [SerializeField, Tooltip("The text to display if the enemy wins")]
    private string enemyWinText;

    [Header("UI elements")]

    [SerializeField, Tooltip("This will display the amount of gold the player has as a number")]
    private Text goldText;
    [SerializeField, Tooltip("This needs to be an image whose transform is stretched to fill its parent. It will display the amount of gold the palyer has as a color bar")]
    private RectTransform goldBar;
    [SerializeField, Tooltip("This needs to be the parent RectTransform of Gold Bar above")]
    private RectTransform goldBarHolder;

    [SerializeField, Tooltip("This will display the amount of wood the player has as a number")]
    private Text woodText;
    [SerializeField, Tooltip("This needs to be an image whose transform is stretched to fill its parent. It will display the amount of wood the palyer has as a color bar")]
    private RectTransform woodBar;
    [SerializeField, Tooltip("This needs to be the parent RectTransform of Wood Bar above")]
    private RectTransform woodBarHolder;

    [SerializeField, Tooltip("This will display the amount of iron the player has as a number")]
    private Text ironText;
    [SerializeField, Tooltip("This needs to be an image whose transform is stretched to fill its parent. It will display the amount of iron the palyer has as a color bar")]
    private RectTransform ironBar;
    [SerializeField, Tooltip("This needs to be the parent RectTransform of Iron Bar above")]
    private RectTransform ironBarHolder;

    [SerializeField, Tooltip("This is the text component which will display Player or Enemy Win Text from above. Its gameobject will be inactive most of the game")]
    private Text gameOverText;



    private void Awake()
    {
        //Hide the game over text object at the start of the game
        gameOverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        //Get the player's current resource & resource capacity amounts
        ResourceTypes resources = playerResourceManager.Resources;
        ResourceTypes resourceCapacity = playerResourceManager.ResourceCapacity;
        
        //Display the player's current resources & resource capacity
        goldText.text = "Gold: " + resources.gold;
        goldBar.offsetMax = new Vector2(-goldBarHolder.sizeDelta.x +
            (resources.gold / (float)resourceCapacity.gold) * goldBarHolder.sizeDelta.x, 0f);

        woodText.text = "Wood: " + resources.wood;
        woodBar.offsetMax = new Vector2(-woodBarHolder.sizeDelta.x +
            (resources.wood / (float)resourceCapacity.wood) * woodBarHolder.sizeDelta.x, 0f);

        ironText.text = "Iron: " + resources.iron;
        ironBar.offsetMax = new Vector2(-ironBarHolder.sizeDelta.x +
            (resources.iron / (float)resourceCapacity.iron) * ironBarHolder.sizeDelta.x, 0f);
    }

    /// <summary>
    /// Cause the UIManager to display game over text
    /// </summary>
    /// <param name="playerWins">True if player won, false if enemy won</param>
    public void DisplayGameOver(bool playerWins)
    {
        gameOverText.gameObject.SetActive(true);

        if (playerWins)
            gameOverText.text = playerWinText;
        else
            gameOverText.text = enemyWinText;
    }

}
