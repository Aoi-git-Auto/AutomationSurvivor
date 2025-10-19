using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    private RectTransform rectTransform;
    private GameObject player;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        element = itemData.ITEMS[UnityEngine.Random.Range(0, itemData.ITEMS.Count)];
        itemName.text = element.NAME;
        itemInfo.text = element.INFO;
        itemImage.sprite = element.SPRITE;

        Vector2 endPos = rectTransform.anchoredPosition;

        rectTransform.anchoredPosition = endPos + new Vector2(0, 800f);

        rectTransform.DOAnchorPosY(endPos.y, 0.6f)
        .SetEase(Ease.OutBack)
        .SetLink(gameObject)
        .SetUpdate(true);
    }

    public void OnClick()
    {
        Instantiate(element.PREHUB, player.transform.position, Quaternion.identity);
        OnSelected?.Invoke();
    }
}
