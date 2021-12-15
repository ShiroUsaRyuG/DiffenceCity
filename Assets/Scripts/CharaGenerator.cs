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
        if (Input.GetMouseButtonDown(0) && !placementCharaSelectPopUp.gameObject.activeSelf 
            && gameManager.currentGameState == GameManager.GameState.Play)
        {
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None)
            {
                //CreateChara(gridPos);
                ActivatePlacementCharaSelectPopUp();
            }
        }
    }

    /// <summary>
    /// /�ݒ�
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator SetUpCharaGenerator(GameManager gameManager)
    {
        this.gameManager = gameManager;
        // TODO �X�e�[�W�̃f�[�^���擾
        CreateHaveCharaDatasList();
        yield return StartCoroutine(CreatePlacementCharaSelectPopUp());
    }

    /// <summary>
    /// �z�u�L�����I��p�|�b�v�A�b�v�̐���
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp = Instantiate(placementCharaSelectPopUpPrefab, canvasTran, false);
        placementCharaSelectPopUp.SetUpPlacementCharaSelectPopUp(this, charaDatasList);
        placementCharaSelectPopUp.gameObject.SetActive(false);

        yield return null;
    }

    /// <summary>
    /// �z�u�L�����I��p�|�b�v�A�b�v�̕\��
    /// </summary>
    public void ActivatePlacementCharaSelectPopUp()
    {

        gameManager.SetGameState(GameManager.GameState.Stop);
        gameManager.PauseEnemies();
        placementCharaSelectPopUp.gameObject.SetActive(true);
        placementCharaSelectPopUp.ShowPopUp();
    }

    /// <summary>
    /// �z�u�L�����I��p�|�b�v�A�b�v�̔�\��
    /// </summary>
    public void InactivatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp.gameObject.SetActive(false);
        if (gameManager.currentGameState == GameManager.GameState.Stop)
        {
            gameManager.SetGameState(GameManager.GameState.Play);
            gameManager.ResumeEnemies();
            // TODO �J�����V�[�̉��Z�������ĊJ
        }

    }

    /// <summary>
    /// �L�����̃f�[�^�̃��X�g��
    /// </summary>
    private void CreateHaveCharaDatasList()
    {
        // TODO ��ڲ԰���������Ă���L�����̒ʂ��ԍ��Ń��X�g���쐬
        for (int i = 0; i < DataBaseManager.instance.charaDataSO.charaDatasList.Count; i++)
        {
            charaDatasList.Add(DataBaseManager.instance.charaDataSO.charaDatasList[i]);
        }
    }

    /// <summary>
    /// �I�������L�����𐶐����Ĕz�u
    /// </summary>
    /// <param name="charaData"></param>
    public void CreateChara(CharaData charaData)
    {
        CharaController chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);
        chara.SetCharaData(charaData, gameManager);
        chara.transform.position =
            new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
        Debug.Log(charaData.charaName);
    }
}
