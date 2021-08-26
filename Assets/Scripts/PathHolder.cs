using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPath
{
    public List<Vector2Int> pathPoints;
}

public class PathHolder : MonoBehaviour
{



    [Header("The paths")]
    public int testValue;
    public EnemyPath[] paths;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
