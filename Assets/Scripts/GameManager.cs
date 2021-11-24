using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;
    [SerializeField]
    private CharaGenerator charaGenerator;

    public bool isEnemyGenerate;
    public int generateIntevalTine;
    public int generateEnemyCount;
    public int maxEnemyCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));
        isEnemyGenerate = true;
        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));
    }

    public void AddEnemyList()
    {
        // TODO “G‚Ìî•ñ‚ðList‚É’Ç‰Á
        generateEnemyCount++;
    }

    public void JudgeGenerateEnemysEnd()
    {
        if (generateEnemyCount >= maxEnemyCount)
        {
            isEnemyGenerate = false;
        }
    }
}
