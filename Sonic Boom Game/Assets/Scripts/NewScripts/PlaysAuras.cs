using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaysAuras : MonoBehaviour
{
    [SerializeField] ParticleSystem AttackAura, DefenseAura, MoraleAura;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackShot"))
        {
            AttackAura.Play();

            DefenseAura.Stop();
            MoraleAura.Stop();
        }
        else if (other.CompareTag("DefenseShot"))
        {
            DefenseAura.Play();

            AttackAura.Stop();
            MoraleAura.Stop();
        }
        else if (other.CompareTag("MoraleShot"))
        {
            MoraleAura.Play();

            DefenseAura.Stop();
            AttackAura.Stop();
        }
    }
}
