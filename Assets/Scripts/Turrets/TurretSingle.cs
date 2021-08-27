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
                    child.GetComponent<Enemy>().Damage(damage);
                    timeTillNextShot += 1f / fireRate;

                    transform.rotation = Quaternion.Euler(0, 0, Utils.RealVector2Angle(child.position - transform.position) - 90f);
                    return;
                }
            }

        }
    }

    private void Start()
    {
        GetGameInfoHolder();
        enemyContainer = GameObject.FindGameObjectWithTag("EnemyContainer");
        noOfUpgrades = 3;
        upgrades = new UpgradeData[noOfUpgrades];
        SetUpgradeData();
    }

    private void SetUpgradeData()
    {
        for(int i =0; i < upgrades.Length;i++)
            upgrades[i] = new UpgradeData();
        upgrades[0].name = "Damage";
        upgrades[0].upPrice = UpgradePriceDamage();
        upgrades[0].level = damageLevel;
        upgrades[0].isInteger = true;
        upgrades[0].valueInt = damage;
        upgrades[0].valueNextInt = damage + damageStep;

        upgrades[1].name = "Range";
        upgrades[1].upPrice = UpgradePriceRange();
        upgrades[1].level = rangeLevel;
        upgrades[1].isInteger = false;
        upgrades[1].valueFloat = range;
        upgrades[1].valueNextFloat = range + rangeStep;

        upgrades[2].name = "Fire rate";
        upgrades[2].upPrice = UpgradePriceFirerate();
        upgrades[2].level = fireRateLevel;
        upgrades[2].isInteger = false;
        upgrades[2].valueFloat = fireRate;
        upgrades[2].valueNextFloat = fireRate + fireRateStep;
    }


    public override void Upgrade(int which)
    {
        if (which == 0) //Damage
        {
            if (gameInfoHolder.statHolder.playerMoney >= UpgradePriceDamage())
            {
                gameInfoHolder.statHolder.playerMoney -= UpgradePriceDamage();
                cashSpent += UpgradePriceDamage();
                damageLevel++;
                damage += damageStep;
            }
        }
        else if (which == 1) //Range
        {
            if (gameInfoHolder.statHolder.playerMoney >= UpgradePriceRange())
            {
                gameInfoHolder.statHolder.playerMoney -= UpgradePriceRange();
                cashSpent += UpgradePriceRange();
                rangeLevel++;
                range += rangeStep;
            }
        }
        else if (which == 2) //Fire rate
        {
            if (gameInfoHolder.statHolder.playerMoney >= UpgradePriceFirerate())
            {
                gameInfoHolder.statHolder.playerMoney -= UpgradePriceFirerate();
                cashSpent += UpgradePriceFirerate();
                fireRateLevel++;
                fireRate += fireRateStep;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetUpgradeData();
        AttackOpponents();
    }
}
