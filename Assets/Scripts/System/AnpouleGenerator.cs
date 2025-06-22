using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnpouleGenerator : MonoBehaviour
{
    [SerializeField] GameObject AnpoulePrehub;
    float currentTime = 0f;
    public float span = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > span){
            var anpoule = Instantiate(AnpoulePrehub,transform.position,transform.rotation);
            currentTime = 0f;
        }
    }
}
