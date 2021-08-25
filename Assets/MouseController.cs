using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    GameInfoHolder gameInfoHolder;

    private void Start()
    {
        gameInfoHolder = FindObjectOfType<GameInfoHolder>();
    }

    // Update is called once per frame
    void Update()
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
                        tile.PlaceTurret();
                    else
                    {
                        gameInfoHolder.selectionHolder.SelectedTurretOnMap = tile.indexCoordinates;
                        gameInfoHolder.selectionHolder.SelectedTurretInMenu = -1;
                    }
                }
                else if (hit.transform.gameObject.tag == "UIElement")
                    gameInfoHolder.selectionHolder.SelectedTurretOnMap = new Vector2Int(-1, -1);

            }
            else
            {
                gameInfoHolder.selectionHolder.SelectedTurretInMenu = -1;
                gameInfoHolder.selectionHolder.SelectedTurretOnMap = new Vector2Int(-1, -1);
            }
        }
    }
}
