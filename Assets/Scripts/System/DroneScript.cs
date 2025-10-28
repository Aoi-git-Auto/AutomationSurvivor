using UnityEngine;

public class DroneScript : MonoBehaviour
{
    [SerializeField]
    private GameObject rayPrehub;
    [SerializeField]
    private float speed = 360f;
    [SerializeField]
    private float radius = 4f;
    private float angle;
    private GameObject player;
    private float currentTime = 0f;

    [SerializeField]
    private AudioClip launchSE;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        angle = Random.Range(0f, 360f);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        angle += speed * Time.deltaTime;
        if (angle > 360f)
        {
            angle -= 360f;
        }

        float rad = angle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
        transform.position = player.transform.position + offset;

        currentTime += Time.deltaTime;
        if (currentTime > 2f)
        {
            audioSource.PlayOneShot(launchSE);
            RayGenerate();
            currentTime = 0f;
        }
    }
    
    private void RayGenerate()
    {
        Instantiate(rayPrehub,transform.position,Quaternion.identity);
    }
}
