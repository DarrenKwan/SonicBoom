using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GauntletScript : MonoBehaviour
{
    public GameObject gauntlet1Projectile;

    public GameObject gauntlet2Projectile;

    public Transform gauntlet1FirePos;

    public Transform gauntlet2FirePos;

    public BPMScript bpmScript;

    public float comboCount;

    public Text comboText;

    public float beatWindow;

    private AudioSource rightGaunletSound;

    private AudioSource leftGauntletSound;

    bool hasAttacked;

    public float timeBetweenAttacks;

    public Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        bpmScript = GameObject.FindObjectOfType<BPMScript>().GetComponent<BPMScript>();
        rightGaunletSound = GameObject.Find("rightgloves").GetComponent<AudioSource>();
        leftGauntletSound = GameObject.Find("leftgloves").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var x = Screen.width / 2;
        var y = Screen.height / 2;
        ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));
        comboText.text = "COMBO: " + comboCount;
        if (Input.GetButtonDown("Fire1") && !hasAttacked)
        {
            TrebleMajor();
            hasAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
            if (bpmScript.IsCloseEnoughToBeat(beatWindow) || bpmScript.IsCloseEnoughToBeat(-beatWindow))
            {
                comboCount += 1;
            }
            else
            {
                comboCount = 0;
            }
        }
        if (Input.GetButtonDown("Fire2") && !hasAttacked)
        {
            TrebleMinor();
            hasAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
            if (bpmScript.IsCloseEnoughToBeat(beatWindow) || bpmScript.IsCloseEnoughToBeat(-beatWindow))
            {
                comboCount += 1;
            }
            else
            {
                comboCount = 0;
            }
        }
    }

    void ResetAttack()
    {
        hasAttacked = false;
    }

    void TrebleMajor()
    {
        GameObject projectile = Instantiate(gauntlet1Projectile, gauntlet1FirePos.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(ray.direction * 50f, ForceMode.Impulse);
        Destroy(projectile, 2f);
        //Debug.Log("Click");
        rightGaunletSound.Play();
    }

    void TrebleMinor()
    {
        GameObject projectile = Instantiate(gauntlet2Projectile, gauntlet2FirePos.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(ray.direction * 50f, ForceMode.Impulse);
        Destroy(projectile, 2f);
        //Debug.Log("Click");
        leftGauntletSound.Play();
    }
}
