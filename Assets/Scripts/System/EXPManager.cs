using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPManager : MonoBehaviour
{
    [SerializeField] Text ExpText;
    [SerializeField] GameObject LvelUppanelUI;
    [SerializeField] Text LevelText;
    [SerializeField] Text ItemName;
    [SerializeField] Transform PlayerTrans;
    [SerializeField] GameObject particle;
    [SerializeField] Text LevelUpText;
    [SerializeField] GameObject Player;
    public static int currentExp;
    public static int currentLv;
    int NextLv;
    int NeedExp;
    int CumExp;
    public int[,] EXP = {
        {1,0},
        {2,6},
        {3,8},
        {4,12},
        {5,18},
        {6,26},
        {7,36},
        {8,48},
        {9,52},
        {10,68},
        {11,76},
        {12,96},
        {13,116},
        {14,138}
    };
    Vector2 PlayerPos;
    public Slider EXPbar;
    public static EXPManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null){
            instance = this;
        }
        currentExp = 0;
        currentLv = 1;
        NextLv = currentLv + 1;
        NeedExp = EXP[currentLv,1];
        if(EXPbar != null){
            EXPbar.maxValue = NeedExp;
            EXPbar.value = currentExp;
        }
    }
    void Awake()
    {
        if(instance == null){
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(currentExp >= EXP[currentLv,1]){
            currentLv += 1;
            NextLv += 1;
            NeedExp = EXP[currentLv,1];
            LevelText.text = "Level"+currentLv.ToString();
            StartCoroutine(LevelUP());
        }   
    }
    private IEnumerator LevelUP(){
        PlayerPos = Player.transform.position;
        currentExp = 0;
        EXPbar.maxValue = NeedExp;
        EXPbar.value = currentExp;
        var coffeti = Instantiate(particle,PlayerPos,transform.rotation);
        leveluppanel();
        LevelUpText.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(1);
        LevelUpText.GetComponent<Text>().enabled = false;
    }
    public void leveluppanel(){
        LvelUppanelUI.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
    }
    public void ExpBarDraw(){
        CumExp++;
        currentExp++;
        EXPbar.value = currentExp;
    }
}
