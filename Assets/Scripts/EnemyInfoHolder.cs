using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoHolder : MonoBehaviour
{
    public GameObject[] enemies;

    public int RandomIndex()
    {
        return Random.Range(0, enemies.Length - 1);
    }
}

