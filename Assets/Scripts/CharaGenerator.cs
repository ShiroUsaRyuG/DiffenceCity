using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharaGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject charaPrefab;
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private Tilemap tilemaps;

    private Vector3Int gridPos;

    // Update is called once per frame
    void Update()
    {
        //TODO 配置できる最大キャラ数に達している場合には配置できない
        if (Input.GetMouseButtonDown(0))
        {
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None)
            {
                CreateChara(gridPos);
            }
        }
    }

    void CreateChara(Vector3Int gridPos)
    {
        GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);
        chara.transform.position =
            new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
    }
}
