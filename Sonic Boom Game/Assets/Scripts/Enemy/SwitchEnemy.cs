using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchEnemy : MonoBehaviour
{
    private EnemyAI enemyAI;
    // Start is called before the first frame update
    void Start()
    {
        BPMScript bpmScript = GameObject.FindObjectOfType<BPMScript>();
        if (bpmScript != null)
        {
            bpmScript.onBeat.AddListener(ChangeState);
        }

        enemyAI = gameObject.GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAI.currentState == EnemyAI.enemyState.trebleMode)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        if (enemyAI.currentState == EnemyAI.enemyState.bassMode)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    public void ChangeState()
    {
        if (enemyAI.currentState == EnemyAI.enemyState.trebleMode)
        {
            enemyAI.currentState = EnemyAI.enemyState.bassMode;
        }
        else if (enemyAI.currentState == EnemyAI.enemyState.bassMode)
        {
            enemyAI.currentState = EnemyAI.enemyState.trebleMode;
        }
    }
}
