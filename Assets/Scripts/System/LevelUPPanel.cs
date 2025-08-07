using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUPPanel : MonoBehaviour
{
    public static LevelUPPanel instance;
    [SerializeField] GameObject LevelUPUI;
    [SerializeField] GameObject drone;
    [SerializeField] GameObject anpoule;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnclickItem1(int ItemID){
        switch(ItemID){
            case(0):
            var Drone = Instantiate(drone,transform.position,transform.rotation);
            break;
            case(1):
            
            break;
            case(2):
            var Anpoule = Instantiate(anpoule,transform.position,transform.rotation);
            break;
        }
        
        Time.timeScale = 1;
        LevelUPUI.GetComponent<Canvas>().enabled = false;
    }
}
