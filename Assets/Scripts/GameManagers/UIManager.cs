using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [Header("Data sources")]

    [SerializeField]
    private ResourceManager playerResourceManager;

    [Header("Settings")]

    [SerializeField]
    private string playerWinText;
    [SerializeField]
    private string enemyWinText;

    [Header("UI elements")]

    [SerializeField]
    private Text goldText;
    [SerializeField]
    private RectTransform goldBar;
    [SerializeField]
    private RectTransform goldBarHolder;

    [SerializeField]
    private Text woodText;
    [SerializeField]
    private RectTransform woodBar;
    [SerializeField]
    private RectTransform woodBarHolder;

    [SerializeField]
    private Text ironText;
    [SerializeField]
    private RectTransform ironBar;
    [SerializeField]
    private RectTransform ironBarHolder;

    [SerializeField]
    private Text gameOverText;



    private void Awake()
    {
        gameOverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        ResourceTypes resources = playerResourceManager.Resources;
        ResourceTypes resourceCapacity = playerResourceManager.ResourceCapacity;

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

    public void DisplayGameOver(bool playerWins)
    {
        gameOverText.gameObject.SetActive(true);

        if (playerWins)
            gameOverText.text = playerWinText;
        else
            gameOverText.text = enemyWinText;
    }

}
