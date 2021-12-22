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
    /// �Q�[���̏��
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
        // TODO �Q�[���f�[�^��������
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));
        // TODO ���_�̐ݒ�
        // TODO �I�[�v�j���O���o�Đ�
        isEnemyGenerate = true;
        SetGameState(GameState.Play);
        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));
        StartCoroutine(CurrencyCulc());
    }

    /// <summary>
    /// �G�̏��� List �ɒǉ�
    /// </summary>
    public void AddEnemyList(EnemyController enemy)
    {
        enemiesList.Add(enemy);
        generateEnemyCount++;
    }

    /// <summary>
    /// �G�̐������~���邩����
    /// </summary>
    public void JudgeGenerateEnemysEnd()
    {
        if (generateEnemyCount >= maxEnemyCount)
        {
            isEnemyGenerate = false;
        }
    }

    /// <summary>
    /// GameState �̕ύX
    /// </summary>
    /// <param name="nextGameState"></param>
    public void SetGameState(GameState nextGameState)
    {
        currentGameState = nextGameState;
    }

    /// <summary>
    /// ���ׂĂ̓G�̈ړ����ꎞ��~
    /// </summary>
    public void PauseEnemies()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].PauseMove();
        }
    }

    /// <summary>
    /// ���ׂĂ̓G�̈ړ����ĊJ
    /// </summary>
    public void ResumeEnemies()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].ResumeMove();
        }
    }

    /// <summary>
    /// �G�̏���List����폜
    /// </summary>
    /// <param name="removeEnemy"></param>
    public void RemoveEnemyList(EnemyController removeEnemy)
    {
        enemiesList.Remove(removeEnemy);
    }

    /// <summary>
    /// ���Ԃ̌o�߂ɉ����ăJ�����V�[��ǉ�
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
    /// �j�󂵂��G�̐����J�E���g
    /// </summary>
    /// <param name="enemy"></param>
    public void DestroyCount(EnemyController enemy)
    {
        RemoveEnemyList(enemy);
        destroyCount++;
        if (destroyCount == maxEnemyCount) GameClear();
    }

    /// <summary>
    /// �Q�[���N���A����
    /// </summary>
    public void GameClear()
    {
        Debug.Log("�Q�[���N���A�I");
    }

    /// <summary>
    /// �I�������L�����̏��� List �ɒǉ�
    /// </summary>
    /// <param name="chara"></param>
    public void AddCharasList(CharaController chara)
    {
        charasList.Add(chara);
    }

    /// <summary>
    /// �I�������L������j�����A����List����폜
    /// </summary>
    /// <param name="chara"></param>
    public void RemoveCharasList(CharaController chara)
    {
        Destroy(chara.gameObject);
        charasList.Remove(chara);
    }

    /// <summary>
    /// ���݂̔z�u���Ă���L�������̎擾
    /// </summary>
    /// <returns></returns>
    public int GetPlacementCharaCount()
    {
        return charasList.Count;
    }
}
