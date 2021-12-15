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

    public bool isEnemyGenerate;
    public int generateIntevalTine;
    public int generateEnemyCount;
    public int maxEnemyCount;
    public int destroyCount = 0;

    int timer;

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
        // ステージの設定とステージごとの PathData を設定
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

    public IEnumerator CurrencyCulc()
    {
        timer = 0;
        while (currentGameState == GameState.GameUp)
        {
            if(currentGameState == GameState.Play)
            {
                timer++;
            }
            yield return null;
        }
    }

    public void DestroyCount(EnemyController enemy)
    {
        RemoveEnemyList(enemy);
        destroyCount++;
        if (destroyCount == maxEnemyCount) GameClear();
    }

    public void GameClear()
    {
        Debug.Log("ゲームクリア！");
    }
}
