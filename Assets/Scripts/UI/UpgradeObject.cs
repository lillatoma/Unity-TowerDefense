using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeObject : MonoBehaviour
{
    public int upgradeIndex;
    public Text nameText;
    public Text upText;
    public Button priceButton;
    public Text priceText;

    [Header("Colors")]
    public Color goodColor;
    public Color badColor;

    private GameInfoHolder gameInfoHolder;

    // Start is called before the first frame update
    void Start()
    {
        gameInfoHolder = FindObjectOfType<GameInfoHolder>();
    }
    Turret GetSelected()
    {
        Vector2Int se = gameInfoHolder.selectionHolder.SelectedTurretOnMap;
        return gameInfoHolder.mapCreator.Blocks[se.x, se.y].transform.GetChild(0).GetComponent<Turret>();
    }

    public void CommitUpgrade()
    {
        if (gameInfoHolder.selectionHolder.SelectedTurretOnMap != new Vector2Int(-1, -1))
        {
            GetSelected().Upgrade(upgradeIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameInfoHolder.selectionHolder.SelectedTurretOnMap != new Vector2Int(-1,-1))
        {
            nameText.text = GetSelected().upgrades[upgradeIndex].name;
            upText.text = string.Format("{0} -> {1}", GetSelected().upgrades[upgradeIndex].GetValuePrinted(), GetSelected().upgrades[upgradeIndex].GetNextValuePrinted());
            priceText.text = "$" + GetSelected().upgrades[upgradeIndex].upPrice;
            if (gameInfoHolder.statHolder.playerMoney >= GetSelected().upgrades[upgradeIndex].upPrice)
                priceButton.GetComponent<Image>().color = goodColor;
            else 
                priceButton.GetComponent<Image>().color = badColor;
        }
    }
}
