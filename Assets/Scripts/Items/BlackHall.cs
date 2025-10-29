using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHall : MonoBehaviour
{
    [SerializeField]
    private float pullForce = 5f;

    void Start()
    {
        Destroy(this.gameObject, 2f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.attachedRigidbody;
            if (rb == null) return;

            Vector3 direction = (transform.position - other.transform.position).normalized;

            rb.AddForce(direction * pullForce);
        }
    }
}
