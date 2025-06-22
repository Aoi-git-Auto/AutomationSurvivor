using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    private GameObject[] target;
    private GameObject closeEnemy;
    [SerializeField] StatusData statusdata;
    [SerializeField] float hitThreshould = 0.1f;
    Vector3 diff;
    // Start is called before the first frame update
    void Start()
    {
        Search();   
    }

    // Update is called once per frame
    void Update()
    {
        if(closeEnemy != null){
            Vector3 targetPos = closeEnemy.transform.position;
            Vector3 moveDir = (targetPos - transform.position).normalized;
            transform.rotation = Quaternion.FromToRotation(Vector3.right,moveDir);
            float distance = Vector3.Distance(transform.position,targetPos);
            if(distance < hitThreshould){
                 Hittarget(closeEnemy);
                 return ;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPos, statusdata.SPEED * Time.deltaTime);
        }   
        
        else{
            Destroy(gameObject);
        }
    }
    void Hittarget(GameObject enemy){
        var enemyscript = enemy.GetComponent<Enemy_Move>();
        if(enemyscript != null){
            enemyscript.Damage(statusdata.ATK);
            enemyscript.NockBack(statusdata.NockBack);
        }
        Destroy(gameObject);
    }
    void Search(){
        float closeDist = 100;
        target = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject t in target){
            float tDist = Vector2.Distance(transform.position,t.transform.position);
            if(closeDist > tDist){
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
