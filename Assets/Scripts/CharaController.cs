using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    [SerializeField, Header("攻撃力")]
    private int attackPower = 1;
    [SerializeField, Header("攻撃するまでの待機時間")]
    private float attackInterval = 60.0f;
    [SerializeField]
    private bool isAttack;
    [SerializeField]
    private EnemyController enemy;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isAttack && !enemy)
        {
            Debug.Log("標的確認");
            //Destroy(collision.gameObject);
            if (collision.gameObject.TryGetComponent(out enemy))
            {
                isAttack = true;
                StartCoroutine(PrepareteAttack());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Debug.Log("標的なし");
            isAttack = false;
            enemy = null;
        }
        
    }

    public IEnumerator PrepareteAttack()
    {
        Debug.Log("攻撃準備開始");
        int timer = 0;
        while (isAttack)
        {
            timer++;
            if (timer > attackInterval)
            {
                timer = 0;
                Attack();
            }
            yield return null;
        }
    }

    private void Attack()
    {
        Debug.Log("攻撃");
        enemy.CulcDamage(attackPower);
    }
}
