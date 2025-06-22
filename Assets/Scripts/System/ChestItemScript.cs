using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChestItemScript : MonoBehaviour
{
    [SerializeField] StatusData statusdata;
    [SerializeField] GameObject DronePrehub;
    [SerializeField] GameObject AnpoulePrehub;
    [SerializeField] GameObject LevelUPUI;
    public Image itemImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Onclick(){
        if (itemImage.sprite.name == "DroneImage")
        {
            var Drone = Instantiate(DronePrehub, transform.position, transform.rotation);
        }
        if(itemImage.sprite.name == "AnpouleImage"){
            var Anpoule = Instantiate(AnpoulePrehub,transform.position,transform.rotation);
        }
        if(itemImage.sprite.name == "SwordImage"){
            statusdata.ATK++;
        }
        Time.timeScale = 1;
        LevelUPUI.GetComponent<Canvas>().enabled = false;
    }
}
