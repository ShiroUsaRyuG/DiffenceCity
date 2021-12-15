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
        // �X�e�[�W�̐ݒ�ƃX�e�[�W���Ƃ� PathData ��ݒ�
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
        Debug.Log("�Q�[���N���A�I");
    }
}
