using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ReturnTilePosition ReturnTilePosition;

    private int nowx, nowy; //���݂̍��W
    private bool close;
    private Vector3 target; //�i�s�����̃^�C����Position
    private int x, y;
    [SerializeField] private float step = 2f; //�ړ����x
    
    private Vector3 latestPos;
    void Start()
    {
        target = this.transform.position;
    }
 
    void Update()
    {
        Vector3 diff = transform.position - latestPos;   //�O�񂩂�ǂ��ɐi�񂾂����x�N�g���Ŏ擾
        latestPos = transform.position;  //�O���Position�̍X�V

        nowStateCheck(); //���݂���^�C���̏����擾
        x = nowx; //���݂̍��W��ϐ��ɑ��
        y = nowy;

        if ((this.transform.position.x == target.x) && (this.transform.position.z == target.z)) //���g���ړ����łȂ���Έړ��\
        {
            TargetChange();
        }

        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff); //������ύX����
        }

        this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x,this.transform.position.y,target.z), step * Time.deltaTime); //�ړ������i����x��z�����������Ă��Ȃ��j
      
    }

    void TargetChange() //�i�݂����^�C���̍��W���X�V����֐�
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            y = y - 1;
            target = ReturnTilePosition.TileSearch(x, y,this.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            y = y + 1;
            target = ReturnTilePosition.TileSearch(x, y, this.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            x = x - 1;
            target = ReturnTilePosition.TileSearch(x, y, this.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            x = x + 1;
            target = ReturnTilePosition.TileSearch(x, y, this.transform.position);
        }
    }

    void nowStateCheck() //�^���Ƀ��C���΂��ă^�C���̍��W���擾�i���ꂼ��̃^�C���͍\���̂ō��W�������Ŏ����Ă���j
    {        
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, 6.0f))
        {
            nowx = hit.collider.gameObject.GetComponent<TileCoordinate>().coordinate.x;
            nowy = hit.collider.gameObject.GetComponent<TileCoordinate>().coordinate.y;     
        }
    }

}
