using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPath
{
    public List<Vector2> pathPoints;
}



public class PathHolder : MonoBehaviour
{



    [Header("The paths")]
    public EnemyPath[] paths;
    public EnemyPath bossPath;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
