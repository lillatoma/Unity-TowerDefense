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

        if (level > 10)
            skillPoints += (level - 10);
        if (level > 25)
            skillPoints += (level - 25);
        if (level > 50)
            skillPoints += (level - 50);
        if (level > 75)
            skillPoints += (level - 75);
        if (level > 100)
            skillPoints += (level - 100);

        int baseHealth = 20;
        float baseSpeed = 2f;
        int baseAmount = 10;

        for(int i = 0; i < level; i++)
        {
            baseHealth += 4;
            baseSpeed += .075f;
            baseHealth = (int)(1.0125f * baseHealth);
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
            baseHealth += 10;
            baseHealth = (int)(1.015f * baseHealth);
        }

        for(int i = 0; i < speedPoints; i++)
        {
            baseSpeed += 0.1f;
            baseSpeed *= 1.0125f;
        }

        baseAmount += 3 * amountPoints;

        totalEnemies = baseAmount;
        enemyHealth = baseHealth;
        enemySpeed = baseSpeed;
        remainingEnemies = totalEnemies;

        int baseMoney = 10;

        for(int i = 0; i < speedPoints + healthPoints;i++)
        {
            baseMoney += 2;
            baseMoney = (int)(1.005f * baseMoney);
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
