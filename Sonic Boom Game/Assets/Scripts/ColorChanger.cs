using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color color1;

    public Color color2;

    bool inColor1;

    bool inColor2;

    private Renderer thisRenderer;
    // Start is called before the first frame update
    void Start()
    {
        thisRenderer = GetComponent<Renderer>();
        inColor1 = true;
        BPMScript bpmScript = GameObject.FindObjectOfType<BPMScript>();
        if (bpmScript != null)
        {
            bpmScript.onBeat.AddListener(ChangeColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor()
    {
        if (inColor1)
        {
            thisRenderer.material.color = color1;
            thisRenderer.material.SetColor("_EmissionColor", color1);
            inColor1 = false;
            inColor2 = true;
        }
        else if (inColor2)
        {
            thisRenderer.material.color = color2;
            thisRenderer.material.SetColor("_EmissionColor", color2);
            inColor2 = false;
            inColor1 = true;
        }
    }
}
