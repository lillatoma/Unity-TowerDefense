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


        nameText.text = turretName;
        priceText.text = "$" + turretPrice;

        if (gameInfoHolder.selectionHolder.SelectedTurretInMenu == turretID)
            GetComponent<Image>().color = selectedColor;
        else 
            GetComponent<Image>().color = defaultColor;

        spriteContainer.GetComponent<SpriteRenderer>().sprite = 
            gameInfoHolder.turretInfoHolder.turrets[turretID].GetComponent<SpriteRenderer>().sprite;
    }
}
