using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private int attackCount;
    [SerializeField]
    private Text countText;
    [SerializeField]
    private BoxCollider2D attackRangeArea;
    [SerializeField]
    private CharaData charaData;

    private GameManager gameManager;
    private Animator anim;
    private string overrideClipName = "Chara_0";
    private AnimatorOverrideController overrideController;


    private void Start()
    {
        countText.text = attackCount.ToString();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isAttack && !enemy)
        {
            //Debug.Log("標的確認");
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

    /// <summary>
    /// 攻撃準備
    /// </summary>
    /// <returns></returns>
    public IEnumerator PrepareteAttack()
    {
        Debug.Log("攻撃準備開始");
        int timer = 0;
        while (isAttack && attackCount > 0)
        {
            if (gameManager.currentGameState == GameManager.GameState.Play)
            {
                timer++;
                if (timer > attackInterval)
                {
                    timer = 0;
                    Attack();
                }
            }
            yield return null;
        }
        if (attackCount <= 0) CharaDestroy();
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    private void Attack()
    {
        Debug.Log("攻撃");
        enemy.CulcDamage(attackPower);
        attackCount--;
        countText.text = attackCount.ToString();
    }

    private void CharaDestroy()
    {
        gameManager.RemoveCharasList(this);
    }

    /// <summary>
    /// キャラデータを設定する
    /// </summary>
    /// <param name="chara"></param>
    public void SetCharaData(CharaData chara ,GameManager gameManager)
    {
        this.charaData = chara;
        this.gameManager = gameManager;

        attackPower = chara.attackPower;
        attackInterval = chara.intervalAttackTime;
        attackCount = chara.maxAttackCount;
        attackRangeArea.size = DataBaseManager.instance.GetAttackRangeSize(chara.attackRange);
        countText.text = attackCount.ToString();
        SetUpAnimation();
    }

    /// <summary>
    /// Motion に登録されている AnimationClip を変更
    /// </summary>
    private void SetUpAnimation()
    {
        if (TryGetComponent(out anim))
        {
            overrideController = new AnimatorOverrideController();
            overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;
            anim.runtimeAnimatorController = overrideController;

            AnimatorStateInfo[] layerInfo = new AnimatorStateInfo[anim.layerCount];
            for (int i = 0; i < anim.layerCount; i++)
            {
                layerInfo[i] = anim.GetCurrentAnimatorStateInfo(i);
            }

            overrideController[overrideClipName] = this.charaData.charaAnim;
            anim.runtimeAnimatorController = overrideController;
            anim.Update(0.0f);

            for (int i = 0; i < anim.layerCount; i++)
            {
                anim.Play(layerInfo[i].fullPathHash, i, layerInfo[i].normalizedTime);
            }
        }
    }
}
