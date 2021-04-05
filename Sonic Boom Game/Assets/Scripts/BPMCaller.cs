using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMCaller : MonoBehaviour
{
    public Animator animator;

    public Animation pulseAnim;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPulse()
    {
        animator.SetTrigger("Pulse");
        //pulseAnim.Play();
    }
}
