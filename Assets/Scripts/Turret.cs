using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Base data")]
    public int basePrice;
    public int damage;
    public float range;
    public float fireRate;

    [Header("Base data on upgrades")]
    public int damageStep;
    public float rangeStep;
    public float fireRateStep;

    [Header("Price data")]
    public int damageUpgrade;
    public int rangeUpgrade;
    public int fireRateUpgrade;

    [Header("Price data on upgrades")]
    public int damageUpgradeStep;
    public int rangeUpgradeStep;
    public int fireRateUpgradeStep;

    protected int damageLevel = 0;
    protected int rangeLevel = 0;
    protected int fireRateLevel = 0;
    protected float timeTillNextShot = 0f;

    protected GameObject enemyContainer;


    public int upgradePriceDamage()
    {
        return damageUpgrade + damageLevel * damageUpgradeStep;
    }

    public int upgradePriceRange()
    {
        return rangeUpgrade + rangeLevel * rangeUpgradeStep;
    }

    public int fireRatePriceDamage()
    {
        return fireRateUpgrade + fireRateLevel * fireRateUpgradeStep;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyContainer = GameObject.FindGameObjectWithTag("EnemyContainer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
