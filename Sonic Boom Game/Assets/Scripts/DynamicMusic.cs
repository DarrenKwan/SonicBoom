using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour
{
    public GauntletScript gauntletScript;

    public List<AudioSource> musicParts = new List<AudioSource>();

    public float[] scoreThresholds;
    // Start is called before the first frame update
    void Start()
    {
        gauntletScript = FindObjectOfType<GauntletScript>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < scoreThresholds.Length; i++)
        {
            if (gauntletScript.comboCount >= scoreThresholds[i])
            {
                musicParts[i].volume = 1;
            }
            else
            {
                musicParts[i].volume = 0;
            }
        }


        /*
        for (int i = 0; i < musicParts.Count; i++)
        {
            if (gauntletScript.comboCount >= 0)
            {
                musicParts[0].volume = 1;
                musicParts[i - 0].volume = 0;
            }
            else if (gauntletScript.comboCount >= 5)
            {
                musicParts[0].volume = 1;
                musicParts[1].volume = 1;
                musicParts[i - 1].volume = 0;
            }
        }

        if (gauntletScript.comboCount >= 0)
        {
            for (int i = 0; i < musicParts.Count; i++)
            {
                musicParts[0].volume = 1;
            }
        }
        else if (gauntletScript.comboCount >= 5)
        {
            for (int i = 0; i < musicParts.Count; i++)
            {
                musicParts[1].volume = 1;
            }
        }
        */
    }
}
