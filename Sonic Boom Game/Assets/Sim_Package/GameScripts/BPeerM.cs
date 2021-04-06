using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPeerM : MonoBehaviour
{
    private static BPeerM _BPeerMInstance;

    public float bpm;

    private float beatInterval, beatTimer, beatIntervalD8, beatTimerD8;

    public static bool beatFull, beatD8;

    public static int beatCountFull, beatCountD8;

    private void Awake()
    {
        if(_BPeerMInstance != null && _BPeerMInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _BPeerMInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        BeatDetection();
    }

    void BeatDetection()
    {
        //full beat count
        beatFull = false;

        // so every second, there will be a beat interval
        beatInterval = 60 / bpm;

        beatTimer += Time.deltaTime;

        if(beatTimer >= beatInterval)
        {
            beatTimer -= beatInterval;
            beatFull = true;
            beatCountFull++;

            Debug.Log("Full");
        }

        //divided beat count
        beatD8 = false;
        beatIntervalD8 = beatInterval / 8;
        beatTimerD8 += Time.deltaTime;
        if(beatTimerD8 >= beatIntervalD8)
        {
            beatTimerD8 -= beatIntervalD8;
            beatD8 = true;
            beatCountD8++;

            Debug.Log("D8");
        }
    }

}
