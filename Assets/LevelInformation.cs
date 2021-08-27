using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelInformation : MonoBehaviour
{
    public Text nameText;
    public Text hpText;
    public Text speedText;
    public Text moneyText;
    public Text amountText;

    private GameInfoHolder gameInfoHolder;
    private GameObject enemyContainer;
    // Start is called before the first frame update
    void Start()
    {
        gameInfoHolder = FindObjectOfType<GameInfoHolder>();
        enemyContainer = GameObject.FindGameObjectWithTag("EnemyContainer");
    }

    void DisableChildren()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }

    void EnableChildren()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }

    void UpdateTexts()
    {
        nameText.text = gameInfoHolder.enemyInfoHolder.enemies[gameInfoHolder.currentLevelHolder.enemyIndex].GetComponent<Enemy>().enemyName;
        hpText.text = "HP: " + gameInfoHolder.currentLevelHolder.enemyHealth;
        speedText.text = "Speed: " + gameInfoHolder.currentLevelHolder.enemySpeed;
        moneyText.text = "Cash: $" + gameInfoHolder.currentLevelHolder.enemyMoney;
        amountText.text = "Count: " + enemyContainer.transform.childCount + " | " + gameInfoHolder.currentLevelHolder.remainingEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameInfoHolder.selectionHolder.SelectedTurretOnMap == new Vector2Int(-1, -1))
        {
            EnableChildren();
            UpdateTexts();
        }
        else
            DisableChildren();
    }
}
