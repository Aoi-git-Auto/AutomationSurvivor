using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private float damageTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Enemy")){
            damageTime += Time.deltaTime;
            if (damageTime > 1f)
            {
                other.gameObject.GetComponent<IEnemy>().Damage(0.5f);
                damageTime = 0f;
            }
        }
    }
}
