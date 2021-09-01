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

    public bool IsBossRound(int l = -1)
    {
        if (l == -1)
            l = level;
        return (l % 10) == 0;
    }
    /// <summary>
    /// Generates the current level
    /// Levels get harder in a faster pace at levels 10, 25, 50, 75 and 100
    /// Every 10th level is a boss round
    /// </summary>
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

        if (IsBossRound(level + 1))
        {
            int healthPoints = Random.Range(0, skillPoints);
            skillPoints -= healthPoints;

            int speedPoints = Random.Range(0, skillPoints);
            skillPoints -= speedPoints;

            int rval = Random.Range(0, skillPoints);
            speedPoints += rval;
            healthPoints += (skillPoints - rval);

            int baseHealth = 200;
            float baseSpeed = 1f;

            for (int i = 0; i < level; i++)
            {
                baseHealth += 30;
                baseSpeed += .015f;
                baseHealth = (int)(1.0125f * baseHealth);
            }

            for (int i = 0; i < healthPoints; i++)
            {
                baseHealth += 75;
                baseHealth = (int)(1.015f * baseHealth);
            }

            for (int i = 0; i < speedPoints; i++)
            {
                baseSpeed += 0.1f;
            }

            totalEnemies = 1;
            enemyHealth = baseHealth;
            enemySpeed = baseSpeed;
            remainingEnemies = totalEnemies;

            int baseMoney = 100;

            for (int i = 0; i < speedPoints + healthPoints; i++)
            {
                baseMoney += 25;
                baseMoney = (int)(1.005f * baseMoney);
            }

            enemyMoney = baseMoney;

            enemyIndex = gameInfoHolder.enemyInfoHolder.RandomIndex();
        }
        else
        {

            int baseHealth = 20;
            float baseSpeed = 2f;
            int baseAmount = 10;

            for (int i = 0; i < level; i++)
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

            for (int i = 0; i < healthPoints; i++)
            {
                baseHealth += 10;
                baseHealth = (int)(1.015f * baseHealth);
            }

            for (int i = 0; i < speedPoints; i++)
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

            for (int i = 0; i < speedPoints + healthPoints; i++)
            {
                baseMoney += 2;
                baseMoney = (int)(1.005f * baseMoney);
            }

            enemyMoney = baseMoney;

            enemyIndex = gameInfoHolder.enemyInfoHolder.RandomIndex();

        }
    }

    public void AdvanceLevel()
    {
        level++;
    }
}
