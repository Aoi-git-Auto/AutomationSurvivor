using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUPPanel : MonoBehaviour
{
    [SerializeField] GameObject LevelUPUI;
    [SerializeField] GameObject drone;
    [SerializeField] GameObject anpoule;

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
        LevelUPUI.SetActive(false);
    }
}
