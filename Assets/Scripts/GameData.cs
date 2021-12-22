using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    [Header("�R�X�g�p�̒ʉ�")]
    public int currency;
    [Header("�J�����V�[�ő�l")]
    public int maxCurrency;
    [Header("���Z�܂ł̃C���^�[�o��")]
    public int currencyIntervalTime;
    [Header("���Z�l")]
    public int addCurrencyPoint;

    public int maxCharaPlacementCount;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }  
}