using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Git : MonoBehaviour
{
    [SerializeField] int test = 0;
    private float test2;
    // Start is called before the first frame update
    void Start()
    {
        test2 = 0f;   
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizonal") == 1)
        {
            Debug.Log(test2); 
        }
  
    }
}
