using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for a dead enemy
/// Lasts only for 0.666 seconds, then destroys the gameobject
/// </summary>
public class EnemyDead : MonoBehaviour
{
    float time = 0f;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float a = 1f;
        if (time > 0.333f)
            a = 2f - 3f * time;

        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, a);

        if (time > 0.666f)
            Destroy(gameObject);
    }
}
