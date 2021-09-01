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

    /// <summary>
    /// Disables every children of the current gameobject
    /// </summary>
    void DisableChildren()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Enables every children of the current gameobject
    /// </summary>
    void EnableChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// Returns the currently selected turret
    /// Warning!! Can return wrong data or lead to unexpected behaviour if the turret selection is invalid
    /// </summary>
    /// <returns></returns>
    public Turret GetSelectedTurret()
    {
        Vector2Int se = gameInfoHolder.selectionHolder.SelectedTurretOnMap;
        return gameInfoHolder.mapCreator.Blocks[se.x, se.y].transform.GetChild(0).GetComponent<Turret>();
    }
    /// <summary>
    /// Changes the texts of the texts
    /// </summary>
    void UpdateTexts()
    {
        nameText.text = GetSelectedTurret().turretName;
        levelText.text = "Level " + GetSelectedTurret().totalUpgrades;
        elimText.text = "";
        sellText.text = "$" + GetSelectedTurret().CalculateSellPrice();
    }

    /// <summary>
    /// Sells the currently selected turret, and changes the selection to invalid
    /// </summary>
    public void DoSell()
    {
        gameInfoHolder.statHolder.playerMoney += GetSelectedTurret().CalculateSellPrice();
        gameInfoHolder.statHolder.moneyGained += GetSelectedTurret().CalculateSellPrice();
        Destroy(GetSelectedTurret().gameObject);
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
