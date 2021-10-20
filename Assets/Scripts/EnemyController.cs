using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("�ړ��o�H�̏��")]
    private PathData pathData;
    [SerializeField, Header("�ړ����x")]
    private float moveSpeed;
    [SerializeField, Header("�ő�̗�")]
    private int maxHP;
    [SerializeField]
    private int currentHP;

    private Tween tween;
    private Vector3[] paths;
    private Animator anim;
    //private Vector3 currentPos;

    public void SetUpEnemyController(Vector3[] pathsData)
    {
        currentHP = maxHP;
        TryGetComponent(out anim);

        paths = pathsData;
        tween = transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear).OnWaypointChange(ChangeAnimeDirection);

        PauseMove();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    ChangeAnimeDirection();
    //}

    void ChangeAnimeDirection(int index)
    {
        Debug.Log(index);
        if (index >= paths.Length)
        {
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", 0);
            return;
        }

        Vector3 direction = (transform.position - paths[index]).normalized;
        Debug.Log(direction);

        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);

        //currentPos = transform.position;
    }

    public void CulcDamage(int amount)
    {
        currentHP = Mathf.Clamp(currentHP -= amount, 0, maxHP);
        Debug.Log("�c��HP:" + currentHP);

        if (currentHP <= 0)
        {
            DestroyEnemy();
        }
    }

    public void DestroyEnemy()
    {
        tween.Kill();
        Destroy(gameObject);
    }

    public void PauseMove() { tween.Pause(); }

    public void ResumeMove() { tween.Play(); }
}
