using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Alignment : MonoBehaviour
{
    [Header("Size")]
    public int leftBarSize = 4;
    [Header("Screen objects")]
    public GameObject leftBar;
    public GameObject map;
    public GameObject rightBar;

    void Update()
    {
        // This sets the left bar correctly
        leftBar.transform.position = new Vector3(0,0,-1);
        leftBar.GetComponent<LeftBar>().UpdateBgPanel(leftBarSize);


        // This sets the map correctly
        // 13 is a value that seemed an appropriate value in order for the (-15.5,y) world block to be on the left side
        map.transform.position = new Vector3(leftBarSize - 13, 0, 0);

        // This sets the right bar correctly
        rightBar.transform.position = new Vector3(0, 0, -1);
        rightBar.GetComponent<RightBar>().UpdateBgPanel(leftBarSize + 32);
    }


}
