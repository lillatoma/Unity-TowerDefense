using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject lineEffect;

    private List<float> lineEffectTime;
    private List<GameObject> lineEffects;

    int FindFirstInactiveLine()
    {
        for (int i = 0; i < lineEffects.Count; i++)
            if (!lineEffects[i].activeSelf)
                return i;

        return -1;
    }

    public void SetupLineEffect(Vector3 start, Vector3 end, Color startC, Color endC)
    {
        int first = FindFirstInactiveLine();
        if (first != -1)
        {
            lineEffectTime[first] = 0;
            lineEffects[first].GetComponent<LineRenderer>().SetPosition(0, start);
            lineEffects[first].GetComponent<LineRenderer>().SetPosition(1, end);

            lineEffects[first].GetComponent<LineRenderer>().startColor = startC;
            lineEffects[first].GetComponent<LineRenderer>().endColor = endC;
            
            for (int i = 0; i < lineEffects[first].GetComponent<LineRenderer>().colorGradient.alphaKeys.Length; i++)
            {
                lineEffects[first].GetComponent<LineRenderer>().colorGradient.alphaKeys[i].alpha = 1f;
            }
            lineEffects[first].SetActive(true);
        }
        else
        {
            GameObject go = GameObject.Instantiate(lineEffect);
            go.transform.parent = transform;
            go.SetActive(false);
            lineEffects.Add(go);
            float linetime = 0f;
            lineEffectTime.Add(linetime);
            SetupLineEffect(start, end, startC, endC);

        }
        
    }

    void SimulateLineEffects()
    {
        for(int i = 0; i < lineEffects.Count; i++)
        {
            if(lineEffects[i].activeSelf)
            {
                lineEffectTime[i] += Time.deltaTime;

                float alpha = (0.25f - lineEffectTime[i]) * 4f;

                for (int j = 0; j < lineEffects[i].GetComponent<LineRenderer>().colorGradient.alphaKeys.Length; j++)
                {
                    lineEffects[i].GetComponent<LineRenderer>().colorGradient.alphaKeys[j].alpha = alpha;
                }

                Color startC = lineEffects[i].GetComponent<LineRenderer>().startColor;
                Color endC = lineEffects[i].GetComponent<LineRenderer>().endColor;

                lineEffects[i].GetComponent<LineRenderer>().startColor = new Color(startC.r, startC.g, startC.b, alpha);
                lineEffects[i].GetComponent<LineRenderer>().endColor = new Color(endC.r, endC.g, endC.b, alpha);

                if (lineEffectTime[i] > 0.25f)
                    lineEffects[i].SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lineEffects = new List<GameObject>();
        lineEffectTime = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        SimulateLineEffects();
    }
}
