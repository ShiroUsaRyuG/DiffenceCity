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
        // TODO ボタンを押せない状態に切り替える
        imgChara.sprite = this.charaData.charaSprite;
        // TODO カレンシーの値が更新される旅にコストが支払えるか確認する処理
        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);
        // TODO コストに応じてボタンを押せるかどうか切り替える
    }

    private void OnClickSelectCharaDetail()
    {
        // TODO アニメ演出
        //placementCharaSelectPopUp.SetSelectCharaDetail(charaData);
    }
}
