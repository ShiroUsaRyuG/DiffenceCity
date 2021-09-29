using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("ˆÚ“®Œo˜H‚Ìî•ñ")]
    private PathData pathData;
    [SerializeField, Header("ˆÚ“®‘¬“x")]
    private float moveSpeed;

    private Vector3[] paths;
    private Animator anim;
    //private Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out anim);

        paths = pathData.pathTranArray.Select(x => x.position).ToArray();

        transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear).OnWaypointChange(ChangeAnimeDirection);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    ChangeAnimeDirection();
    //}

    void ChangeAnimeDirection(int index)
    {
        Debug.Log(index);
        if (index >= paths.Length) return;

        Vector3 direction = (paths[index] - transform.position).normalized;
        Debug.Log(direction);

        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);

        //currentPos = transform.position;
    }
}
