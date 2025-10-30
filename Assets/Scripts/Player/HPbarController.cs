using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPbarController : MonoBehaviour
{
    [SerializeField] 
    private RectTransform canvasRectTfm;
    [SerializeField] 
    private Transform targetTfm;
    private RectTransform myRectTfm;
    private Vector3 offset = new Vector3(0,-0.2f,0);
    Vector2 pos;
    public Vector2 worldAngle;
    Vector3 def;
    Vector3 _parent;
    Vector3 before;
    // Start is called before the first frame update
    void Start()
    {
        myRectTfm = GetComponent<RectTransform>();
        def = transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        _parent = transform.parent.transform.localRotation.eulerAngles;
        if(before != _parent){
            transform.localRotation = Quaternion.Euler(def - _parent);
        }
        before = transform.localRotation.eulerAngles;
        
    }
}
