using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leap.Unity.Interaction
{
    public class GameManager : MonoBehaviour
    {
        #region don't worry about this stuff
        [HideInInspector] public enum Song_or_GameMode
        {
            STATIC,
            PLAY_YOUROWN_SOUNDS_CRIMEWAVE
        }
        public Song_or_GameMode curMode;
        #endregion

        PlayersChampion player;

        public AudioSource[] audiosource;
        
        //eventually important variables for enemies/object pooling to prevent lag
        public List<GameObject> enemies = new List<GameObject>();
        public Transform[] spawnPos;

        //leap motion shit
        Transform desktopLeapRig, interactionObjects;
        [SerializeField]Transform firePos_L, firePos_R, firePos_C;
        [SerializeField] GameObject redBullet, blueBullet, greenBullet;
        bool canFire_L, canFire_R, canFire_C;
        [SerializeField] float firingCooldown = 1.5f;

        [SerializeField] RiggedHand[] hand = new RiggedHand[2];
        Animator reticle_L, reticle_R, reticle_Center;

        private void Awake()
        {
            GetStartingComponents_LeapMotion();
        }

        void Start()
        {
            canFire_L = true;
            canFire_R = true;
            canFire_C = true;

            //our reference to our player
            player = GameObject.Find("PlayersChampion").GetComponent<PlayersChampion>();

            //StartTheGame();
        }

        private void Update()
        {
            //GetBPM();
            //GetD2BPM();
            //GetHalfBPM();
        }

        void StartTheGame()
        {
            switch (curMode)
            {
                case Song_or_GameMode.STATIC:
                    StartCoroutine(Level_Static());
                    break;

                case Song_or_GameMode.PLAY_YOUROWN_SOUNDS_CRIMEWAVE:
                    //manually set bpeerm script on this gameobject to 120

                    StartCoroutine(Level_CrimeWave());

                    break;
            }
        }

        #region Combat/Buffing Stuff

        //need references to player & stats

        public void EncourageAttack()
        {
            //play attack drum

            //increase attack chance

            Debug.Log("Encourage attack ++");
            player.EncourageAttack();
        }

        public void EncourageDefend()
        {
            //play def drum

            //increase def chance

            Debug.Log("Encourage def ++");
            player.EncourageDefend();
        }

        public void EncourageMorale()
        {
            //play morale drum

            //increase morale recharge rate

            Debug.Log("Encourage morale ++");
            player.EncourageMorale();
        }
        #endregion

        #region Firing/LeapMotionStuff

        void GetStartingComponents_LeapMotion()
        {
            if(reticle_L == null)
            {
                reticle_L = GameObject.Find("BPMPulse_L").GetComponent<Animator>();
            }

            if(reticle_R == null)
            {
                reticle_R = GameObject.Find("BPMPulse_R").GetComponent<Animator>();
            }

            if(reticle_Center == null)
            {
                reticle_Center = GameObject.Find("BPMPulse_Center").GetComponent<Animator>();
            }

            if (desktopLeapRig == null)
            {
                desktopLeapRig = GameObject.Find("Desktop Leap Rig").transform;
            }

            if (interactionObjects == null)
            {
                interactionObjects = GameObject.Find("InteractableOobjects").transform;
            }

            //left hand
            if (hand[0] == null)
            {
                hand[0] = GameObject.Find("LoPoly Rigged Hand Left").GetComponent<RiggedHand>();
            }

            //right hand
            if (hand[1] == null)
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

            if(firePos_C == null)
            {
                firePos_C = GameObject.Find("FirePos_Center").transform;
            }
        }

        public void CheckIfFired_R()
        {
            if (canFire_R)
            {
                StartCoroutine(Fire_R());

                //play sfx
                switch (curMode)
                {
                    case Song_or_GameMode.PLAY_YOUROWN_SOUNDS_CRIMEWAVE:
                        audiosource[1].Play();
                        break;
                }
            }
        }

        public void CheckIfFired_L()
        {
            if (canFire_L)
            {
                StartCoroutine(Fire_L());

                //play sfx
                switch (curMode)
                {
                    case Song_or_GameMode.PLAY_YOUROWN_SOUNDS_CRIMEWAVE:
                        audiosource[0].Play();
                        break;
                }
            }
        }

        public void CheckIfFired_C()
        {
            if (canFire_C)
            {
                StartCoroutine(Fire_C());

                //play sfx
                switch (curMode)
                {
                    case Song_or_GameMode.PLAY_YOUROWN_SOUNDS_CRIMEWAVE:
                        audiosource[2].Play();
                        break;
                }

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

        IEnumerator Fire_C()
        {
            {
                canFire_C = false;

                GameObject bul_C = Instantiate(greenBullet, firePos_C.position, Quaternion.identity);

                //Instantiate(bul_L);
                bul_C.GetComponent<Rigidbody>().AddForce(0, 0, 10f, ForceMode.Impulse);

                Debug.Log("Fire top center pos.");
                yield return new WaitForSeconds(firingCooldown);
                canFire_C = true;
            }
        }

        public void TurnLeft()
        {
            //desktopLeapRig.Rotate(0, -90, 0);
            //interactionObjects.Rotate(0, -90, 0);
            //Debug.Log("rotate on the y axis -90 deg");
        }

        public void TurnRight()
        {

        }

        #endregion

        #region Managing Sound Stuff (BGM, SFX, Spawning to music, etc)

        IEnumerator Level_Static()
        {
            StartCoroutine(SpawnRandomThing());

            #region fuck this method
            yield return new WaitForSeconds(9.25f);
            PulseRight();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.25f);
            PulseCenter();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.25f);
            PulseCenter();

            yield return new WaitForSeconds(0.25f);
            PulseLeft();

            yield return new WaitForSeconds(0.25f);
            PulseCenter();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.75f);
            PulseCenter();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.75f);
            PulseCenter();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.75f);
            PulseCenter();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.75f);
            PulseCenter();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.75f);
            PulseCenter();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.75f);
            PulseCenter();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.75f);
            PulseCenter();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.75f);
            PulseCenter();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.75f);
            PulseCenter();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.75f);
            PulseCenter();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.75f);
            PulseCenter();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.75f);
            PulseCenter();

            yield return new WaitForSeconds(0.75f);
            PulseLeft();

            yield return new WaitForSeconds(0.75f);
            PulseCenter();

            Debug.Log(Time.time.ToString());

            PulseRight();

            yield return new WaitForSeconds(0.25f);
            PulseLeft();
            yield return new WaitForSeconds(0.05f);
            PulseCenter();
            yield return new WaitForSeconds(0.25f);
            PulseLeft();
            yield return new WaitForSeconds(0.05f);
            PulseCenter();
            yield return new WaitForSeconds(0.25f);
            PulseLeft();
            yield return new WaitForSeconds(0.05f);
            PulseCenter();
            yield return new WaitForSeconds(0.25f);
            PulseLeft();
            yield return new WaitForSeconds(0.05f);
            PulseCenter();
            yield return new WaitForSeconds(0.25f);
            PulseLeft();
            yield return new WaitForSeconds(0.05f);
            PulseCenter();
            yield return new WaitForSeconds(0.25f);
            PulseLeft();
            yield return new WaitForSeconds(0.05f);
            PulseCenter();
            yield return new WaitForSeconds(0.25f);
            PulseLeft();
            yield return new WaitForSeconds(0.05f);
            PulseCenter();

            #endregion

        }

        IEnumerator Level_CrimeWave()
        {
            yield return new WaitForSeconds(0.5f);
            PulseCenter();
            PulseRight();
            PulseLeft();

            StartCoroutine(Level_CrimeWave());
        }

        IEnumerator SpawnRandomThing()
        {
            int i = Random.Range(0, 3);

            if(i == 0)
            {
                PulseRight();
            }
            else if (i == 1)
            {
                PulseCenter();
            }
            else if (i == 2)
            {
                PulseLeft();
            }
            else
            {
                Debug.Log("something went wrong");
            }

            yield return new WaitForSeconds(0.75f);

            StartCoroutine(SpawnRandomThing());
        }

        void PulseRight()
        {
            reticle_R.SetTrigger("Pulse");
        }

        void PulseLeft()
        {
            reticle_L.SetTrigger("Pulse");
        }

        void PulseCenter()
        {
            reticle_Center.SetTrigger("Pulse");
        }

        void GetBPM()
        {
            if (BPeerM.beatFull)
            {
                Debug.Log("Full beat is now");

                //what do we do on a full beat?
                reticle_L.SetTrigger("Pulse");
            }
        }

        void GetD2BPM()
        {
            if (BPeerM.beatD2)
            {
                Debug.Log("d2 beat is now");

                //do we do something on a d2?
                reticle_R.SetTrigger("Pulse");
            }
        }

        void GetHalfBPM()
        {
            if (BPeerM.beatHalf)
            {
                Debug.Log("half beat is now");

                reticle_Center.SetTrigger("Pulse");
            }
        }

        #endregion

    }
}
