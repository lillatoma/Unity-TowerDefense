using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    GameInfoHolder gameInfoHolder;
    // Start is called before the first frame update
    void Start()
    {
        gameInfoHolder = FindObjectOfType<GameInfoHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            gameInfoHolder.selectionHolder.SelectedTurretInMenu = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            gameInfoHolder.selectionHolder.SelectedTurretInMenu = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            gameInfoHolder.selectionHolder.SelectedTurretInMenu = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            gameInfoHolder.selectionHolder.SelectedTurretInMenu = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            gameInfoHolder.selectionHolder.SelectedTurretInMenu = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            gameInfoHolder.selectionHolder.SelectedTurretInMenu = 5;
        else if (Input.GetKeyDown(KeyCode.Alpha0))
            gameInfoHolder.selectionHolder.SelectedTurretInMenu = -1;
    }
}
