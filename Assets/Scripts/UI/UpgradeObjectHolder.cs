using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeObjectHolder : MonoBehaviour
{
    public UpgradeObject[] upgradeObjects;

    private GameInfoHolder gameInfoHolder;



    // Start is called before the first frame update
    void Start()
    {
        gameInfoHolder = FindObjectOfType<GameInfoHolder>();

        for (int i = 0; i < upgradeObjects.Length; i++)
            upgradeObjects[i].GetComponent<UpgradeObject>().upgradeIndex = i;
    }
    Turret GetSelected()
    {
        Vector2Int se = gameInfoHolder.selectionHolder.SelectedTurretOnMap;
        return gameInfoHolder.mapCreator.Blocks[se.x, se.y].transform.GetChild(0).GetComponent<Turret>();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameInfoHolder.selectionHolder.SelectedTurretOnMap != new Vector2Int(-1, -1))
        {
            for (int i = 0; i < upgradeObjects.Length; i++)
            {
                if (i < GetSelected().noOfUpgrades)
                    upgradeObjects[i].gameObject.SetActive(true);
                else
                    upgradeObjects[i].gameObject.SetActive(false);

            }
        }
        else
            foreach (UpgradeObject child in upgradeObjects)
                child.gameObject.SetActive(false);
    }
}
