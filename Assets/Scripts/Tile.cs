using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int indexCoordinates;
    public bool isTurretSpace = false;
    public GameInfoHolder gameInfoHolder;
    public bool IsEmpty()
    {
        return transform.childCount == 0;
    }


    void SetupGameInfoHolder()
    {
        gameInfoHolder = GameObject.FindObjectOfType<GameInfoHolder>();
    }

    public void PlaceTurret()
    {
        if (gameInfoHolder == null)
            SetupGameInfoHolder();

        int selected = gameInfoHolder.selectionHolder.SelectedTurretInMenu;
        if (selected == -1)
            return;

        if (!IsEmpty() || !isTurretSpace)
            return;


        if (gameInfoHolder.statHolder.playerMoney >= gameInfoHolder.turretInfoHolder.turrets[selected].GetComponent<Turret>().basePrice)
        {
            gameInfoHolder.statHolder.playerMoney -= gameInfoHolder.turretInfoHolder.turrets[selected].GetComponent<Turret>().basePrice;

            GameObject gO = GameObject.Instantiate(gameInfoHolder.turretInfoHolder.turrets[selected]);
            gO.transform.parent = transform;
            gO.transform.position = transform.position + new Vector3(0, 0, -0.1f);


            if (gameInfoHolder.statHolder.playerMoney > gameInfoHolder.turretInfoHolder.turrets[selected].GetComponent<Turret>().basePrice)
                gameInfoHolder.selectionHolder.SelectedTurretInMenu = -1;
        }

    }
}
