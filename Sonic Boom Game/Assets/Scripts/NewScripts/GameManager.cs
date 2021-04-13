using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leap.Unity.Interaction
{
    public class GameManager : MonoBehaviour
    {
        [Header("Handle Enemies")]
        public List<GameObject> enemies = new List<GameObject>();
        public Transform[] spawnPos;

        [Header("LeapMotionStuff")]
        Transform firePos_L, firePos_R;
        [SerializeField] GameObject redBullet, blueBullet;
        bool canFire_L, canFire_R;
        [SerializeField] float firingCooldown = 1.5f;

        [SerializeField] RiggedHand[] hand = new RiggedHand[2];
        

        private void Awake()
        {
            //left hand
            if(hand[0] == null)
            {
                hand[0] = GameObject.Find("LoPoly Rigged Hand Left").GetComponent<RiggedHand>();
            }

            //right hand
            if(hand[1] == null)
            {
                hand[1] = GameObject.Find("LoPoly Rigged Hand Right").GetComponent<RiggedHand>();
            }   

            if (firePos_L == null)
            {
                //firePos_L = GameObject.Find("L_Palm").transform;
                firePos_L = GameObject.Find("FirePos_L").transform;
            }

            if (firePos_R == null)
            {
                //firePos_R = GameObject.Find("R_Palm").transform;
                firePos_R = GameObject.Find("FirePos_R").transform;
            }
        }

        void Start()
        {
            canFire_L = true;
            canFire_R = true;
        }

        public void CheckIfFired_R()
        {
            if (canFire_R)
            {
                StartCoroutine(Fire_R());
            }
        }

        public void CheckIfFired_L()
        {
            if (canFire_L)
            {
                StartCoroutine(Fire_L());
            }
        }

        IEnumerator Fire_R()
        {
            canFire_R = false;

            GameObject bul_R = Instantiate(redBullet, firePos_R.position, Quaternion.identity);

            //Instantiate(bul_R);
            bul_R.GetComponent<Rigidbody>().AddForce(0, 0, 10f, ForceMode.Impulse);

            Debug.Log("Fire from your right hand.");
            yield return new WaitForSeconds(firingCooldown);
            canFire_R = true;
        }

        IEnumerator Fire_L()
        {
            canFire_L = false;

            GameObject bul_L = Instantiate(blueBullet, firePos_L.position, Quaternion.identity);

            //Instantiate(bul_L);
            bul_L.GetComponent<Rigidbody>().AddForce(0, 0, 10f, ForceMode.Impulse);

            Debug.Log("Fire from your left hand.");
            yield return new WaitForSeconds(firingCooldown);
            canFire_L = true;
        }

    }
}
