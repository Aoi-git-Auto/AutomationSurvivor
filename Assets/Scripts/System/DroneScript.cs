using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DroneScript : MonoBehaviour
{
    [SerializeField] private GameObject rayPrehub;
    [SerializeField] StatusData RayStatus;
    GameObject Player;
    Vector2 PlayerPos;
    Vector2 myPos;
    GameObject[] target;
    int DroneQuantity;
    float localdistance;
    private float currentTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");   
        PlayerPos = Player.transform.position;
        this.transform.parent = Player.transform;
        Search();
        PositionSet();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > RayStatus.SPAN){
            RayGenerate();
            currentTime = 0f;
        }
    }
    public void RayGenerate(){
        var ray = Instantiate(rayPrehub,transform.position,transform.rotation);
    }
    void Search(){
        target = GameObject.FindGameObjectsWithTag("Drone");
        foreach(var t in target){
            DroneQuantity = target.Length;
        }
    }
    void PositionSet(){
        localdistance = 0.8f;
        myPos = Player.transform.position;
        switch(DroneQuantity){
            case 1:
            myPos.x = localdistance;
            myPos.y = localdistance;
            transform.localPosition = myPos;
            break;
            case 2:
            myPos.x = -1*localdistance;
            myPos.y = -1*localdistance;
            transform.localPosition = myPos;
            break;
            case 3:
            myPos.x = -1*localdistance;
            myPos.y = localdistance;
            transform.localPosition = myPos;
            break;
            case 4:
            myPos.x = localdistance;
            myPos.y = -1*localdistance;
            transform.localPosition = myPos;
            break;
            case 5:
            myPos.x = 0;
            myPos.y = -5f;
            transform.localPosition = myPos;
            break;
        }
    }
}
