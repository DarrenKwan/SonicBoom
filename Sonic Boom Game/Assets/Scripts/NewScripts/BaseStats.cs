using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseStats : MonoBehaviour
{
    [Header("HP")]
    public float maxHP = 100f;
    public float curHP;
    public Image healthBarImage;

    [Header("STATS?")]
    public float attack = 10f;
    public float defense = 0;
    public float speed = 0;

    private void Start()
    {
        curHP = maxHP;

        healthBarImage.fillAmount = curHP / maxHP;
    }

    private void FixedUpdate()
    {
        healthBarImage.fillAmount = curHP / maxHP;
    }

    public void TakeDamage(float damage)
    {
        curHP -= damage;

        if(curHP <= 0)
        {
            //die
            PleaseDie();
        }
    }

    void PleaseDie()
    {
        Debug.Log(this.gameObject.name + (" died... a tragedy, really."));
        //play some animation?
        //win the game?
    }
}
