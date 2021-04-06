using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    ManageScore scoreManager;

    ParticleSystem particles = null;

    CubeManager cubeManager = null;

    Transform sword;

    Camera cam;

    private void Start()
    {
        scoreManager = GameObject.Find("GM").GetComponent<ManageScore>();

        particles = GameObject.Find("ExplosionParticles").GetComponent<ParticleSystem>();

        cam = GameObject.Find("Player").GetComponent<Camera>();

        sword = GameObject.Find("KatanaHolder").transform;

        if(sword == null)
        {
            Debug.Log("Couldn't find the sword.");
        }
        if (cam == null)
        {
            Debug.Log("Couldn't find the cam.");
        }

        cubeManager = GameObject.Find("CubeManager").GetComponent<CubeManager>();
    }

    private void Update()
    {
        HandleInputs();

        float zpos = cam.WorldToScreenPoint(sword.position).z;

        Vector3 newpos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zpos));

        sword.position = newpos;
    }

    void HandleInputs()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    cubeManager.SendNew_RandomCube();
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("quit the game");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cubeHit"))
        {
            //spawn particles
            particles.Play();

            cubeManager.ReturnCubeToPool(other.transform.parent.gameObject);
            Debug.Log("You hit the cube");

            scoreManager.RegisterHitCube();
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
