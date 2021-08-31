using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int health;
    public int totalHealth;
    public float speed;
    public float timeLeftFrozen = 0f;
    public float moveTime = 0f;
    protected float timeSinceLastHit = 0f;
    private Vector2Int lastHitOriginCoords;

    public EnemyPath path;
    public GameObject healthBarBg;
    public GameObject healthBar;

    public GameObject deadModel;

    public void DrawFreeze()
    {
        if (timeLeftFrozen > 0f)
            GetComponent<SpriteRenderer>().color = new Color(0.01f, 0.5f, 1f);
    }

    public void DrawDamage()
    {
        float o = 0.5f + timeSinceLastHit * 4f;
        if (timeSinceLastHit > 0.25f)
            o = 1f;

        GetComponent<SpriteRenderer>().color = new Color(0.5f*(1f+o), 0.5f*(1f+o), 1f);
    }

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

    public void Freeze(float time)
    {
        timeLeftFrozen += time;
    }

    public void Damage(int dmg, Vector2Int originCoords)
    {
        health -= dmg;

        timeSinceLastHit = 0f;
        lastHitOriginCoords = originCoords;

        if (health <= 0)
            OnDeath(); 
    }
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
        moveTime = 0f;
        GameInfoHolder gih = FindObjectOfType<GameInfoHolder>();
        gih.statHolder.livesLeft--;
        return GetPosition();
    }

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
            r = (byte)(1f - (percent - 0.5f) * 2f);
        }
        else if (percent >= 0f)
        {
            g = (byte)(percent * 2f);
        }


        healthBar.GetComponent<SpriteRenderer>().color = new Color(r, g, b);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeftFrozen > 0f)
        {
            timeLeftFrozen -= Time.deltaTime;
        }
        else moveTime += Time.deltaTime;

        timeSinceLastHit += Time.deltaTime;

        transform.position = GetPosition() + new Vector3(-16.5f,-15.5f,0);
        transform.rotation = Quaternion.Euler(0, 0, GetDirectionAngle());
        UpdateHealthBar();
        DrawDamage();
        DrawFreeze();
    }
}
