﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersChampion : MonoBehaviour
{
    //particles
    [SerializeField] ParticleSystem AttackParticles, DefenseParticles, MoraleParticles;

    //image to convey when your player will do something
    [SerializeField] Image moraleImage;

    //stats
    [SerializeField] float AttackChance, DefenseChance, ChokeChance;
    List<Tuple<float, Action>> possibleChance = new List<Tuple<float, Action>>();
        //dafuq a tuple --> allows u to take two obj and make it 1 obj in a list
            //now u have these floats & an action -> a function (or at least points to a function)
                //item1 is the float, item2 is the action

    //morale stats
    [SerializeField] float maxMorale = 10f;
    [SerializeField] float curMorale = 0;
    float moraleIncreaseOverTime = 1f;
    float encouragementRate = 1.5f;

    //choosing actions
    float TotalChance = 100f;


    void Start()
    {
        //our percentage chances
        AttackChance = 33;
        DefenseChance = 33;
        ChokeChance = 34;

        possibleChance.Add(new Tuple<float, Action>(AttackChance, PlayerAttack));
            //wat we're doing up there is binding the attack chance to our attack function
                //can call this function using our attack chance

        possibleChance.Add(new Tuple<float, Action>(DefenseChance, PlayerDefend));
        possibleChance.Add(new Tuple<float, Action>(ChokeChance, PlayerChoke));

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

            //reset our morale back to 0
            curMorale = 0;
        }
    }

    void ChooseAction()
    {
        Debug.Log("player takes an action now");

        float chance = UnityEngine.Random.Range(0f, 100f);
        //specifying unityengine here cus we're also using system --> for the Tuples

        float sumChance = 0;

        for (int i = 0; i < possibleChance.Count; i++)
        {
            sumChance += possibleChance[i].Item1;
            //item 1 is the chance percentage

            if (sumChance >= chance)
            {
                possibleChance[i].Item2();
                    //wat we doing here is calling our function
                break;
            }
        }
        
    }

    #region encouraging actions
    //if our attack chance goes up, our other chances go down
    public void EncourageAttack()
    {
        //increase our likelihood to attack
        AttackChance += encouragementRate;

        //decrease our likelihood to take other actions
        ChokeChance -= encouragementRate / 2;
        DefenseChance -= encouragementRate / 2;

        //play particles
        if (!AttackParticles.isPlaying)
        {
            AttackParticles.Play();
        }
    }

    public void EncourageDefend()
    {
        DefenseChance += encouragementRate;

        ChokeChance -= encouragementRate / 2;
        AttackChance -= encouragementRate / 2;

        if (!DefenseParticles.isPlaying)
        {
            DefenseParticles.Play();
        }
    }

    public void EncourageMorale()
    {
        ChokeChance += encouragementRate;

        AttackChance -= encouragementRate / 2;
        DefenseChance -= encouragementRate / 2;

        if (!MoraleParticles.isPlaying)
        {
            MoraleParticles.Play();
        }
    }
    #endregion

    #region actions to pick
    void PlayerAttack()
    {
        Debug.Log("the player launches an attack");
    }

    void PlayerDefend()
    {
        Debug.Log("the player defends");
    }

    void PlayerChoke()
    {
        Debug.Log("the player chokes - what a bich");
    }

    #endregion
}