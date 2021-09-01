using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class YouScoredText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = string.Format("You scored {0}.\nYour highest score is {1}.", PlayerPrefs.GetInt("CurrentPoints", 0), PlayerPrefs.GetInt("Highscore", 0));
    }
}
