using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBarContoroller : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider;

    public void InitializeHPbar(float MaxHP)
    {
        hpSlider.maxValue = MaxHP;
        hpSlider.value = MaxHP;
    }

    public void UpdateHP(float currentHP)
    {
        hpSlider.value = currentHP;
    }
}
