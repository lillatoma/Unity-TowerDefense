using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int indexCoordinates;
    public bool isTurretSpace = false;
    public GameInfoHolder gameInfoHolder;
    //Returns if the gameObject has no children
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
        if (selected == -1) //If the selection is empty, turret can't be placed
            return;

        if (!IsEmpty() || !isTurretSpace) //If there is a turret already, or not a turret space, a turret can't be placed
            return;

        //If the player has enough money
        if (gameInfoHolder.statHolder.playerMoney >= gameInfoHolder.turretInfoHolder.turrets[selected].GetComponent<Turret>().basePrice)
        {
            //Money is taken
            gameInfoHolder.statHolder.playerMoney -= gameInfoHolder.turretInfoHolder.turrets[selected].GetComponent<Turret>().basePrice;

            //Turret is placed
            GameObject gO = GameObject.Instantiate(gameInfoHolder.turretInfoHolder.turrets[selected]);
            gO.transform.parent = transform;
            gO.transform.position = transform.position + new Vector3(0, 0, -0.1f);

            //In case there is no more money, the selected turret to place gets invalid
            if (gameInfoHolder.statHolder.playerMoney < gameInfoHolder.turretInfoHolder.turrets[selected].GetComponent<Turret>().basePrice)
                gameInfoHolder.selectionHolder.SelectedTurretInMenu = -1;
        }

    }
}
