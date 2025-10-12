using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBarContoroller : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private TMP_Text nameText;

    public void InitializeHPbar(float MaxHP, string enemyName)
    {
        hpSlider.maxValue = MaxHP;
        hpSlider.value = MaxHP;

        nameText.text = enemyName;
    }

    public void UpdateHP(float currentHP)
    {
        hpSlider.value = currentHP;
    }
}
