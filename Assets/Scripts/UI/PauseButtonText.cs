using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseButtonText : MonoBehaviour
{
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Changes the button text to "Stop autospawn"
        //If spawn is paused, then "Start autospawn"
        Text text = GetComponent<Text>();
        if (gameController.spawnPaused)
            text.text = "Start autospawn";
        else
            text.text = "Stop autospawn";
    }
}
