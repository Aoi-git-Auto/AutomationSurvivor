using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] StatusData statusdata;
    Vector3 worldAngle;
    public SpriteRenderer spriteRenderer;
    private float currentTime = 0.0f;
    [SerializeField] GameObject normalATK;
    [SerializeField] Sprite imageIdle;
    [SerializeField] Sprite imagenomalATK;
    private Rigidbody2D rb;
    private Vector2 inputAxis;
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        spriteRenderer.sprite = imageIdle;
        normalATK.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frameb
    void Update()
    {
        inputAxis.x = Input.GetAxisRaw("Horizontal");
        inputAxis.y = Input.GetAxisRaw("Vertical");
        currentTime += Time.deltaTime;
        if (currentTime > statusdata.SPAN)
        {
            spriteRenderer.sprite = imagenomalATK;
            normalATK.GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine(N_ATKswitch());
            currentTime = 0.0f;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = inputAxis.normalized * statusdata.SPEED;
    }
    IEnumerator N_ATKswitch()
    {
        yield return new WaitForSeconds(5);
        spriteRenderer.sprite = imageIdle;
        normalATK.GetComponent<BoxCollider2D>().enabled = false;
    }
}
