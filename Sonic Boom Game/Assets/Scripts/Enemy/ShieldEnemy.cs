using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour
{
    private GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        
        BPMScript bpmScript = GameObject.FindObjectOfType<BPMScript>();
        if (bpmScript != null)
        {
            bpmScript.onFourthBeat.AddListener(DeactivateShield);
            bpmScript.onThirdBeat.AddListener(ActivateShield);
        }

        
        shield = transform.Find("Shield").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ActivateShield()
    {
        shield.SetActive(true);
    }

    public void DeactivateShield()
    {
        shield.SetActive(false);
    }

}
