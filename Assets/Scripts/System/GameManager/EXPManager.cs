using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EXPManager : MonoBehaviour
{
    [SerializeField] 
    private Text LevelText;
    [SerializeField]
    private Text LevelUpText;
    [SerializeField]
    private Slider EXPbar;
    [SerializeField]
    private List<GameObject> itemPanels = new List<GameObject>();

    private List<GameObject> keepPanels = new List<GameObject>();
    private int currentExp;
    private int currentLv;
    private int NeedExp;
    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private float growth = 1.2f;
    [SerializeField]
    private int baseExp = 100;
    // Start is called before the first frame update
    void Start()
    {
        currentExp = 0;
        currentLv = 1;
        NeedExp = baseExp * (int)Mathf.Pow(growth, currentLv - 1);
        LevelText.text = "Level " + currentLv.ToString();
        LevelUpText.enabled = false;
        if (EXPbar != null)
        {
            EXPbar.maxValue = NeedExp;
            EXPbar.value = currentExp;
        }
    }

    private IEnumerator LevelUP()
    {
        currentExp = 0;
        EXPbar.maxValue = NeedExp;
        EXPbar.value = currentExp;
        LevelUpText.enabled = true;
        yield return new WaitForSeconds(0.5f);
        LevelUpText.enabled = false;
        foreach (GameObject panel in itemPanels)
        {
            var panelPrehub = Instantiate(panel, canvas.transform);
            keepPanels.Add(panelPrehub);
            ItemPanel item = panelPrehub.GetComponent<ItemPanel>();
            item.OnSelected += ClosePanel;
        }
        Time.timeScale = 0;
    }

    public void AddEXP(int get)
    {
        currentExp += get;
        EXPbar.value = currentExp;
        if (currentExp >= NeedExp)
        {
            currentLv++;
            currentExp -= NeedExp;
            NeedExp = baseExp * (int)Mathf.Pow(growth, currentLv - 1);
            EXPbar.maxValue = NeedExp;
            EXPbar.value = currentExp;
            LevelText.text = "Level " + currentLv.ToString();
            StartCoroutine(LevelUP());
        }
    }
    
    private void ClosePanel()
    {
        foreach (GameObject panel in keepPanels)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(panel.transform.DOScale(1.05f, 0.1f).SetEase(Ease.OutBack))
            .Append(panel.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack))
            .SetLink(panel)
            .OnComplete(() => {
                Destroy(panel);
                keepPanels.Remove(panel);
            })
            .SetUpdate(true);
        }
        Time.timeScale = 1;
    }
}
