using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int totalHealth;
    public float speed;
    public float timeLeftFrozen = 0f;
    public float moveTime = 0f;

    public EnemyPath path;


    public void OnDeath()
    {
        GameInfoHolder gih = FindObjectOfType<GameInfoHolder>();
        gih.statHolder.moneyGained += gih.currentLevelHolder.enemyMoney;
        gih.statHolder.playerMoney += gih.currentLevelHolder.enemyMoney;
        gih.statHolder.eliminatedOpponents++;
        Destroy(this.gameObject);
    }
    public void Damage(int dmg)
    {
        health -= dmg;
        if (health < 0)
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
        return new Vector3(0, 0, -0.5f);
    }

    public float GetDirectionAngle()
    {
        float distTraveled = moveTime * speed;

        for (int i = 0; i < path.pathPoints.Count - 1; i++)
        {
            float pointDist = (path.pathPoints[i - 1] - path.pathPoints[i]).magnitude;

            if (distTraveled < pointDist)
            {
                float angle = Utils.RealVector2Angle(path.pathPoints[i] - path.pathPoints[i - 1]);
                return angle;
            }
            distTraveled -= pointDist;
        }
        return 0f;
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
        transform.position = GetPosition() + new Vector3(-16.5f,-15.5f,0);
        transform.rotation = Quaternion.Euler(0, 0, GetDirectionAngle());
    }
}
