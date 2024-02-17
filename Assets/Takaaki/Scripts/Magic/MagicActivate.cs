using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicActivate : MonoBehaviour
{
    [SerializeField] private MagicCardView cardView;
    [SerializeField] private AttackEffect attackEffect;
    [SerializeField] private AttackTextManager attackTextManager;

    [SerializeField] private PlayerAnimator playerAnimator;

    private float count;

    private EnemyController enemyController;
    private PlayerStatus playerStatus;

    private bool isCheck;


    private void Update()
    {
        if (StageManager.instance.state == StageManager.State.Battle)
        {
            count -= Time.deltaTime * PlayerStatus.instance.GetCoolTime();
            cardView.CoolTimeDisplay(count);
        }


        if (count < 0 && !isCheck)
        {
            isCheck = true;
            StageManager.instance.Check();
        }
    }

    public void SetWave()
    {
        isCheck = false;
        count = 0;
        cardView.CoolTimeDisplay(count);
    }


    public void Active(float attackDamage, Skill skill)
    {
        if (count > 0)
        {
            //Debug.Log("�N�[���^�C���ł�");
            return;
        }

        //Debug.Log("���@�̔���");

        /* �N�[���^�C���̐ݒ� */
        isCheck = false;

        count = PlayerStatus.coolTime;

        /* �X�L���̏��� */
        if (playerStatus == null)
        {
            playerStatus = PlayerStatus.instance;
        }


        playerStatus.ApplyBuff(skill);


        /* �U���̏���  */
        if (enemyController == null)
        {
            enemyController = StageManager.instance.enemy;
        }

        int damage = (int)PlayerStatus.instance.GetAttack(attackDamage);

        StartCoroutine(AttackCor(damage));
    }

    private IEnumerator AttackCor(int damage)
    {
        playerAnimator.AttackAnim();

        yield return new WaitForSeconds(0.5f);

        attackEffect.Play();

        yield return new WaitForSeconds(0.5f);

        attackTextManager.Attack(damage);
        enemyController.HitPointDamage(damage);
    }
}
