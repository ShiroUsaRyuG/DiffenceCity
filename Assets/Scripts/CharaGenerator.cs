using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharaGenerator : MonoBehaviour
{

    [SerializeField]
    private CharaController charaPrefab;
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private Tilemap tilemaps;
    [SerializeField]
    private PlacementCharaSelectPopUp placementCharaSelectPopUpPrefab;
    [SerializeField]
    private Transform canvasTran;
    [SerializeField, Header("キャラのデータリスト")]
    private List<CharaData> charaDatasList = new List<CharaData>();

    private PlacementCharaSelectPopUp placementCharaSelectPopUp;
    private GameManager gameManager;

    private Vector3Int gridPos;

    // Update is called once per frame
    void Update()
    {
        //TODO 配置できる最大キャラ数に達している場合には配置できない
        if (Input.GetMouseButtonDown(0) && !placementCharaSelectPopUp.gameObject.activeSelf)
        {
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None)
            {
                //CreateChara(gridPos);
                ActivatePlacementCharaSelectPopUp();
            }
        }
    }

    public IEnumerator SetUpCharaGenerator(GameManager gameManager)
    {
        this.gameManager = gameManager;
        // TODO ステージのデータを取得
        CreateHaveCharaDatasList();
        yield return StartCoroutine(CreatePlacementCharaSelectPopUp());
    }

    private IEnumerator CreatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp = Instantiate(placementCharaSelectPopUpPrefab, canvasTran, false);
        placementCharaSelectPopUp.SetUpPlacementCharaSelectPopUp(this, charaDatasList);
        placementCharaSelectPopUp.gameObject.SetActive(false);

        yield return null;
    }

    public void ActivatePlacementCharaSelectPopUp()
    {
        // TODO ゲームの進行状態をゲーム停止に変更
        // TODO すべての敵の行動を一時停止
        placementCharaSelectPopUp.gameObject.SetActive(true);
        placementCharaSelectPopUp.ShowPopUp();
    }

    public void InactivatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp.gameObject.SetActive(false);
        // TODO ゲームオーバーやゲームクリアではないときにゲームをプレイ中に変更し、ゲーム再開
        // TODO すべての敵の移動を再開
        // TODO カレンシーの加算処理を再開

    }

    private void CreateHaveCharaDatasList()
    {
        // TODO ﾌﾟﾚｲﾔｰが所持しているキャラの通し番号でリストを作成
        for (int i = 0; i < DataBaseManager.instance.charaDataSO.charaDatasList.Count; i++)
        {
            charaDatasList.Add(DataBaseManager.instance.charaDataSO.charaDatasList[i]);
        }
    }

    public void CreateChara(CharaData charaData)
    {
        CharaController chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);
        chara.SetCharaData(charaData, gameManager);
        chara.transform.position =
            new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
        Debug.Log(charaData.charaName);
    }
}
