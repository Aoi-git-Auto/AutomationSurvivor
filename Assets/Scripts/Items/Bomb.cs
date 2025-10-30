using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrehub;
    private float explosionTime = 0f;

    // Update is called once per frame
    void Update()
    {
        explosionTime += Time.deltaTime;
        if (explosionTime > 3f)
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Explode();
        }
    }
    
    private void Explode()
    {
        Instantiate(explosionPrehub, transform.position, Quaternion.identity);
        explosionTime = 0f;
        Destroy(this.gameObject);
    }
}
