using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffenceBace : MonoBehaviour
{
    [SerializeField,Header("拠点初期体力")]
    private int CoreMaxHP = 15;
    private int CoreCurrentHP;
    private int EnemyDamage;

    [SerializeField]
    private CoreUI coreUI;

    private void Start()
    {
        CoreCurrentHP = CoreMaxHP;
        coreUI.HitPointManager(CoreCurrentHP, CoreMaxHP);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            EnemyDamage = enemyController.atp;
            CoreCurrentHP = CoreCurrentHP - EnemyDamage;
            coreUI.HitPointManager(CoreCurrentHP, CoreMaxHP);
            Debug.Log("残り体力 : "+CoreCurrentHP);

            //敵キャラの破壊
            enemyController.DestroyEnemy();

            //ダメージ演出
            //TODO メゾットを作っておく:ゲームオーバー処理
            if(CoreCurrentHP <= 0)
            {
                Debug.Log("Game Over...");
            }
        }
    }

    
}
