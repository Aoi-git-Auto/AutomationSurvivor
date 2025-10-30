using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChestUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform rect;
    [SerializeField]
    private Sprite openChest;
    private GameObject expManager;
    private Image chestImage;

    [SerializeField]
    private AudioClip openSE;
    [SerializeField]
    private AudioClip dropSE;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        chestImage = GetComponent<Image>();
        expManager = GameObject.Find("ExpManager");
        audioSource = GetComponent<AudioSource>();
        DropChest();
    }
    
    private void DropChest()
    {
        Vector2 startPos = new Vector2(0, 1000);
        Vector2 endPos = Vector2.zero;
        rect.anchoredPosition = startPos;

        Sequence sq = DOTween.Sequence();
        sq.Append(rect.DOAnchorPos(endPos, 0.8f))
        .SetEase(Ease.OutBounce)
        .AppendCallback(() =>
        {
            audioSource.PlayOneShot(dropSE);
        })
        .AppendInterval(0.3f)
        .AppendCallback(() =>
        {
            audioSource.PlayOneShot(openSE);
            chestImage.sprite = openChest;
        })
        .AppendInterval(0.2f)
        .AppendCallback(() =>
        {
            expManager.GetComponent<EXPManager>().OpenPanel();
            Destroy(this.gameObject);
        })
        .SetUpdate(true)
        .SetLink(gameObject); 
    }
}
