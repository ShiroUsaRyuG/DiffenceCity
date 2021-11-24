using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharaGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject charaPrefab;
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private Tilemap tilemaps;
    [SerializeField]
    private PlacementCharaSelectPopUp placementCharaSelectPopUpPrefab;
    [SerializeField]
    private Transform canvasTran;

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
        // TODO キャラのデータをリスト化
        yield return StartCoroutine(CreatePlacementCharaSelectPopUp());
    }

    private IEnumerator CreatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp = Instantiate(placementCharaSelectPopUpPrefab, canvasTran, false);
        // TODO あとでキャラ設定用の情報も渡す
        placementCharaSelectPopUp.SetUpPlacementCharaSelectPopUp(this);
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

    //void CreateChara(Vector3Int gridPos)
    //{
    //    GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);
    //    chara.transform.position =
    //        new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
    //}
}
