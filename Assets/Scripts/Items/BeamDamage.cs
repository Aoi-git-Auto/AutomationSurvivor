using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDamage : MonoBehaviour
{
    [SerializeField]
    private float damage = 30f;

    void Start()
    {
        Destroy(this.gameObject, 2f);
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IDamageable>().Damage(damage);
        }
    }
}
