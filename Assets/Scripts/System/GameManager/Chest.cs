using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private GameObject chestUI;
    private GameObject canvas;

    void Start()
    {
        canvas = GameObject.Find("UICanvas");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            Instantiate(chestUI, canvas.transform);
            Destroy(this.gameObject);
        }
    }
}