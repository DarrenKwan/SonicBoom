using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboCounttext;

    GameObject theGame;

    public List<int> comboCount;
    public float score;

    int scoreToAdd = 100;

    void Start()
    {
        theGame = GameObject.Find("Game");

        StartCoroutine(EndTheGame());

        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
    }

    public void RegisterHitCube()
    {
        AddScore();
    }

    public void AddScore()
    {
        score += scoreToAdd * 0.1f * comboCount.Count;

        //add to our combo count
        comboCount.Add(1);

        //update the text
        scoreText.text = score.ToString();

        comboCounttext.text = comboCount.Count.ToString();
    }

    public void ResetCombo()
    {
        comboCount.Clear();

        comboCounttext.text = comboCount.Count.ToString();
    }

    IEnumerator EndTheGame()
    {
        yield return new WaitForSeconds(104);

        theGame.SetActive(false);

        yield return new WaitForSeconds(3);

        Application.Quit();
        Debug.Log("quit the game");
    }
}
