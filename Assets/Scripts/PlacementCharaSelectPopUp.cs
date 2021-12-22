using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlacementCharaSelectPopUp : MonoBehaviour
{
    [SerializeField]
    private Button btnClosePopUp;
    [SerializeField]
    private Button btnChooseChara;
    [SerializeField]
    private CanvasGroup canvasGroup;

    private CharaGenerator charaGenerator;

    [SerializeField]
    private Image imgPickupChara;
    [SerializeField]
    private Text txtPickupCharaName;
    [SerializeField]
    private Text txtPickupCharaAttackPower;
    [SerializeField]
    private Text txtPickupCharaAttackRangeType;
    [SerializeField]
    private Text txtPickupCharaCost;
    [SerializeField]
    private Text txtPickupCharaMaxAttackCount;

    [SerializeField]
    private SelectCharaDetail selectCharaDetailPrefab;
    [SerializeField]
    private Transform selectCharaDetailTran;
    [SerializeField]
    private List<SelectCharaDetail> selectCharaDetailsList = new List<SelectCharaDetail>();

    private CharaData chooseCharaData;

    /// <summary>
    /// �|�b�v�A�b�v�̐ݒ�
    /// </summary>
    /// <param name="charaGenerator"></param>
    /// <param name="haveCharaDataList"></param>
    public void SetUpPlacementCharaSelectPopUp(CharaGenerator charaGenerator, List<CharaData> haveCharaDataList)
    {
        this.charaGenerator = charaGenerator;
        canvasGroup.alpha = 0;
        SwitchActivateButtons(false);
        for (int i = 0; i < haveCharaDataList.Count; i++)
        {
            SelectCharaDetail selectCharaDetail = Instantiate(selectCharaDetailPrefab, selectCharaDetailTran, false);
            selectCharaDetail.SetUpSelectCharaDetail(this, haveCharaDataList[i]);
            selectCharaDetailsList.Add(selectCharaDetail);
            if (i == 0)
            {
                SetSelectCharaDetail(haveCharaDataList[i]);
            }
        }
        btnChooseChara.onClick.AddListener(OnClickSubmitChooseChara);
        btnClosePopUp.onClick.AddListener(OnClickClosePopUp);
        SwitchActivateButtons(true);
    }

    /// <summary>
    /// �e�{�^���̃A�N�e�B�u��Ԃ̐؂�ւ�
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwitchActivateButtons(bool isSwitch)
    {
        btnChooseChara.interactable = isSwitch;
        btnClosePopUp.interactable = isSwitch;
    }

    /// <summary>
    /// �|�b�v�A�b�v�̕\��
    /// </summary>
    public void ShowPopUp()
    {
        CheckAllCharaButtons();
        canvasGroup.DOFade(1.0f, 0.5f);
    }

    /// <summary>
    /// �z�u�{�^�����������ۂ̏���
    /// </summary>
    private void OnClickSubmitChooseChara()
    {
        if (chooseCharaData.cost > GameData.instance.currency)
        {
            return;
        }
        charaGenerator.CreateChara(chooseCharaData);
        HidePopUp();
    }

    /// <summary>
    /// �߂�{�^�����������ۂ̏���
    /// </summary>
    private void OnClickClosePopUp()
    {
        HidePopUp();
    }

    /// <summary>
    /// �|�b�v�A�b�v�̔�\��
    /// </summary>
    private void HidePopUp()
    {
        CheckAllCharaButtons();
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => charaGenerator.InactivatePlacementCharaSelectPopUp());
    }


    public void SetSelectCharaDetail(CharaData charaData)
    {
        chooseCharaData = charaData;
        imgPickupChara.sprite = charaData.charaSprite;
        txtPickupCharaName.text = charaData.charaName;
        txtPickupCharaAttackPower.text = charaData.attackPower.ToString();
        txtPickupCharaAttackRangeType.text = charaData.attackRange.ToString();
        txtPickupCharaCost.text = charaData.cost.ToString();
        txtPickupCharaMaxAttackCount.text = charaData.maxAttackCount.ToString();
    }

    private void CheckAllCharaButtons()
    {
        if (selectCharaDetailsList.Count > 0)
        {
            for (int i = 0; i < selectCharaDetailsList.Count; i++)
                selectCharaDetailsList[i].ChangeActiveButton(selectCharaDetailsList[i]
                    .JudgePermissionCost(GameData.instance.currency));
        }
    }
}
