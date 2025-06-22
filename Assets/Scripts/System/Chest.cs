using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject ChestUI;
    [SerializeField] GameObject conffeti;
    Image ItemImage;
    Text ItemName;
    [SerializeField] Sprite imageDrone;
    [SerializeField] Sprite imageAnpoule;
    Vector2 conffetiPos;
    List<int> numbers = new List<int>();
    int argmentrnd;
    Vector2 InitialPos;

    // Start is called before the first frame update
    void Start()
    {
        ChestUI = GameObject.Find("ChestUI");
        conffetiPos = new Vector2(0, 0);
        ItemImage = GameObject.Find("ItemImage").GetComponent<Image>();
        ItemName = GameObject.Find("ItemName").GetComponent<Text>();
        InitialPos = this.transform.position;
        if (InitialPos.x < -3)
        {
            InitialPos.x = -2.3f;
            this.transform.position = InitialPos;
        }
        if (InitialPos.x > 3)
        {
            InitialPos.x = 2.3f;
            this.transform.position = InitialPos;
        }
        if (InitialPos.y < -5)
        {
            InitialPos.y = -4.5f;
            this.transform.position = InitialPos;
        }
        if (InitialPos.y > 5)
        {
            InitialPos.y = 4.5f;
            this.transform.position = InitialPos;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            argmentrnd = Random.Range(0, 2);
            if (argmentrnd == 0)
            {
                ItemImage.sprite = imageDrone;
                ItemName.text = ("Drone");
            }
            if (argmentrnd == 1)
            {
                ItemImage.sprite = imageAnpoule;
                ItemName.text = ("Anpoule");
            }
            ChestUI.gameObject.GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
            var confe = Instantiate(conffeti, conffetiPos, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}