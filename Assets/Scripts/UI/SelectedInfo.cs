using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedInfo : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Text elimText;
    public Text sellText;

    private GameInfoHolder gameInfoHolder;

    void DisableChildren()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void EnableChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    Turret GetSelected()
    {
        Vector2Int se = gameInfoHolder.selectionHolder.SelectedTurretOnMap;
        return gameInfoHolder.mapCreator.Blocks[se.x, se.y].transform.GetChild(0).GetComponent<Turret>();
    }
    void UpdateTexts()
    {
        nameText.text = GetSelected().turretName;
        levelText.text = "Level " + GetSelected().totalUpgrades;
        elimText.text = "";
        sellText.text = "$" + GetSelected().CalculateSellPrice();
    }

    public void DoSell()
    {
        gameInfoHolder.statHolder.playerMoney += GetSelected().CalculateSellPrice();
        gameInfoHolder.statHolder.moneyGained += GetSelected().CalculateSellPrice();
        Destroy(GetSelected().gameObject);
        gameInfoHolder.selectionHolder.SelectedTurretOnMap = new Vector2Int(-1, -1);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameInfoHolder = FindObjectOfType<GameInfoHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameInfoHolder.selectionHolder.SelectedTurretOnMap == new Vector2Int(-1, -1))
            DisableChildren();
        else
        {
            EnableChildren();
            UpdateTexts();
        } 
            
    }
}
