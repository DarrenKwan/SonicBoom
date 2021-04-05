using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BPMScript bpmScript = GameObject.FindObjectOfType<BPMScript>();
        if (bpmScript != null)
        {
            bpmScript.onBeat.AddListener(BeatActive);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeatActive()
    {
        Light light = gameObject.GetComponent<Light>();
        light.enabled = !light.enabled;
    }
}
