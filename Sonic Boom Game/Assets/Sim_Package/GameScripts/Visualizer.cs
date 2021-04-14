using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visualizer : MonoBehaviour
{
    AudioSource bgm = null;

    public float minHeight = 15f;
    public float maxHeight = 300;
    public bool loop = true;

    public int visualizerSimples = 64;

    [Space(15), Range(64, 8192)]
    public VisualizerObject[] visualizerObjects;

    void Start()
    {
        bgm = GameObject.Find("GameManager").GetComponent<AudioSource>();
        visualizerObjects = GetComponentsInChildren<VisualizerObject>();
    }

    void Update()
    {
        float[] spectrumData = new float[8192];

        bgm.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        for (int i = 0; i < visualizerObjects.Length; i++)
        {
            Vector2 newSize = visualizerObjects[i].GetComponent<RectTransform>().rect.size;
            newSize.y = Mathf.Lerp(newSize.y, minHeight + (spectrumData[i] * (maxHeight - minHeight) * 90), 0.5f);

            visualizerObjects[i].GetComponent<RectTransform>().sizeDelta = newSize;
        }
    }
}
