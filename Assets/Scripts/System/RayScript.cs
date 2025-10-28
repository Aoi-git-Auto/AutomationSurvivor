using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    private GameObject[] target;
    private GameObject closeEnemy;
    [SerializeField]
    private float damage = 3f;
    [SerializeField]
    private float knockback = 1.1f;
    [SerializeField]
    private float speed = 2f;
    private float hitThreshould = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Search();   
    }

    // Update is called once per frame
    void Update()
    {
        if(closeEnemy != null)
        {
            Vector3 targetPos = closeEnemy.transform.position;
            Vector3 moveDir = (targetPos - transform.position).normalized;
            transform.rotation = Quaternion.FromToRotation(Vector3.right,moveDir);
            float distance = Vector3.Distance(transform.position,targetPos);
            if(distance < hitThreshould){
                 Hittarget(closeEnemy);
                 return ;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    private void Hittarget(GameObject enemy)
    {
        var enemyscript = enemy.GetComponent<IEnemy>();
        if (enemyscript != null)
        {
            enemyscript.Damage(damage);
            enemyscript.KnockBack(knockback);
        }
        Destroy(this.gameObject);
    }

    private void Search()
    {
        float closeDist = 100;
        target = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject t in target)
        {
            float tDist = Vector2.Distance(transform.position, t.transform.position);
            if (closeDist > tDist)
            {
                closeDist = tDist;
                closeEnemy = t;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy")){
            Hittarget(other.gameObject);
        }
    }
}
