using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevelHolder : MonoBehaviour
{
    public GameInfoHolder gameInfoHolder;

    public int level;
    public int remainingEnemies;
    public int totalEnemies;
    public int enemyIndex;
    public int enemyHealth;
    public float enemySpeed;
    public int enemyMoney;

    public void GenerateLevel()
    {
        int skillPoints = level;

        int baseHealth = 20;
        float baseSpeed = 2f;
        int baseAmount = 10;

        for(int i = 0; i < level; i++)
        {
            baseHealth += 2;
            baseSpeed += .025f;
            baseHealth = (int)(1.01f * baseHealth);
        }

        int healthPoints = Random.Range(0, skillPoints);
        skillPoints -= healthPoints;

        int speedPoints = Random.Range(0, skillPoints);
        skillPoints -= speedPoints;

        int amountPoints = Random.Range(0, Mathf.Min(30, skillPoints));
        skillPoints -= amountPoints;

        int rval = Random.Range(0, skillPoints);
        speedPoints += rval;
        healthPoints += (skillPoints - rval);
        
        for(int i = 0; i < healthPoints; i++)
        {
            baseHealth += 2;
            baseHealth = (int)(1.005f * baseHealth);
        }

        for(int i = 0; i < speedPoints; i++)
        {
            baseSpeed += 0.05f;
            baseSpeed *= 1.01f;
        }

        baseAmount += 3 * amountPoints;

        totalEnemies = baseAmount;
        enemyHealth = baseHealth;
        enemySpeed = baseSpeed;
        remainingEnemies = totalEnemies;

        int baseMoney = 10;

        for(int i = 0; i < speedPoints + healthPoints;i++)
        {
            baseMoney += 1;
            baseMoney = (int)(1.03f * baseMoney);
        }

        enemyMoney = baseMoney;

        enemyIndex = gameInfoHolder.enemyInfoHolder.RandomIndex();


    }

    public void AdvanceLevel()
    {
        level++;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
