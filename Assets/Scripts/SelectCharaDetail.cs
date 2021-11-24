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
        // TODO �{�^���������Ȃ���Ԃɐ؂�ւ���
        imgChara.sprite = this.charaData.charaSprite;
        // TODO �J�����V�[�̒l���X�V����闷�ɃR�X�g���x�����邩�m�F���鏈��
        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);
        // TODO �R�X�g�ɉ����ă{�^���������邩�ǂ����؂�ւ���
    }

    private void OnClickSelectCharaDetail()
    {
        // TODO �A�j�����o
        //placementCharaSelectPopUp.SetSelectCharaDetail(charaData);
    }
}
