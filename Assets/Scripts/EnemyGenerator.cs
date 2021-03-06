using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyControllerPrefab;
    [SerializeField]
    private PathData[] pathDatas;
    [SerializeField]
    private DrawPathLine pathLinePrefab;

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
            if (this.gameManager.currentGameState == GameManager.GameState.Play)
            {
                timer++;
                if (timer > gameManager.generateIntevalTine)
                {
                    timer = 0;
                    gameManager.AddEnemyList(GenerateEnemy());
                    gameManager.JudgeGenerateEnemysEnd();
                }
            }
            yield return null;
        }
    }
   
    public EnemyController GenerateEnemy()
    {
        int randomValue = Random.Range(0, pathDatas.Length);

        EnemyController enemyController = 
            Instantiate(enemyControllerPrefab, pathDatas[randomValue].generateTran.position, Quaternion.identity);

        Vector3[] paths = pathDatas[randomValue].pathTranArray.Select(x => x.position).ToArray();
        enemyController.SetUpEnemyController(paths, gameManager);
        StartCoroutine(PreparateCreatePathLine(paths, enemyController));
        return enemyController;
    }

    private IEnumerator PreparateCreatePathLine(Vector3[] paths, EnemyController enemyController)
    {
        yield return StartCoroutine(CreatePathLine(paths));
        yield return new WaitUntil(() => gameManager.currentGameState == GameManager.GameState.Play);
        enemyController.ResumeMove();
    }

    private IEnumerator CreatePathLine(Vector3[] paths)
    {
        List<DrawPathLine> drawPathLinesList = new List<DrawPathLine>();

        for(int i = 0; i < paths.Length - 1; i++)
        {
            DrawPathLine drawPathLine = Instantiate(pathLinePrefab, transform.position, Quaternion.identity);
            Vector3[] drawPaths = new Vector3[2] { paths[i], paths[i + 1] };
            drawPathLine.CreatePathLine(drawPaths);
            drawPathLinesList.Add(drawPathLine);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < drawPathLinesList.Count; i++)
        {
            Destroy(drawPathLinesList[i].gameObject);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
