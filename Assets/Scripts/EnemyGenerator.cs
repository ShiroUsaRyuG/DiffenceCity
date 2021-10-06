using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyControllerPrefab;
    [SerializeField]
    private PathData pathData;

    public bool isEnemyGenerate;
    public int generateIntervalTime;
    public int generateEnemyCount;
    public int maxEnemyCount;

    // Start is called before the first frame update
    void Start()
    {
        isEnemyGenerate = true;
        StartCoroutine(PreparateEnemyGenerate());
    }

    public IEnumerator PreparateEnemyGenerate()
    {
        int timer = 0;
        while (isEnemyGenerate)
        {
            timer++;
            GenerateEnemy();
            generateEnemyCount++;
            if (generateEnemyCount >= maxEnemyCount)
            {
                isEnemyGenerate = false;
            }
        }
        yield return null;
    }

    public void GenerateEnemy()
    {
        EnemyController enemyController = 
            Instantiate(enemyControllerPrefab, pathData.generateTran.position, Quaternion.identity);
    }
}
