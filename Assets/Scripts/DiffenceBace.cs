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
            EnemyDamage = collision.gameObject.GetComponent<EnemyController>().atp;
            CoreCurrentHP = CoreCurrentHP - EnemyDamage;
            coreUI.HitPointManager(CoreCurrentHP, CoreMaxHP);
            Debug.Log("�c��̗� : "+CoreCurrentHP);

            //�G�L�����̔j��
            Destroy(collision.gameObject, 0.5f) ;

            //�_���[�W���o
            //TODO ���]�b�g������Ă���:�Q�[���I�[�o�[����
            if(CoreCurrentHP <= 0)
            {
                Debug.Log("Game Over...");
            }
        }
    }

    
}
