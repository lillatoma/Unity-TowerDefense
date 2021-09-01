using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// An object that represents an enemy ingame
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("Properties")]
    public string enemyName;
    public int health;
    public int totalHealth;
    public float speed;
    public float timeLeftFrozen = 0f;
    public float freezeImmunityTime = 0f;
    public float moveTime = 0f;
    public bool immune_to_freeze = false;
    protected float timeSinceLastHit = 0f;
    private Vector2Int lastHitOriginCoords;

    [Header("Object references")]
    public EnemyPath path;
    public GameObject healthBarBg;
    public GameObject healthBar;
    public GameObject deadModel;

    /// <summary>
    /// This function changes the color modulation of the sprite to a lightblueish color
    /// </summary>
    public void DrawFreeze()
    {
        if (timeLeftFrozen > 0f)
            GetComponent<SpriteRenderer>().color = new Color(0.35f, 0.7f, 1f);
    }
    /// <summary>
    /// In a .25 second timewindow, it changes the color modulation to a redish color which fades over time
    /// </summary>
    public void DrawDamage()
    {
        float o = 0.5f + timeSinceLastHit * 4f;
        if (timeSinceLastHit > 0.25f)
            o = 1f;

        GetComponent<SpriteRenderer>().color = new Color(0.5f*(1f+o), 0.5f*(1f+o), 1f);
    }

    /// <summary>
    /// Gains money for the player 
    /// Adds an elimination to the player and the turret
    /// Creates a new dead model
    /// </summary>
    public void OnDeath()
    {
        GameInfoHolder gih = FindObjectOfType<GameInfoHolder>();
        gih.statHolder.moneyGained += gih.currentLevelHolder.enemyMoney;
        gih.statHolder.playerMoney += gih.currentLevelHolder.enemyMoney;
        gih.statHolder.eliminatedOpponents++;
        gih.mapCreator.Blocks[lastHitOriginCoords.x, lastHitOriginCoords.y].transform.GetChild(0).GetComponent<Turret>().eliminations++;

        GameObject dead = GameObject.Instantiate(deadModel);
        dead.transform.parent = GameObject.FindGameObjectWithTag("DeadEnemies").transform;
        dead.transform.position = transform.position;
        dead.transform.rotation = transform.rotation;
        dead.transform.localScale = transform.localScale;
        Destroy(this.gameObject);
    }

    /// <summary>
    /// If the object is freezeable, it increases the freezetime
    /// </summary>
    /// <param name="time"></param>
    public void Freeze(float time)
    {
        if (immune_to_freeze || freezeImmunityTime > 0f)
            return;
        timeLeftFrozen += time;
    }
    /// <summary>
    /// Deals damage and checks for death
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="originCoords"></param>
    public void Damage(int dmg, Vector2Int originCoords)
    {
        health -= dmg;

        timeSinceLastHit = 0f;
        lastHitOriginCoords = originCoords;

        if (health <= 0)
            OnDeath(); 
    }

    /// <summary>
    /// Returns the accurate position on the current path according to the time moved
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPosition()
    {
        float distTraveled = moveTime * speed;

        for(int i = 0; i < path.pathPoints.Count - 1; i++)
        {
            float pointDist = (path.pathPoints[i + 1] - path.pathPoints[i]).magnitude;

            if(distTraveled < pointDist)
            {
                float scale = distTraveled / pointDist;
                float x = (float)path.pathPoints[i + 1].x * scale + (float)path.pathPoints[i].x * (1f-scale);
                float y = (float)path.pathPoints[i + 1].y * scale + (float)path.pathPoints[i].y * (1f-scale);
                float z = -0.5f;
                return new Vector3(x, y, z);
            }
            distTraveled -= pointDist;
        }
        //The enemy has passed all the pathpoints
        //
        moveTime = 0f;
        GameInfoHolder gih = FindObjectOfType<GameInfoHolder>();
        gih.statHolder.livesLeft--;
        return GetPosition();
    }
    /// <summary>
    /// Returns the accurate moveAngle on the current path according to the time moved
    /// </summary>
    /// <returns></returns>
    public float GetDirectionAngle()
    {
        float distTraveled = moveTime * speed;

        for (int i = 0; i < path.pathPoints.Count - 1; i++)
        {
            float pointDist = (path.pathPoints[i + 1] - path.pathPoints[i]).magnitude;

            if (distTraveled < pointDist)
            {
                float angle = Utils.RealVector2Angle(path.pathPoints[i+1] - path.pathPoints[i]);
                return angle;
            }
            distTraveled -= pointDist;
        }
        moveTime = 0f;
        GameInfoHolder gih = FindObjectOfType<GameInfoHolder>();
        gih.statHolder.livesLeft--;
        return GetDirectionAngle();
    }
    /// <summary>
    /// Returns true if there is no freeze immunity and the enemy is not frozen and is not immnue to freeze  
    /// </summary>
    /// <returns></returns>
    public bool IsFreezeable()
    {
        return freezeImmunityTime <= 0f && timeLeftFrozen <= 0f && !immune_to_freeze;
    }
    /// <summary>
    /// Updates the health bar to accurately show how much health is left
    /// </summary>
    public void UpdateHealthBar()
    {
        float percent = (float)health / (float)totalHealth;

        healthBarBg.transform.rotation = Quaternion.Euler(0, 0, 0);
        healthBarBg.transform.position = transform.position + new Vector3(0f, -0.55f, -0.4f);
        healthBar.transform.localScale = new Vector3(0.8f * percent, 0.1f, 1f);
        healthBar.transform.rotation = Quaternion.Euler(0, 0, 0);
        healthBar.transform.position = transform.position + new Vector3(0f - 0.4f * (1f-percent)*transform.localScale.x, -0.55f, -0.5f);



        float r = 1;
        float g = 1;
        byte b = 0;

        if(percent > 0.5f)
        {
            r = (1f - (percent - 0.5f) * 2f);
        }
        else if (percent >= 0f)
        {
            g = (percent * 2f);
        }


        healthBar.GetComponent<SpriteRenderer>().color = new Color(r, g, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //Doing the movement
        if (timeLeftFrozen > 0f)
        {
            timeLeftFrozen -= Time.deltaTime;
            if (timeLeftFrozen <= 0f)
                freezeImmunityTime = 0.1f;
        }
        else moveTime += Time.deltaTime;

        //Doing timers
        timeSinceLastHit += Time.deltaTime;
        freezeImmunityTime -= Time.deltaTime;

        //Setting position and rotation
        transform.position = GetPosition() + new Vector3(-16.5f,-15.5f,0);
        transform.rotation = Quaternion.Euler(0, 0, GetDirectionAngle());

        UpdateHealthBar();
        DrawDamage();
        DrawFreeze();
    }
}
