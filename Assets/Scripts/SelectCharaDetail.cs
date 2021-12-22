using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectCharaDetail : MonoBehaviour
{
    [SerializeField]
    private Button btnSelectCharaDetail;
    [SerializeField]
    private Image imgChara;

    private PlacementCharaSelectPopUp placementCharaSelectPopUp;
    private CharaData charaData;

    public void SetUpSelectCharaDetail(PlacementCharaSelectPopUp placementCharaSelectPopUp, CharaData charaData)
    {
        this.placementCharaSelectPopUp = placementCharaSelectPopUp;
        this.charaData = charaData;
        ChangeActiveButton(false);
        imgChara.sprite = this.charaData.charaSprite;
        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);
        ChangeActiveButton(JudgePermissionCost(GameData.instance.currency));
    }

    private void OnClickSelectCharaDetail()
    {
        // TODO アニメ演出
        placementCharaSelectPopUp.SetSelectCharaDetail(charaData);
    }

    /// <summary>
    /// ボタンのアクティブ切り替え
    /// </summary>
    /// <param name="isSwitch"></param>
    public void ChangeActiveButton(bool isSwitch)
    {
        btnSelectCharaDetail.interactable = isSwitch;
    }

    /// <summary>
    /// コストが支払えるかの確認
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool JudgePermissionCost(int value)
    {
        Debug.Log("コスト確認");
        if (charaData.cost <= value)
        {
            ChangeActiveButton(true);
            return true;
        }
        return false;
    }

    /// <summary>
    /// ボタンの状態の取得
    /// </summary>
    /// <returns></returns>
    public bool GetActiveButtonState()
    {
        return btnSelectCharaDetail.interactable;
    }

    /// <summary>
    /// CharaData の取得
    /// </summary>
    /// <returns></returns>
    public CharaData GetCharaData()
    {
        return charaData;
    }
}