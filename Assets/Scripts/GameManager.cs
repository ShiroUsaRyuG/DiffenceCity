using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    public bool isEnemyGenerate;
    public int generateIntevalTine;
    public int generateEnemyCount;
    public int maxEnemyCount;

    // Start is called before the first frame update
    void Start()
    {
        isEnemyGenerate = true;
        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));
    }

    public void AddEnemyList()
    {
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
