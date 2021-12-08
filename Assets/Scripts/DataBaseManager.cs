using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;
    public CharaDataSO charaDataSO;
    public AttackRangeSizeSO attackRangeSizeSO;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// AttackRangeType ���� BoxColider �p�� Size ���擾
    /// </summary>
    /// <param name="attackRangeType"></param>
    /// <returns></returns>
    public Vector2 GetAttackRangeSize(AttackRangeType attackRangeType)
    {
        return attackRangeSizeSO.attackRangeSizesList.Find(x => x.attackRangeType == attackRangeType).size;
    }
}