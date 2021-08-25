using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class LeftBar : MonoBehaviour
{
    public GameObject canvas;
    public GameObject bgPanel;
    public Text moneyText;

    private GameInfoHolder gameInfoHolder;

    // Start is called before the first frame update

    /// <summary>
    /// This function uses the Canvas's rect transform, and widthInUnits to 
    /// decide the appropriate size and positioning for the panel on the left-side
    /// </summary>
    /// <param name="widthInUnits"></param>
    public void UpdateBgPanel(int widthInUnits)
    {
        
        Vector2 size = ((RectTransform)canvas.transform).sizeDelta;
        float unitScale = size.y / 32;
        RectTransform rT = (RectTransform)bgPanel.transform;
        rT.sizeDelta = new Vector2(unitScale * widthInUnits, size.y);
        rT.position = new Vector2(unitScale * widthInUnits * 0.5f - size.x * 0.5f - 0.5f, 0f);
    }

    private void Start()
    {
        gameInfoHolder = FindObjectOfType<GameInfoHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$ " + gameInfoHolder.statHolder.playerMoney;
    }
}
