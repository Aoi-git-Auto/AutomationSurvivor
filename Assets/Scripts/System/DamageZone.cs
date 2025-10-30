using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private float damageTime = 1f;

    [SerializeField]
    private AudioClip poisenedSE;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(this.gameObject, 5f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Enemy")){
            damageTime += Time.deltaTime;
            if (damageTime > 1f)
            {
                audioSource.PlayOneShot(poisenedSE);
                other.gameObject.GetComponent<IEnemy>().Damage(2f);
                damageTime = 0f;
            }
        }
    }
}
