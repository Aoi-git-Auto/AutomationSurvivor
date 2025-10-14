using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField]
    private ItemData itemData;
    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private Text itemInfo;

    private ItemStatus element;
    public Action OnSelected;

    // Start is called before the first frame update
    void Start()
    {
        element = itemData.ITEMS[UnityEngine.Random.Range(0, itemData.ITEMS.Count)];
        itemName.text = element.NAME;
        itemInfo.text = element.INFO;
        itemImage.sprite = element.SPRITE;
    }

    public void OnClick()
    {
        Instantiate(element.PREHUB, transform.position, transform.rotation);
        OnSelected?.Invoke();
    }
}
