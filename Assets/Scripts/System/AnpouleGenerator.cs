using UnityEngine;

public class AnpouleGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject AnpoulePrehub;
    private GameObject player;
    private float currentTime = 0f;
    private float span = 4f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            transform.SetParent(player.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > span){
            Instantiate(AnpoulePrehub,transform.position,Quaternion.identity);
            currentTime = 0f;
        }
    }
}
