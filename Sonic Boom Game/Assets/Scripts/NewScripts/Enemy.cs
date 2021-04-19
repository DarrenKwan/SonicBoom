using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    BaseStats myBaseStats;

    [SerializeField] float AttackChance, DefenseChance, ChokeChance;
    List<Tuple<float, Action>> possibleChance = new List<Tuple<float, Action>>();

    //morale stats
    [SerializeField] float maxMorale = 10f;
    [SerializeField] float curMorale = 0;
    float moraleIncreaseOverTime = 1f;
    [SerializeField] Image moraleImage;

    //player reference
    [SerializeField] GameObject player;

    AudioSource theAudioSource;
    [SerializeField] AudioClip attackSound, defendSound, chokeSound;

    void Start()
    {
        myBaseStats = GetComponent<BaseStats>();

        theAudioSource = GetComponent<AudioSource>();

        //our percentage chances
        AttackChance = 42.5f;
        DefenseChance = 42.5f;
        ChokeChance = 15f;

        possibleChance.Add(new Tuple<float, Action>(AttackChance, EnemyAttack));
        //wat we're doing up there is binding the attack chance to our attack function
        //can call this function using our attack chance

        possibleChance.Add(new Tuple<float, Action>(DefenseChance, EnemyDefend));
        possibleChance.Add(new Tuple<float, Action>(ChokeChance, EnemyChoke));

        //filling meter
        moraleImage.fillAmount = curMorale / maxMorale;
    }


    void Update()
    {
        //increase our morale rate
        curMorale += moraleIncreaseOverTime * Time.deltaTime;

        //visual feedback for morale increase --> need ref to image
        moraleImage.fillAmount = curMorale / maxMorale;

        //if our morale is maxxed, or above max, we choose an action
        if (curMorale >= maxMorale)
        {
            ChooseAction();
            curMorale = 0;
        }
    }

    void ChooseAction()
    {
        //reset our defense stance
        myBaseStats.defending = false;

        //Debug.Log("enemy takes an action now");

        float chance = UnityEngine.Random.Range(0f, 100f);
        //specifying unityengine here cus we're also using system --> for the Tuples

        float sumChance = 0;

        for (int i = 0; i < possibleChance.Count; i++)
        {
            sumChance += possibleChance[i].Item1;

            if (sumChance >= chance)
            {
                possibleChance[i].Item2();
                break;
            }
        }

        //randomize our new morale recovery rate
        float newMoraleRecoveryRate = UnityEngine.Random.Range(0.5f, 1.5f);
        moraleIncreaseOverTime = newMoraleRecoveryRate;
    }

    void EnemyAttack()
    {
        //Debug.Log("enemy attacks");

        //play sound
        theAudioSource.PlayOneShot(attackSound);

        player.GetComponent<BaseStats>().TakeDamage(myBaseStats.attack);
    }

    void EnemyDefend()
    {
        //Debug.Log("enemy defends");

        //play sound
        theAudioSource.PlayOneShot(defendSound);
    }

    void EnemyChoke()
    {
        //Debug.Log("enemy choked & skipped his turn");

        //play sound
        theAudioSource.PlayOneShot(chokeSound);
    }
}
