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

    public void SwitchActivateButtons(bool isSwitch)
    {
        btnChooseChara.interactable = isSwitch;
        btnClosePopUp.interactable = isSwitch;
    }

    public void ShowPopUp()
    {
        // TODO 各キャラのボタン制御
        canvasGroup.DOFade(1.0f, 0.5f);
    }

    private void OnClickSubmitChooseChara()
    {
        // TODO コストの支払いが可能か最終確認
        charaGenerator.CreateChara(chooseCharaData);
        HidePopUp();
    }

    private void OnClickClosePopUp()
    {
        HidePopUp();
    }

    private void HidePopUp()
    {
        // TODO 各キャラのボタンの制御
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
}
