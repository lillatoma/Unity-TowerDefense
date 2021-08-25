using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script positions the world object to the middle of a unit-square
/// </summary>
[ExecuteInEditMode]
public class WorldObjectMid : MonoBehaviour
{
    /// <summary>
    /// Sets makes the fraction part of .x and .y of the transform to be 0.5
    /// </summary>
    public void SetPosToIntMiddle()
    {
        //Saving the original position
        Vector3 oP = transform.position;
        //This makes the fraction part of .x and .y to be 0.5
        transform.position = new Vector3(Mathf.Floor(oP.x) + 0.5f, Mathf.Floor(oP.y) + 0.5f, oP.z);
    }
    // Update is called once per frame
    void Update()
    {
        SetPosToIntMiddle();
    }
}
