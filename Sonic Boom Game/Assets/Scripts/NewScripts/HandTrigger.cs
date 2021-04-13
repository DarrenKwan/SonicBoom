using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
[RequireComponent(typeof(InteractionBehaviour))]
public class HandTrigger : MonoBehaviour
{
    private InteractionBehaviour _intObj;

    private void Start()
    {
        _intObj = GetComponent<InteractionBehaviour>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hand"))
        {
            Debug.Log("Shoot a thing");
        }
    }
}
