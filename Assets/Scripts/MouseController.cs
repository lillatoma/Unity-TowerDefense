using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    GameInfoHolder gameInfoHolder;
    public WorldObjectMid mouseTile;
    public GameObject rangeVisCircle;


    private void Start()
    {
        gameInfoHolder = FindObjectOfType<GameInfoHolder>();
    }

    void RangeVis()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit)
        {
            if (hit.transform.gameObject.GetComponent<Tile>())
            {
                Tile tile = hit.transform.gameObject.GetComponent<Tile>();
                if (!tile.IsEmpty())
                {
                    if (tile.transform.GetChild(0).GetComponent<Turret>())
                    {
                        Turret turret = tile.transform.GetChild(0).GetComponent<Turret>();
                        float range = turret.range;
                        Vector2 position = turret.transform.position;
                        rangeVisCircle.transform.localScale = new Vector3(range * 2f, range * 2f, range * 2f);
                        rangeVisCircle.transform.position = new Vector3(position.x, position.y, -0.5f);
                        return;
                    }
                }
                else if (gameInfoHolder.selectionHolder.SelectedTurretInMenu != -1)
                {
                    Turret turret = gameInfoHolder.turretInfoHolder.turrets[gameInfoHolder.selectionHolder.SelectedTurretInMenu].GetComponent<Turret>();
                    float range = turret.range;
                    Vector2 position = tile.transform.position;
                    rangeVisCircle.transform.localScale = new Vector3(range * 2f, range * 2f, range * 2f);
                    rangeVisCircle.transform.position = new Vector3(position.x, position.y, -0.5f);
                    return;
                }
            }
        }

        if (gameInfoHolder.selectionHolder.SelectedTurretOnMap != new Vector2Int(-1,-1))
        {
            Vector2Int c = gameInfoHolder.selectionHolder.SelectedTurretOnMap;
            Turret turret = gameInfoHolder.mapCreator.Blocks[c.x, c.y].transform.GetChild(0).GetComponent<Turret>();
            float range = turret.range;
            Vector2 position = turret.transform.position;
            rangeVisCircle.transform.localScale = new Vector3(range * 2f, range * 2f, range * 2f);
            rangeVisCircle.transform.position = new Vector3(position.x, position.y, -0.5f);
            return;
        }

        rangeVisCircle.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        rangeVisCircle.transform.position = new Vector3(1000, 1000, 1000);
    }
    

    void UpdateMouseTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = mousePos + new Vector3(0, 0, -mousePos.z - 0.5f);
        mouseTile.transform.position = mousePos;
        mouseTile.SetPosToIntMiddle();
    }

    void ControlLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit)
            {
                if (hit.transform.gameObject.GetComponent<Tile>())
                {
                    Tile tile = hit.transform.gameObject.GetComponent<Tile>();
                    if (tile.IsEmpty())
                    {
                        gameInfoHolder.selectionHolder.SelectedTurretOnMap = new Vector2Int(-1, -1);
                        tile.PlaceTurret();
                    }
                    else
                    {
                        gameInfoHolder.selectionHolder.SelectedTurretOnMap = tile.indexCoordinates;
                        gameInfoHolder.selectionHolder.SelectedTurretInMenu = -1;
                    }
                }
                //else if (hit.transform.gameObject.tag == "UIElement")
                    //gameInfoHolder.selectionHolder.SelectedTurretOnMap = new Vector2Int(-1, -1);

            }
            else
            {
                gameInfoHolder.selectionHolder.SelectedTurretInMenu = -1;
                gameInfoHolder.selectionHolder.SelectedTurretOnMap = new Vector2Int(-1, -1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ControlLeftClick();
        UpdateMouseTile();
        RangeVis();
    }
}
