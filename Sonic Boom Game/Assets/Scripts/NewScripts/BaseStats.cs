using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseStats : MonoBehaviour
{
    public enum WhoAmI
    {
        ORCUS,
        ENEMY
    }
    public WhoAmI whoamI;

    [Header("HP")]
    public float maxHP = 100f;
    public float curHP;
    public Image healthBarImage;

    [Header("STATS?")]
    public float attack = 10f;
    public float defense = 0;
    public float speed = 0;

    public bool defending = false;

    AudioSource theAudioSource;
    [SerializeField] AudioClip hurtSound, tauntSound;

    GameObject theGame = null;

    //[SerializeField] GameObject myDeadBody = null;
    //[SerializeField] GameObject myLivingBody = null;

    private void Start()
    {
        theGame = GameObject.Find("TheGame");

        theAudioSource = GetComponent<AudioSource>();

        curHP = maxHP;

        healthBarImage.fillAmount = curHP / maxHP;
    }

    private void FixedUpdate()
    {
        healthBarImage.fillAmount = curHP / maxHP;
    }

    public void TakeDamage(float damage)
    {
        //shake screen
        StartCoroutine(Shake());

        //if we're not defending, take full damage
        if (!defending)
        {
            curHP -= damage;

            //playsound
            theAudioSource.PlayOneShot(hurtSound);
        }


        //else, if we're defending, then check if your defense is successful
        else if (defending)
        {
            //if your defense is successful, take 0 damage
            if (CheckSuccessfulDefend())
            {
                curHP -= 0;

                defending = false;

                //play block sound

                //playsound
                theAudioSource.PlayOneShot(hurtSound);
            }
            else
            {
                //if your defense isn't successful, take half damage - still better than nothing, eh?
                curHP -= damage / 2;

                defending = false;

                //play block sound
                theAudioSource.PlayOneShot(hurtSound);

                //play taunt sound
                theAudioSource.PlayOneShot(tauntSound);
            }
        }



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

        //myDeadBody.SetActive(true);

        //myLivingBody.SetActive(false);

        StartCoroutine(EndTheGame());
    }

    bool CheckSuccessfulDefend()
    {
        float defenseRoll = Random.Range(0f, 100f);

        if(defenseRoll >= 25)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator Shake()
    {
        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y, theGame.transform.position.z);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y + 0.05f, theGame.transform.position.z + 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x - 0.1f, theGame.transform.position.y - 0.05f, theGame.transform.position.z - 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y, theGame.transform.position.z);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y + 0.05f, theGame.transform.position.z + 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x - 0.1f, theGame.transform.position.y - 0.05f, theGame.transform.position.z - 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y, theGame.transform.position.z);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y + 0.05f, theGame.transform.position.z + 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x - 0.1f, theGame.transform.position.y - 0.05f, theGame.transform.position.z - 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y, theGame.transform.position.z);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y + 0.05f, theGame.transform.position.z + 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x - 0.1f, theGame.transform.position.y - 0.05f, theGame.transform.position.z - 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y, theGame.transform.position.z);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y + 0.05f, theGame.transform.position.z + 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x - 0.1f, theGame.transform.position.y - 0.05f, theGame.transform.position.z - 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y, theGame.transform.position.z);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y + 0.05f, theGame.transform.position.z + 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x - 0.1f, theGame.transform.position.y - 0.05f, theGame.transform.position.z - 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y, theGame.transform.position.z);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y + 0.05f, theGame.transform.position.z + 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x - 0.1f, theGame.transform.position.y - 0.05f, theGame.transform.position.z - 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y, theGame.transform.position.z);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y + 0.05f, theGame.transform.position.z + 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x - 0.1f, theGame.transform.position.y - 0.05f, theGame.transform.position.z - 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y, theGame.transform.position.z);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y + 0.05f, theGame.transform.position.z + 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x - 0.1f, theGame.transform.position.y - 0.05f, theGame.transform.position.z - 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y, theGame.transform.position.z);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y + 0.05f, theGame.transform.position.z + 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x - 0.1f, theGame.transform.position.y - 0.05f, theGame.transform.position.z - 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y, theGame.transform.position.z);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y + 0.05f, theGame.transform.position.z + 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x - 0.1f, theGame.transform.position.y - 0.05f, theGame.transform.position.z - 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y, theGame.transform.position.z);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x + 0.05f, theGame.transform.position.y + 0.05f, theGame.transform.position.z + 0.05f);
        yield return new WaitForSeconds(0.05f);

        theGame.transform.position = new Vector3(theGame.transform.position.x - 0.1f, theGame.transform.position.y - 0.05f, theGame.transform.position.z - 0.05f);
        yield return new WaitForSeconds(0.05f);



    }

    IEnumerator EndTheGame()
    {
        switch (whoamI)
        {
            case WhoAmI.ORCUS:
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene("DeathScene_Player");
                break;

            case WhoAmI.ENEMY:
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene("DeathScene_Enemy");
                break;
        }
    }
}
