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
        if (Input.GetMouseButtonDown(0) && !placementCharaSelectPopUp.gameObject.activeSelf 
            && gameManager.currentGameState == GameManager.GameState.Play)
        {
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None)
            {
                //CreateChara(gridPos);
                ActivatePlacementCharaSelectPopUp();
            }
        }
    }

    /// <summary>
    /// /設定
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator SetUpCharaGenerator(GameManager gameManager)
    {
        this.gameManager = gameManager;
        // TODO ステージのデータを取得
        CreateHaveCharaDatasList();
        yield return StartCoroutine(CreatePlacementCharaSelectPopUp());
    }

    /// <summary>
    /// 配置キャラ選択用ポップアップの生成
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp = Instantiate(placementCharaSelectPopUpPrefab, canvasTran, false);
        placementCharaSelectPopUp.SetUpPlacementCharaSelectPopUp(this, charaDatasList);
        placementCharaSelectPopUp.gameObject.SetActive(false);

        yield return null;
    }

    /// <summary>
    /// 配置キャラ選択用ポップアップの表示
    /// </summary>
    public void ActivatePlacementCharaSelectPopUp()
    {

        gameManager.SetGameState(GameManager.GameState.Stop);
        gameManager.PauseEnemies();
        placementCharaSelectPopUp.gameObject.SetActive(true);
        placementCharaSelectPopUp.ShowPopUp();
    }

    /// <summary>
    /// 配置キャラ選択用ポップアップの非表示
    /// </summary>
    public void InactivatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp.gameObject.SetActive(false);
        if (gameManager.currentGameState == GameManager.GameState.Stop)
        {
            gameManager.SetGameState(GameManager.GameState.Play);
            gameManager.ResumeEnemies();
            // TODO カレンシーの加算処理を再開
        }

    }

    /// <summary>
    /// キャラのデータのリスト化
    /// </summary>
    private void CreateHaveCharaDatasList()
    {
        // TODO ﾌﾟﾚｲﾔｰが所持しているキャラの通し番号でリストを作成
        for (int i = 0; i < DataBaseManager.instance.charaDataSO.charaDatasList.Count; i++)
        {
            charaDatasList.Add(DataBaseManager.instance.charaDataSO.charaDatasList[i]);
        }
    }

    /// <summary>
    /// 選択したキャラを生成して配置
    /// </summary>
    /// <param name="charaData"></param>
    public void CreateChara(CharaData charaData)
    {
        CharaController chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);
        chara.SetCharaData(charaData, gameManager);
        chara.transform.position =
            new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
        Debug.Log(charaData.charaName);
    }
}
