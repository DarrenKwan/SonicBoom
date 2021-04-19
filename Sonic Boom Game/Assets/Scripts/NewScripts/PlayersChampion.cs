using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayersChampion : MonoBehaviour
{
    //particles
    [SerializeField] ParticleSystem AttackParticles, DefenseParticles, MoraleParticles;

    //image to convey when your player will do something
    [SerializeField] Image moraleImage;

    //player stats
    BaseStats myBaseStats;
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

    //reference to enemy - how we affect the enemy
    [SerializeField] GameObject BigusOrcus_TheFoul;

    AudioSource theAudioSource;
    [SerializeField] AudioClip attackSound, defendSound, chokeSound;

    Animator animator;

    [SerializeField] TextMeshProUGUI att_chance_text, def_chance_text, choke_chance_text;

    void Start()
    {
        myBaseStats = GetComponent<BaseStats>();

        theAudioSource = GetComponent<AudioSource>();

        //our percentage chances
        AttackChance = 33;
        DefenseChance = 33;
        ChokeChance = 34;

        ResetOurTuples();

        //filling meter
        moraleImage.fillAmount = curMorale / maxMorale;

        animator = GetComponentInChildren<Animator>();
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

    private void FixedUpdate()
    {
        Update_ChanceText();
    }

    void ChooseAction()
    {
        //reset our defense stance
        myBaseStats.defending = false;

        //reset our tuples so we can get our actual rates
        ResetOurTuples();

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

        //since we've updated our chances for things, let's update the text that tells the player that
        ResetOurTuples();
        Update_ChanceText();

        ////play particles
        //if (!AttackParticles.isPlaying)
        //{
        //    AttackParticles.Play();
        //}
        //if (MoraleParticles.isPlaying)
        //{
        //    MoraleParticles.Stop();
        //}
        //if (DefenseParticles.isPlaying)
        //{
        //    DefenseParticles.Stop();
        //}
    }

    public void EncourageDefend()
    {
        DefenseChance += encouragementRate;

        ChokeChance -= encouragementRate / 2;
        AttackChance -= encouragementRate / 2;

        //since we've updated our chances for things, let's update the text that tells the player that
        ResetOurTuples();
        Update_ChanceText();

            //if (!DefenseParticles.isPlaying)
            //{
            //    DefenseParticles.Play();
            //}
            //if (MoraleParticles.isPlaying)
            //{
            //    MoraleParticles.Stop();
            //}
            //if (AttackParticles.isPlaying)
            //{
            //    AttackParticles.Stop();
            //}
    }

    public void EncourageMorale()
    {
        curMorale += 0.1f;

        ChokeChance += encouragementRate;

        AttackChance -= encouragementRate / 2;
        DefenseChance -= encouragementRate / 2;

        //since we've updated our chances for things, let's update the text that tells the player that
        ResetOurTuples();
        Update_ChanceText();

        //if (!MoraleParticles.isPlaying)
        //{
        //    MoraleParticles.Play();
        //}
        //if (AttackParticles.isPlaying)
        //{
        //    AttackParticles.Stop();
        //}
        //if (DefenseParticles.isPlaying)
        //{
        //    DefenseParticles.Stop();
        //}
    }
    #endregion

    #region actions to pick
    void PlayerAttack()
    {
        Debug.Log("the player launches an attack");

        //need a reference to enemy & their HP bar
        BigusOrcus_TheFoul.GetComponent<BaseStats>().TakeDamage(myBaseStats.attack);
        BigusOrcus_TheFoul.GetComponentInChildren<Animator>().SetTrigger("Damage");
        //play animation

        //play sound
        theAudioSource.PlayOneShot(attackSound);

        ResetChances();
        ResetOurTuples();
        Update_ChanceText();

        animator.SetTrigger("Attack");
    }

    void PlayerDefend()
    {
        Debug.Log("the player defends");

        myBaseStats.defending = true;

        //play sound
        theAudioSource.PlayOneShot(defendSound);

        //play a defend animation

        ResetChances();
        ResetOurTuples();
        Update_ChanceText();

        animator.SetTrigger("Block");
    }

    void PlayerChoke()
    {
        Debug.Log("the player chokes - what a bich");

        //play sound
        theAudioSource.PlayOneShot(chokeSound);

        //do nothing

        ResetChances();
        ResetOurTuples();
        Update_ChanceText();

        animator.SetTrigger("Crying");
    }

    void ResetChances()
    {
        AttackChance = 34f;
        DefenseChance = 34f;
        ChokeChance = 34f;
    }

    void ResetOurTuples()
    {
        possibleChance.Clear();

        possibleChance.Add(new Tuple<float, Action>(AttackChance, PlayerAttack));
        //wat we're doing up there is binding the attack chance to our attack function
        //can call this function using our attack chance

        possibleChance.Add(new Tuple<float, Action>(DefenseChance, PlayerDefend));
        possibleChance.Add(new Tuple<float, Action>(ChokeChance, PlayerChoke));
    }

    #endregion

    void Update_ChanceText()
    {
        att_chance_text.text = possibleChance[0].Item1.ToString();
        def_chance_text.text = possibleChance[1].Item1.ToString();
        choke_chance_text.text = possibleChance[2].Item1.ToString();
    }
}
