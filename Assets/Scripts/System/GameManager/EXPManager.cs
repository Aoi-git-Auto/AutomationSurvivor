using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class EXPManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject LevelUppanelUI;
    [SerializeField] 
    private Text LevelText;
    [SerializeField]
    private Text LevelUpText;
    [SerializeField]
    private Slider EXPbar;
    [SerializeField]
    private List<GameObject> itemPanels = new List<GameObject>();

    private int currentExp;
    private int currentLv;
    private int NeedExp;
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
        LevelText.text = "Level" + currentLv.ToString();
        LevelUpText.enabled = false;
        LevelUppanelUI.SetActive(false);
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
        yield return new WaitForSeconds(1);
        LevelUpText.enabled = false;
        LevelUppanelUI.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void AddEXP(int get)
    {
        currentExp += get;
        EXPbar.value = currentExp;
        if(currentExp >= NeedExp)
        {
            currentLv++;
            currentExp -= NeedExp;
            NeedExp = baseExp * (int)Mathf.Pow(growth, currentLv - 1);
            EXPbar.maxValue = NeedExp;
            EXPbar.value = currentExp;
            LevelText.text = "Level "+currentLv.ToString();
            StartCoroutine(LevelUP());
        }
    }
}
