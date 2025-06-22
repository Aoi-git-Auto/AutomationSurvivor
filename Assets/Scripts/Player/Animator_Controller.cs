using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Controller : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector = new Vector2(
            (int)Input.GetAxisRaw("Horizontal"),
            (int)Input.GetAxisRaw("Vertical"));
        setStateToAnimator(vector : vector != Vector2.zero ? vector : (Vector2?) null);
    }
    private void setStateToAnimator(Vector2? vector)
    {
        if(!vector.HasValue)
        {
            this.animator.speed = 0.0f;
            return ;
        }
        this.animator.speed = 1.0f;
        this.animator.SetFloat("InputX",vector.Value.x);
        this.animator.SetFloat("InputY",vector.Value.y);
    }
    private Vector2? actionKeyDown()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)) return Vector2.up;
        if(Input.GetKeyDown(KeyCode.LeftArrow)) return Vector2.left;
        if(Input.GetKeyDown(KeyCode.DownArrow))  return Vector2.down;
        if(Input.GetKeyDown(KeyCode.RightArrow)) return Vector2.right;
        return null;
    }
}
