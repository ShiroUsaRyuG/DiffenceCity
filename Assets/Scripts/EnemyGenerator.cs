using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyControllerPrefab;
    [SerializeField]
    private PathData pathData;

    private GameManager gameManager;
    //public bool isEnemyGenerate;
    //public int generateIntervalTime;
    //public int generateEnemyCount;
    //public int maxEnemyCount;

    // Start is called before the first frame update
    /*void Start()
    {
        isEnemyGenerate = true;
        StartCoroutine(PreparateEnemyGenerate());
    }*/

    public IEnumerator PreparateEnemyGenerate(GameManager gameManager)
    {
        this.gameManager = gameManager;
        int timer = 0;
        while (gameManager.isEnemyGenerate)
        {
            timer++;
            if (timer > gameManager.generateIntevalTine)
            {
                timer = 0;
                GenerateEnemy();
                gameManager.AddEnemyList();
                gameManager.JudgeGenerateEnemysEnd();
            }
            yield return null;
        }
    }
   
    public void GenerateEnemy()
    {
        EnemyController enemyController = 
            Instantiate(enemyControllerPrefab, pathData.generateTran.position, Quaternion.identity);
    }
}
