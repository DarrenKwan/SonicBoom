using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPos = null;

    public List<GameObject> cubePool = new List<GameObject>();

    public List<GameObject> sentCube = new List<GameObject>();

    private void Start()
    {
        //spawnPos = GameObject.Find("CubeSpawner").transform;
    }

    private void Update()
    {
        if (BPeerM.beatFull)
        {
            //spawn a cube
            SendNew_RandomCube();
        }

        //if (BPeerM.beatD8)
        //{
        //    SendNew_RandomCube();
        //}
    }

    public void SendNew_RandomCube()
    {
        int n = Random.Range(0, cubePool.Count);

        cubePool[n].SetActive(true);

        sentCube.Add(cubePool[n]);

        cubePool.Remove(cubePool[n]);
    }

    public void ReturnCubeToPool(GameObject cubeToReturn)
    {
        cubeToReturn.SetActive(false);

        int rand = Random.Range(0, spawnPos.Length);

        cubeToReturn.transform.position = spawnPos[rand].position;

        sentCube.Remove(cubeToReturn);


        cubePool.Add(cubeToReturn);
    }

    public void SendNew_Cube_Left()
    {

    }

    public void SendNew_Cube_Right()
    {

    }

    public void SendNew_Cube_Up()
    {

    }

    public void SendNew_Cube_Down()
    {

    }
}
