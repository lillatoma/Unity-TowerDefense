using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Default Turret. Shots one bullet, no special effect.
/// </summary>
public class TurretFlash : Turret
{
    [Header("Flash data")]
    public float flashDuration;
    public float flashDurationStep;
    public int flashDurationUpgrade;
    public int flashDurationUpgradeStep;

    private int flashLevel = 0;

    EffectManager effectManager;

    public int UpgradePriceFlashDuration()
    {
        return flashDurationUpgrade + flashLevel * flashDurationUpgradeStep;
    }
    /// <summary>
    /// Deals damage and flashes opponents that are within range
    /// Target priority is lowest index, that is unfrozen
    /// </summary>
    void AttackOpponents()
    {
        if (timeTillNextShot >= -0.1f)
            timeTillNextShot -= Time.deltaTime;
        //For every possible shot compensated for lag
        while (timeTillNextShot < 0f)
        {
            bool hit = false;
            foreach (Transform child in enemyContainer.transform)
            {
                //First non-frozen enemies are targeted
                if (timeTillNextShot > 0f) //So no extra impossible shots are made
                    break;
                if (child.GetComponent<Enemy>().health <= 0) //Ignoring dead targets
                    continue;
                if ((transform.position - child.position).magnitude < range //Is within range
                    && child.gameObject.GetComponent<Enemy>().IsFreezeable()) //And is freezable
                {
                    child.GetComponent<Enemy>().Damage(damage,
                        transform.parent.GetComponent<Tile>().indexCoordinates);

                    child.GetComponent<Enemy>().Freeze(flashDuration);
                    timeTillNextShot += 1f / fireRate;
                    hit = true;
                    CreateLine(child.position);
                    transform.rotation = Quaternion.Euler(0, 0, Utils.RealVector2Angle(child.position - transform.position) - 90f);
                }
            }
            if (hit)
                continue;

            foreach (Transform child in enemyContainer.transform)
            {
                if (timeTillNextShot > 0f)
                    break;
                if (child.GetComponent<Enemy>().health <= 0)
                    continue;
                if ((transform.position-child.position).magnitude < range)
                {
                    child.GetComponent<Enemy>().Damage(damage,
                        transform.parent.GetComponent<Tile>().indexCoordinates);
                    child.GetComponent<Enemy>().Freeze(flashDuration);
                    timeTillNextShot += 1f / fireRate;
                    hit = true;
                    CreateLine(child.position);
                    transform.rotation = Quaternion.Euler(0, 0, Utils.RealVector2Angle(child.position - transform.position) - 90f);
                }
            }
            if (!hit)
                break;
        }
    }

    private void Start()
    {
        GetGameInfoHolder();
        enemyContainer = GameObject.FindGameObjectWithTag("EnemyContainer");
        effectManager = GameObject.FindGameObjectWithTag("Effects").GetComponent<EffectManager>();
        noOfUpgrades = 4;
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

        upgrades[3].name = "Flash duration";
        upgrades[3].upPrice = UpgradePriceFlashDuration();
        upgrades[3].level = flashLevel;
        upgrades[3].isInteger = false;
        upgrades[3].valueFloat = flashDuration;
        upgrades[3].valueNextFloat = flashDuration + flashDurationStep;
    }
    /// <summary>
    /// Creates a line effect between the turret and lastShotTarget
    /// </summary>
    /// <param name="lastShotTarget"></param>
    public void CreateLine(Vector3 lastShotTarget)
    {

        effectManager.SetupLineEffect(new Vector3(transform.position.x, transform.position.y, -0.2f),
            new Vector3(lastShotTarget.x, lastShotTarget.y, -0.2f),
            new Color(0f, 1f, 0f),
            new Color(0f, 0.75f, 0.33f));

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
        else if (which == 3) //Flash duration
        {
            if (gameInfoHolder.statHolder.playerMoney >= UpgradePriceFlashDuration())
            {
                gameInfoHolder.statHolder.playerMoney -= UpgradePriceFlashDuration();
                cashSpent += UpgradePriceFlashDuration();
                flashLevel++;
                totalUpgrades++;
                flashDuration += flashDurationStep;
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
