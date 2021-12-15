using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffenceBace : MonoBehaviour
{
    [SerializeField,Header("���_�����̗�")]
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
            Debug.Log("�c��̗� : "+CoreCurrentHP);

            //�G�L�����̔j��
            enemyController.DestroyEnemy();

            //�_���[�W���o
            //TODO ���]�b�g������Ă���:�Q�[���I�[�o�[����
            if(CoreCurrentHP <= 0)
            {
                Debug.Log("Game Over...");
            }
        }
    }

    
}
