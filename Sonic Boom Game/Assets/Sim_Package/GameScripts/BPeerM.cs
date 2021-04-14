using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPeerM : MonoBehaviour
{
    private static BPeerM _BPeerMInstance;

    public float bpm;

    private float beatInterval, beatTimer, beatIntervalD2, beatTimerD2, beatIntervalHalf, beatTimerHalf;

    public static bool beatFull, beatD2, beatHalf;

    public static int beatCountFull, beatCountD2, beatCountHalf;

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

            //Debug.Log("Full");
        }

        //divided beat count
        beatD2 = false;
        beatIntervalD2 = beatInterval / 2;
        beatTimerD2 += Time.deltaTime;
        if(beatTimerD2 >= beatIntervalD2)
        {
            beatTimerD2 -= beatIntervalD2;
            beatD2 = true;
            beatCountD2++;

            //Debug.Log("D8");
        }

        //multiplied beat count
        beatHalf = false;
        beatIntervalHalf = beatInterval / 0.5f;
        beatTimerHalf += Time.deltaTime;
        if(beatTimer>= beatIntervalHalf)
        {
            beatTimer -= beatIntervalHalf;
            beatHalf = true;
            beatCountHalf++;
        }
    }

}
