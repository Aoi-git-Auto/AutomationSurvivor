using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LoadTimerText : MonoBehaviour
{
    [SerializeField]
    private Text TimerText;
    // Start is called before the first frame update
    void Start()
    {
        if(GMScript.instance != null)
        {
            GMScript.instance.GetTimerText(TimerText);
        }
    }
}
