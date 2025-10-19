using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractStatusItem : MonoBehaviour
{
    protected abstract void Enhance(GameObject player);

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Enhance(other.gameObject);
        }
    }
}
