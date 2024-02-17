using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileState : MonoBehaviour
{
    public enum TileSituation //�^�C���̏�
    {
        Normal, //�ʏ�̃^�C����������
        Block,�@//��Q��������ʂ�Ȃ����
        Empty, //�^�C���������������݂��Ȃ����
    }

    //�^�C���̌����ڂ̊Ǘ�
    private TileView tileView;
    
    //�G�ꂽ�������̏��
    [SerializeField]
    private int state;

    //�G�ꂽ�������̒��Ԓl
    private float middle;

    //�v���C���[���������Ă���Ȃ�true
    public bool isPressing;

    public TileSituation situation;�@//�^�C���̏�

    public AudioSource tileSound;

    private void Awake()
    {
        tileView = this.GetComponent<TileView>();
    }

    private void Start()
    {
        //������
        //�G�ꂽ��Ԃɂ���
        middle = 0f;
        SetState(0);

        tileView.SetFast();

        isPressing = false;
    }

    private void Update()
    {
        if (StageManager.instance.state == StageManager.State.Battle)
        {
            //�v���C���[���������Ă���Ƃ�
            if (isPressing)
            {
                //������
                IncreaseBorder();
            }
            else
            {
                //�G���
                DecreaseBorder();
            }
        }
    }

    public void SetUpTileView()
    {
        tileView.SetTargetImage(StageManager.stageEntity.TargetColor);
    }

    public void EnemyAttackING(float i)
    {
        tileView.SetTarget(i);
    }

    public IEnumerator DoEnemyAttack(GameObject direction)
    {
        tileView.SetDirection(direction);
        situation = TileSituation.Block;
        while (true)
        {
            tileView.SetTarget(1);
            SetState(0);
            middle = 0;
            yield return null;
        }
    }

    public void FinishEnemyAttack()
    {
        situation = TileSituation.Normal;
        tileView.SetTarget(0);
        tileView.DeleteDirection();
    }

    //������
    private void IncreaseBorder()
    {
        middle += Time.deltaTime * PlayerStatus.instance.GetWriteSpeed();
        if (middle > 0.7f)
        {
            SetState(1);

            if (middle > 1f)
            {
                middle = 1f;
            }
        }

        tileView.SetMagicUp(middle);
    }

    //�G�ꂽ
    private void DecreaseBorder()
    {
        middle -= Time.deltaTime * PlayerStatus.instance.GetEraseSpeed();
        if (middle < 0.3f)
        {
            SetState(0);

            if (middle < 0f)
            {
                middle = 0f;
            }
        }

        tileView.SetMagicDown(middle);
    }

    //�G�ꂽ��������Ԃ̕ύX�̂Ƃ��Ɏg��
    //�����ڂ܂ŕς��Ă����̂Ő�΂�������ύX
    private void SetState(int setValue)
    {
        if (state != setValue)
        {
            state = setValue;

            StageManager.instance.tileManager.Check();

            //���@�w��`�����Ƃ�
            if(setValue == 1)
            {
                tileSound.Play();
                tileView.particle.SetActive(true);
            }

            if(setValue == 0)
            {
                tileView.particle.SetActive(false);

            }
        }
    }

    //��Ԃ𔽑΂ɂ���A�g���ĂȂ�����
    private void ChangeState()
    {
        if (state == 0)
        {
            SetState(1);
        }
        else
        {
            SetState(0);
        }
    }

    public int GetState()
    {
        return state;
    }

    //�l�����Z�b�g����
    //�G�̍U���ŔG�炷�ꍇ�͂������g��
    public void ResetState()
    {
        SetState(0);
        middle = 0f;
    }
}
