using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Default Turret. Shots one bullet, no special effect.
/// </summary>
public class TurretSingle : Turret
{





    void AttackOpponents()
    {
        if (timeTillNextShot >= -0.1f)
            timeTillNextShot -= Time.deltaTime;

        if (timeTillNextShot < 0f)
        {
            foreach(Transform child in enemyContainer.transform)
            {
                if((transform.position-child.position).magnitude < range)
                {
                    //TODO: Damage
                    timeTillNextShot += 1f / fireRate;
                    return;
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
