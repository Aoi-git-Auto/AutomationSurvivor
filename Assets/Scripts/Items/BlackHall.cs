using UnityEngine;

public class BlackHall : MonoBehaviour
{
    [SerializeField]
    private float pullForce;

    [SerializeField]
    private AudioClip hallSE;
    private AudioSource audioSource;

    void Start()
    {
        Destroy(this.gameObject, 4f);
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(hallSE);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.attachedRigidbody;
            if (rb == null) return;

            Vector3 direction = (transform.position - other.transform.position).normalized;

            rb.AddForce(direction * pullForce);
        }
    }
}
