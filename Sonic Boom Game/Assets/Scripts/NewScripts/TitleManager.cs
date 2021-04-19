using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("You gotta spell this shit right")]
    [SerializeField] string sceneNameToLoad;

    public void GoNext()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
