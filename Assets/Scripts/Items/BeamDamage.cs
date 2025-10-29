using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDamage : MonoBehaviour
{
    [SerializeField]
    private float damage = 30f;

    [SerializeField]
    private AudioClip beamSE;
    private AudioSource audioSource;

    void Start()
    {
        Destroy(this.gameObject, 2f);
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(beamSE);
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IDamageable>().Damage(damage);
        }
    }
}
