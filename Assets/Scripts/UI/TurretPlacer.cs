using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class TurretPlacer : MonoBehaviour
{
    [Header("Editor")]
    public Text nameText;
    public Text priceText;
    public Color defaultColor;
    public Color selectedColor;
    public GameObject spriteContainer;


    public int turretID = 0;

    public GameInfoHolder gameInfoHolder;
    void SetupGameInfoHolder()
    {
        gameInfoHolder = GameObject.FindObjectOfType<GameInfoHolder>();
    }
    /// <summary>
    /// Changes the selected turret to place
    /// </summary>
    public void OnClick()
    {
        if (gameInfoHolder == null)
            SetupGameInfoHolder();

        gameInfoHolder.selectionHolder.SelectedTurretInMenu = turretID;
    }

    private void Update()
    {
        if (gameInfoHolder == null)
            SetupGameInfoHolder();


        int turretPrice = gameInfoHolder.turretInfoHolder.turrets[turretID].GetComponent<Turret>().basePrice;
        string turretName = gameInfoHolder.turretInfoHolder.turrets[turretID].GetComponent<Turret>().turretName;

        //Changing texts
        nameText.text = turretName;
        priceText.text = "$" + turretPrice;

        //Changing the background
        if (gameInfoHolder.selectionHolder.SelectedTurretInMenu == turretID)
            GetComponent<Image>().color = selectedColor;
        else 
            GetComponent<Image>().color = defaultColor;

        //Changing the sprite to represent the right turret
        spriteContainer.GetComponent<SpriteRenderer>().sprite = 
            gameInfoHolder.turretInfoHolder.turrets[turretID].GetComponent<SpriteRenderer>().sprite;
    }
}
