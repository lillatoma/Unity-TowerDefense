using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class RightBar : MonoBehaviour
{
    public GameObject canvas;
    public GameObject bgPanel;
    // Start is called before the first frame update

    /// <summary>
    /// This function uses the Canvas's rect transform, and widthInUnits to 
    /// decide the appropriate size and positioning for the panel on the right-side
    /// </summary>
    /// <param name="widthInUnits"></param>
    public void UpdateBgPanel(int widthInUnits)
    {
        
        Vector2 size = ((RectTransform)canvas.transform).sizeDelta;
        float unitScale = size.y / 32;
        RectTransform rT = (RectTransform)bgPanel.transform;

        float remainingSpace = size.x - widthInUnits;
        rT.sizeDelta = new Vector2(unitScale * remainingSpace, size.y);
        rT.position = new Vector2((size.x-remainingSpace)*0.5f - 0.5f, 0f);
    }


    // Update is called once per frame
    void Update()
    {
    }
}
