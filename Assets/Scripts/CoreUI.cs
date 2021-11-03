using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CoreUI : MonoBehaviour
{
    [SerializeField]
    private Text tairyoku;
    [SerializeField]
    private Slider slider;

    public void HitPointManager(int Cur,int Max)
    {
        tairyoku.text = Cur+" / "+Max;
        slider.maxValue = Max;
        slider.value = Cur;
    }
}
