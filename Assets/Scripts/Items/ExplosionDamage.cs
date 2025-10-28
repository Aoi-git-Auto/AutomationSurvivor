using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    [SerializeField]
    private float damage = 15f;
    [SerializeField]
    private float knockback = 1.5f;

    [SerializeField]
    private AudioClip explosionSE;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(explosionSE);
        Destroy(this.gameObject, 0.12f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<IEnemy>().Damage(damage);
            other.GetComponent<IEnemy>().KnockBack(knockback);
        }
    }
}
