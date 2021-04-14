using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float timeBefore_SelfDescruct = 5f;

    void Start()
    {
        StartCoroutine(SelfDestructPlease());
    }

    IEnumerator SelfDestructPlease()
    {
        yield return new WaitForSeconds(timeBefore_SelfDescruct);

        Destroy(this.gameObject);
    }
}
