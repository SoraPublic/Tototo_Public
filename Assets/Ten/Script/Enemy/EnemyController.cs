using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemyController : MonoBehaviour
{
    [System.NonSerialized]
    public Info_Enemy enemy;
    private EnemyPattern[] enemyPatterns;
    private EnemyAttackRule nowRule;
    private SkillDataSet[] _skillDatas;
    //private SkillData_Enemy[] enemySkills;
    [SerializeField]
    private EnemyAttackController attackController;
    [SerializeField]
    private EnemyHPController hpController;
    [System.NonSerialized]
    public StageManager.State state;
    public GameObject enemyModel;
    public Animator animator;
    public GameObject nowModel;

    private const string attack = "AttackTrigger";
    private const string damage = "DamageTrigger";
    private const string death = "DeathTrigger";

    private float coolTime = 0;//攻撃のクールタイム
    public float alertTime = 3.0f;//攻撃予測の表示時間

    void Update()
    {
        if (StageManager.instance.state == StageManager.State.Battle)
        {
            coolTime += Time.deltaTime;
            if (coolTime > nowRule.GetCoolTime())
            {
                StartCoroutine(AttackCor());
                //Debug.Log(nowRule);
                coolTime = 0.0f;
            }
        }

        //↓デバッグ用
        /*
        if (Input.GetKeyDown(KeyCode.H))
        {
            HitPointHeal(10);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            HitPointDamage(100);
        }
        */
    }

    private IEnumerator AttackCor()
    {
        attackController.Attack(_skillDatas);

        yield return new WaitForSeconds(alertTime);
        animator.SetTrigger(attack);
    }

    public void Change_Rule(int nowHP)
    {
        float nowRate = (float)nowHP / enemy.GetEnemyHP() * 100;
        //Debug.Log(nowRate);
        int index = 0;

        foreach (EnemyPattern pattern in enemy.GetEnemyPattern())
        {
            float rate = pattern.Condition;
            if (nowRate <= rate)
            {
                nowRule = enemy.GetEnemyPattern()[index].Rule;
                _skillDatas = nowRule.GetSkillDataSets();
                //Debug.Log(nowRule);
            }
            index++;
        }
    }

    public Info_Enemy GetInfo_Enemy()
    {
        return enemy;
    }

    public void HitPointDamage(int value)
    {
        int hp = hpController.GetHP();

        if (hp <= 0)
        {
            return;
        }

        hp = hpController.Damage(value);
        Change_Rule(hp);
        hpController.hPGaugeSurface.HPGaugeUpdate(hp);
        if (hp <= 0)
        {
            attackController.StopAllCoroutines();
            StageManager.instance.AllEnemyKill();
            if(animator != null)
            {
                animator.SetTrigger(death);
            }
        }
        if (animator != null)
        {
            animator.SetTrigger(damage);
        }
    }

    public void HitPointHeal(int value)
    {
        int hp = hpController.GetHP();

        if (hp <= 0)
        {
            return;
        }
        
        hp = hpController.Heal(value);
        Change_Rule(hp);
        hpController.hPGaugeSurface.HPGaugeUpdate(hp);
    }

    public void SetUp()
    {
        if (nowModel != null)
        {
            nowModel.SetActive(false);
        }
        enemy = StageManager.instance.wave.infoEnemy;
        enemyModel = enemy.GetEnemyModel();
        
        nowModel = Instantiate(enemyModel);
        animator = nowModel.GetComponent<Animator>();
        nowModel.SetActive(true);
        enemyPatterns = enemy.GetEnemyPattern();
        nowRule = enemyPatterns[0].Rule;
        _skillDatas = nowRule.GetSkillDataSets();
        attackController.SetUp();
        hpController.SetUp();
    }
}
