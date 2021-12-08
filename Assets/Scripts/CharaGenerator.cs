using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharaGenerator : MonoBehaviour
{

    [SerializeField]
    private CharaController charaPrefab;
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private Tilemap tilemaps;
    [SerializeField]
    private PlacementCharaSelectPopUp placementCharaSelectPopUpPrefab;
    [SerializeField]
    private Transform canvasTran;
    [SerializeField, Header("�L�����̃f�[�^���X�g")]
    private List<CharaData> charaDatasList = new List<CharaData>();

    private PlacementCharaSelectPopUp placementCharaSelectPopUp;
    private GameManager gameManager;

    private Vector3Int gridPos;

    // Update is called once per frame
    void Update()
    {
        //TODO �z�u�ł���ő�L�������ɒB���Ă���ꍇ�ɂ͔z�u�ł��Ȃ�
        if (Input.GetMouseButtonDown(0) && !placementCharaSelectPopUp.gameObject.activeSelf)
        {
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None)
            {
                //CreateChara(gridPos);
                ActivatePlacementCharaSelectPopUp();
            }
        }
    }

    public IEnumerator SetUpCharaGenerator(GameManager gameManager)
    {
        this.gameManager = gameManager;
        // TODO �X�e�[�W�̃f�[�^���擾
        CreateHaveCharaDatasList();
        yield return StartCoroutine(CreatePlacementCharaSelectPopUp());
    }

    private IEnumerator CreatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp = Instantiate(placementCharaSelectPopUpPrefab, canvasTran, false);
        placementCharaSelectPopUp.SetUpPlacementCharaSelectPopUp(this, charaDatasList);
        placementCharaSelectPopUp.gameObject.SetActive(false);

        yield return null;
    }

    public void ActivatePlacementCharaSelectPopUp()
    {
        // TODO �Q�[���̐i�s��Ԃ��Q�[����~�ɕύX
        // TODO ���ׂĂ̓G�̍s�����ꎞ��~
        placementCharaSelectPopUp.gameObject.SetActive(true);
        placementCharaSelectPopUp.ShowPopUp();
    }

    public void InactivatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp.gameObject.SetActive(false);
        // TODO �Q�[���I�[�o�[��Q�[���N���A�ł͂Ȃ��Ƃ��ɃQ�[�����v���C���ɕύX���A�Q�[���ĊJ
        // TODO ���ׂĂ̓G�̈ړ����ĊJ
        // TODO �J�����V�[�̉��Z�������ĊJ

    }

    private void CreateHaveCharaDatasList()
    {
        // TODO ��ڲ԰���������Ă���L�����̒ʂ��ԍ��Ń��X�g���쐬
        for (int i = 0; i < DataBaseManager.instance.charaDataSO.charaDatasList.Count; i++)
        {
            charaDatasList.Add(DataBaseManager.instance.charaDataSO.charaDatasList[i]);
        }
    }

    public void CreateChara(CharaData charaData)
    {
        CharaController chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);
        chara.SetCharaData(charaData, gameManager);
        chara.transform.position =
            new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
        Debug.Log(charaData.charaName);
    }
}
