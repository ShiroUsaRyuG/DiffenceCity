using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;
    [SerializeField]
    private CharaGenerator charaGenerator;
    [SerializeField]
    private List<EnemyController> enemiesList = new List<EnemyController>();
    [SerializeField]
    private List<CharaController> charasList = new List<CharaController>();

    public bool isEnemyGenerate;
    public int generateIntevalTine;
    public int generateEnemyCount;
    public int maxEnemyCount;
    public int destroyCount = 0;

    int timer;
    public UIManager uiManager;


    /// <summary>
    /// ゲームの状態
    /// </summary>
    public enum GameState
    {
        Preparate,
        Play,
        Stop,
        GameUp
    }

    public GameState currentGameState;


    // Start is called before the first frame update
    void Start()
    {
        SetGameState(GameState.Preparate);
        // TODO ゲームデータを初期化
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));
        // TODO 拠点の設定
        // TODO オープニング演出再生
        isEnemyGenerate = true;
        SetGameState(GameState.Play);
        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));
        StartCoroutine(CurrencyCulc());
    }

    /// <summary>
    /// 敵の情報を List に追加
    /// </summary>
    public void AddEnemyList(EnemyController enemy)
    {
        enemiesList.Add(enemy);
        generateEnemyCount++;
    }

    /// <summary>
    /// 敵の生成を停止するか判定
    /// </summary>
    public void JudgeGenerateEnemysEnd()
    {
        if (generateEnemyCount >= maxEnemyCount)
        {
            isEnemyGenerate = false;
        }
    }

    /// <summary>
    /// GameState の変更
    /// </summary>
    /// <param name="nextGameState"></param>
    public void SetGameState(GameState nextGameState)
    {
        currentGameState = nextGameState;
    }

    /// <summary>
    /// すべての敵の移動を一時停止
    /// </summary>
    public void PauseEnemies()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].PauseMove();
        }
    }

    /// <summary>
    /// すべての敵の移動を再開
    /// </summary>
    public void ResumeEnemies()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].ResumeMove();
        }
    }

    /// <summary>
    /// 敵の情報をListから削除
    /// </summary>
    /// <param name="removeEnemy"></param>
    public void RemoveEnemyList(EnemyController removeEnemy)
    {
        enemiesList.Remove(removeEnemy);
    }

    /// <summary>
    /// 時間の経過に応じてカレンシーを追加
    /// </summary>
    /// <returns></returns>
    public IEnumerator CurrencyCulc()
    {
        timer = 0;
        while (currentGameState != GameState.GameUp)
        {
            if(currentGameState == GameState.Play)
            {
                timer++;

                if (timer > GameData.instance.currencyIntervalTime && 
                    GameData. instance.currency < GameData.instance.maxCurrency)
                {
                    timer = 0;
                    GameData.instance.currency = 
                        Mathf.Clamp(GameData.instance.currency += GameData.instance.addCurrencyPoint,
                        0, GameData.instance.maxCurrency);
                    uiManager.UpdateDisplayCurrency();
                }
            }
            yield return null;
        }
    }

    /// <summary>
    /// 破壊した敵の数をカウント
    /// </summary>
    /// <param name="enemy"></param>
    public void DestroyCount(EnemyController enemy)
    {
        RemoveEnemyList(enemy);
        destroyCount++;
        if (destroyCount == maxEnemyCount) GameClear();
    }

    /// <summary>
    /// ゲームクリア判定
    /// </summary>
    public void GameClear()
    {
        Debug.Log("ゲームクリア！");
    }

    /// <summary>
    /// 選択したキャラの情報を List に追加
    /// </summary>
    /// <param name="chara"></param>
    public void AddCharasList(CharaController chara)
    {
        charasList.Add(chara);
    }

    /// <summary>
    /// 選択したキャラを破棄し、情報をListから削除
    /// </summary>
    /// <param name="chara"></param>
    public void RemoveCharasList(CharaController chara)
    {
        Destroy(chara.gameObject);
        charasList.Remove(chara);
    }

    /// <summary>
    /// 現在の配置しているキャラ数の取得
    /// </summary>
    /// <returns></returns>
    public int GetPlacementCharaCount()
    {
        return charasList.Count;
    }
}
