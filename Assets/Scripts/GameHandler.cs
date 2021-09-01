using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public void QuitApp()
    {
        Application.Quit();
    }

    public void LoadScene(string Scene)
    {
        SceneManager.LoadScene(Scene); 
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
