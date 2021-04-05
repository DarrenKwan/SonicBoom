using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StatTracker : MonoBehaviour
{
    public GauntletScript gauntletScript;

    private float highestCombo = 0;

    private float timer = 0;

    private bool gameFinished = false;

    public List<GameObject> enemies = new List<GameObject>();

    public Text comboText;

    public Text timeText;

    public GameObject endGamePanel;
    // Start is called before the first frame update
    void Start()
    {
        endGamePanel.SetActive(false);
        gauntletScript = FindObjectOfType<GauntletScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float minutes = 0f;
        if (gauntletScript.comboCount >= highestCombo)
        {
            highestCombo = gauntletScript.comboCount;
        }

        if (gameFinished == false)
        {
            timer += Time.deltaTime;
        }

        comboText.text = "HIGHEST COMBO: " + highestCombo;
        timeText.text = "TIME: " + timer + "SECONDS";

        
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                gameFinished = false;
            }
            else
            {
                gameFinished = true;
            }
        }
        
        /*
        if (GameObject.FindGameObjectsWithTag("Enemy") == null)
        {
            gameFinished = true;
        }
        */
        if (gameFinished == true)
        {
            endGamePanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
