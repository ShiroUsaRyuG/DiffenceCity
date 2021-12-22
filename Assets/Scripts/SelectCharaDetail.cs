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
        // TODO �A�j�����o
        placementCharaSelectPopUp.SetSelectCharaDetail(charaData);
    }

    /// <summary>
    /// �{�^���̃A�N�e�B�u�؂�ւ�
    /// </summary>
    /// <param name="isSwitch"></param>
    public void ChangeActiveButton(bool isSwitch)
    {
        btnSelectCharaDetail.interactable = isSwitch;
    }

    /// <summary>
    /// �R�X�g���x�����邩�̊m�F
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool JudgePermissionCost(int value)
    {
        Debug.Log("�R�X�g�m�F");
        if (charaData.cost <= value)
        {
            ChangeActiveButton(true);
            return true;
        }
        return false;
    }

    /// <summary>
    /// �{�^���̏�Ԃ̎擾
    /// </summary>
    /// <returns></returns>
    public bool GetActiveButtonState()
    {
        return btnSelectCharaDetail.interactable;
    }

    /// <summary>
    /// CharaData �̎擾
    /// </summary>
    /// <returns></returns>
    public CharaData GetCharaData()
    {
        return charaData;
    }
}