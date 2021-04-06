using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTarget : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;

    ManageScore scoreManager;

    PlayerManager gm = null;

    Transform spawnPos = null;

    CubeManager cubeManager = null;

    private void Start()
    {
        scoreManager = GameObject.Find("GM").GetComponent<ManageScore>();

        gm = GameObject.Find("PlayerHitTrigger").GetComponent<PlayerManager>();

        cubeManager = GameObject.Find("CubeManager").GetComponent<CubeManager>();

        spawnPos = GameObject.Find("CubeSpawner").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cube"))
        {
            cubeManager.ReturnCubeToPool(other.gameObject);

            other.transform.position = spawnPos.transform.position;

            other.gameObject.SetActive(false);

            scoreManager.ResetCombo();

            Debug.Log("You missed the cube.");

            //play particles
            particles.Play();
        }
    }
}
