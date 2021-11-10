using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaController : MonoBehaviour
{
    [SerializeField, Header("�U����")]
    private int attackPower = 1;
    [SerializeField, Header("�U������܂ł̑ҋ@����")]
    private float attackInterval = 60.0f;
    [SerializeField]
    private bool isAttack;
    [SerializeField]
    private EnemyController enemy;
    [SerializeField]
    private int attackCount;
    [SerializeField]
    private Text countText; 

    private void Start()
    {
        countText.text = attackCount.ToString();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isAttack && !enemy)
        {
            Debug.Log("�W�I�m�F");
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
            Debug.Log("�W�I�Ȃ�");
            isAttack = false;
            enemy = null;
        }
        
    }

    public IEnumerator PrepareteAttack()
    {
        Debug.Log("�U�������J�n");
        int timer = 0;
        while (isAttack && attackCount > 0)
        {
            timer++;
            if (timer > attackInterval)
            {
                timer = 0;
                Attack();
            }
            yield return null;
        }
        if (attackCount <= 0) CharaDestroy();
    }

    private void Attack()
    {
        Debug.Log("�U��");
        enemy.CulcDamage(attackPower);
        attackCount--;
        countText.text = attackCount.ToString();
    }

    private void CharaDestroy()
    {
        //�L������j�󂷂�O�ɍs�������������ɋL��
        Destroy(this.gameObject);
    }
}
