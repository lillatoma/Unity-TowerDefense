using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShockTower : Turret
{
    EffectManager effectManager;
    public void CreateLine(Vector3 lastShotTarget)
    {

        effectManager.SetupLineEffect(new Vector3(transform.position.x, transform.position.y, -0.2f),
            new Vector3(lastShotTarget.x, lastShotTarget.y, -0.2f),
            new Color(0f, 0.75f, 1f),
            new Color(0f, 0.25f, 1f));

    }
    /// <summary>
    /// Deals damage to every enemy within range
    /// </summary>
    void AttackOpponents()
    {
        if (timeTillNextShot >= -0.1f)
            timeTillNextShot -= Time.deltaTime;



        while (timeTillNextShot < 0f)
        {
            bool hit = false;
            //In this loop, every enemy in range gets targetted
            foreach (Transform child in enemyContainer.transform)
            {
                 if (child.GetComponent<Enemy>().health <= 0) 
                    continue;
                if ((transform.position - child.position).magnitude < range)
                {
                    child.GetComponent<Enemy>().Damage(damage,
                        transform.parent.GetComponent<Tile>().indexCoordinates);
                    hit = true;
                    CreateLine(child.position);
                }
            }
            if (hit)
                timeTillNextShot += 1f / fireRate;
            else
                break;

        }
    }

    private void Start()
    {
        GetGameInfoHolder();
        enemyContainer = GameObject.FindGameObjectWithTag("EnemyContainer");
        effectManager = GameObject.FindGameObjectWithTag("Effects").GetComponent<EffectManager>();
        noOfUpgrades = 3;
        upgrades = new UpgradeData[noOfUpgrades];
        SetUpgradeData();
    }

    private void SetUpgradeData()
    {
        for (int i = 0; i < upgrades.Length; i++)
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


    public override bool Upgrade(int which)
    {
        if (which == 0) //Damage
        {
            if (gameInfoHolder.statHolder.playerMoney >= UpgradePriceDamage())
            {
                gameInfoHolder.statHolder.playerMoney -= UpgradePriceDamage();
                cashSpent += UpgradePriceDamage();
                damageLevel++;
                totalUpgrades++;
                damage += damageStep;
                return true;
            }
        }
        else if (which == 1) //Range
        {
            if (gameInfoHolder.statHolder.playerMoney >= UpgradePriceRange())
            {
                gameInfoHolder.statHolder.playerMoney -= UpgradePriceRange();
                cashSpent += UpgradePriceRange();
                rangeLevel++;
                totalUpgrades++;
                range += rangeStep;
                return true;
            }
        }
        else if (which == 2) //Fire rate
        {
            if (gameInfoHolder.statHolder.playerMoney >= UpgradePriceFirerate())
            {
                gameInfoHolder.statHolder.playerMoney -= UpgradePriceFirerate();
                cashSpent += UpgradePriceFirerate();
                fireRateLevel++;
                totalUpgrades++;
                fireRate += fireRateStep;
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        SetUpgradeData();
        AttackOpponents();
    }
}
