using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToTitle : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(GoToTitle());
    }

    IEnumerator GoToTitle()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("StartScene");
    }
}
