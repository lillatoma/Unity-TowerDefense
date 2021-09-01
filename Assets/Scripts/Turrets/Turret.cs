using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data holder for turret upgrades
/// </summary>
public class UpgradeData
{

    public string name = "";
    public int upPrice = 5;
    public int level = 0;

    public bool isInteger = false;
    public int valueInt = 5;
    public float valueFloat = 5f;
    public int valueNextInt = 8;
    public float valueNextFloat = 8f;

    public string GetValuePrinted()
    {
        if (isInteger) return valueInt.ToString();
        return string.Format("{0:0.##}",valueFloat);
    }

    public string GetNextValuePrinted()
    {
        if (isInteger) return valueNextInt.ToString();
        return string.Format("{0:0.##}",valueNextFloat);
    }

    public UpgradeData()
    {
        name = "Upgrade";
        upPrice = 10;
        level = 1;
        isInteger = false;
        valueInt = 10;
        valueNextInt = 12;
        valueFloat = 10f;
        valueNextFloat = 12f;
    }

    UpgradeData(string name, int upPrice, int level, bool integer, int value, int valuenext, float val, float valnext)
    {
        this.name = name;
        this.upPrice = upPrice;
        this.level = level;
        isInteger = integer;
        valueInt = value;
        valueFloat = val;
        valueNextInt = valuenext;
        valueNextFloat = valnext;
    }

}

/// <summary>
/// Data holder for turrets
/// </summary>
public class Turret : MonoBehaviour
{
    public string turretName;

    [Header("Base data")]
    public int basePrice;
    public int damage;
    public float range;
    public float fireRate;

    [Header("Base data on upgrades")]
    public int damageStep;
    public float rangeStep;
    public float fireRateStep;

    [Header("Upgrade Price data")]
    public int damageUpgrade;
    public int rangeUpgrade;
    public int fireRateUpgrade;

    [Header("Upgrade Price data on upgrades")]
    public int damageUpgradeStep;
    public int rangeUpgradeStep;
    public int fireRateUpgradeStep;

    [Header("Statistics")]
    public int cashSpent = 0;
    public int totalUpgrades = 0;
    public int noOfUpgrades;
    public UpgradeData[] upgrades;
    public int eliminations = 0;


    protected int damageLevel = 0;
    protected int rangeLevel = 0;
    protected int fireRateLevel = 0;
    protected float timeTillNextShot = 0f;

    protected GameObject enemyContainer;
    protected GameInfoHolder gameInfoHolder;

    protected void GetGameInfoHolder()
    {
        gameInfoHolder = FindObjectOfType<GameInfoHolder>();
    }

    /// <summary>
    /// Returns 80% of the base price + 50% of the money spent on upgrades
    /// </summary>
    /// <returns></returns>
    public int CalculateSellPrice()
    {
        return basePrice * 4 / 5 + cashSpent / 2;
    }

    public int UpgradePriceDamage()
    {
        return damageUpgrade + damageLevel * damageUpgradeStep;
    }

    public int UpgradePriceRange()
    {
        return rangeUpgrade + rangeLevel * rangeUpgradeStep;
    }

    public int UpgradePriceFirerate()
    {
        return fireRateUpgrade + fireRateLevel * fireRateUpgradeStep;
    }

    virtual public bool Upgrade(int which)
    {
        return false;
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
