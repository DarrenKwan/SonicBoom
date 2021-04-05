using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMScript : MonoBehaviour
{
    public float bpm;

    private float musicTime;

    public float beatTime;

    //private bool onBeat;

    private float onBeatNumber;

    private int beatCount;

    public UnityEngine.Events.UnityEvent onBeat;

    public UnityEngine.Events.UnityEvent onFourthBeat;

    public UnityEngine.Events.UnityEvent onThirdBeat;

    public float bpmModifier;
    // Start is called before the first frame update
    void Start()
    {
        beatCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        musicTime = gameObject.GetComponent<AudioSource>().time;

        FindBeatTime();
        CheckOnBeat();
        if (Input.GetButtonDown("Fire1") && IsCloseEnoughToBeat(.2f))
        {
            Debug.Log("TEST");
        }
    }

    void FindBeatTime()
    {
        beatTime = (60f/bpm); 
    }

    void CheckOnBeat()
    {
        int currentBeat;
        currentBeat = Mathf.FloorToInt(musicTime / beatTime);
        if (currentBeat != beatCount)
        {
            beatCount = currentBeat;
            //Debug.Log(beatCount);
            onBeat.Invoke();
        }

        if (currentBeat % 4 == 0)
        {
            onFourthBeat.Invoke();
        }

        if (currentBeat % 3 == 0)
        {
            onThirdBeat.Invoke();
        }
    }

    public bool IsCloseEnoughToBeat(float window)
    {
        int nextBeatNumber = Mathf.FloorToInt(musicTime / beatTime);
        float beatCheck = nextBeatNumber * beatTime;
        float offset = (musicTime - beatCheck);
        return (Mathf.Abs(offset) <= window);
    }

}
