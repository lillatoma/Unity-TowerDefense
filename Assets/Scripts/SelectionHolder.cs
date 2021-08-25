using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHolder : MonoBehaviour
{
    /// <summary>
    /// -1 is invalid selection/no selected turret
    /// </summary>
    public int SelectedTurretInMenu = -1;
    /// <summary>
    /// (-1,-1) is invalid selection/no selected turret
    /// </summary>
    public Vector2Int SelectedTurretOnMap = new Vector2Int(-1, -1);
}
