using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private SkillEffectTimeManager skillEffectTimeManager;

    /** 初期値　**/
    public const float moveTime = 1f/2f; //移動速度
    public const float writeSpeed = 1f/2f;　//魔法陣を描く速度
    public const float eraseSpeed = 1f/12f;　//魔法陣の消える速度
    public const float coolTime = 20f; //魔法陣のクールタイム
    public const float buffDuration = 10f; //バフの持続時間s
    public const float attack = 5f;

    public const float stopTime = 2.5f;


    /** バフの値 **/
    private const float attackBuff_Fixed = 3; //攻撃力固定値上昇
    private const float attackBuff_Multiple = 1.5f;　//攻撃力倍率上昇

    private const float writeSpeedBuff = 2f; //魔法陣を描く速度アップ 倍率
    private const float coolTimeBuff = 1.5f; //魔法陣のクールタイム減少速度アップ　倍率
    private const float moveSpeedBuff = 1.5f;　//移動速度アップ　倍率

    private const float eraseSpeedDebuff = 0.5f;　//魔法陣が消える速度ダウン　倍率
    private const float enemyAttackSpeedDebuff = 0.5f; //敵の攻撃速度ダウン　倍率


    /** バフの時間 **/
    private  const int attackBuff_Fixed_Time = 0;
    private  const int attackBuff_Multiple_Time = 1;

    private  const int writeSpeedBuff_Time = 2;
    private  const int coolTimeBuff_Time = 3;
    private  const int moveSpeedBuff_Time = 4;

    private  const int eraseSpeedDebuff_Time = 5;
    private  const int enemyAttackSpeedDebuff_Time = 6;
    public const int no_skill = 7;

    private float[] buffs = new float[7];

    public static PlayerStatus instance;


    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        for (int i=0; i< buffs.Length;i++) 
        {
            buffs[i] -= Time.deltaTime;
        }

        skillEffectTimeManager.SkillEffectTime(buffs);
    }

    //バフをかける
    public void ApplyBuff(Skill buff)
    {
        if (buff == Skill.ATTACK_FIXED) 
        {
            buffs[attackBuff_Fixed_Time] = buffDuration;
        }
        else if (buff == Skill.ATTACK_MULITIPLE)
        {
            buffs[attackBuff_Multiple_Time] = buffDuration;
        }
        else if(buff == Skill.WRITE_SPEED)
        {
            buffs[writeSpeedBuff_Time] = buffDuration;
        }
        else if(buff == Skill.COOL_TIME)
        {
            buffs[coolTimeBuff_Time] = buffDuration;
        }
        else if(buff == Skill.MOVE_SPEED)
        {
            buffs[moveSpeedBuff_Time] = buffDuration;
        }
        else if(buff == Skill.ERASE_SPEED) 
        {
            buffs[eraseSpeedDebuff_Time] = buffDuration;   
        }
        else if(buff == Skill.ENEMY_SPEED)
        {
            buffs[eraseSpeedDebuff_Time] = buffDuration;
        }
    }

    public void ResetBuff()
    {
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = 0 ;
        }
    }

    public float GetAttack(float attack)
    {
        if (buffs[attackBuff_Fixed_Time] > 0f)
        {
            attack += attackBuff_Fixed;
        }
        
        if(buffs[attackBuff_Multiple_Time] > 0f)
        {
            attack *= attackBuff_Multiple;
        }

        return attack;
    }

    public float GetWriteSpeed()
    {
        float speed = writeSpeed;

        if(buffs[writeSpeedBuff_Time] > 0f)
        {
            speed *= writeSpeedBuff; 
        }

        return speed;
    }

    public float GetCoolTime()
    {
        float speed = 1f;

        if(buffs[coolTimeBuff_Time] > 0f)
        {
            speed *= coolTimeBuff;
        }
        return speed;
    }

    public float GetMoveSpeed()
    {
        float speed = moveTime;

        if(buffs[moveSpeedBuff_Time] > 0f)
        {
            speed /= moveSpeedBuff;
        }

        return speed;
    }

    public float GetEraseSpeed()
    {
        float speed = eraseSpeed;
        
        if (buffs[eraseSpeedDebuff_Time] > 0f)
        {
            speed *= eraseSpeedDebuff;
        }

        return speed;
    }

    public float GetEnemyAttackSpeed(float speed)
    {
        if(buffs[eraseSpeedDebuff_Time] > 0f)
        {
            speed *= enemyAttackSpeedDebuff;
        }

        return speed;
    }

    /** 重ね掛け可能　**

    //** バフ **
    public List<float> attackBuffs_Fixed = new List<float>();
    public List<float> attackBuffs_Multiple = new List<float>();

    public List<float> writeSpeedBuffs = new List<float>();
    public List<float> coolTimeBuffs = new List<float>();
    public List<float> moveSpeedBuffs = new List<float>();

    public List<float> eraseSpeedDebuffs = new List<float>();
    public List<float> enemyAttackSpeedDebuffs = new List<float>();

    //バフのlist
    public List<List<float>> lists = new List<List<float>>();

    private void Start()
    {
        lists.Add(attackBuffs_Fixed);
        lists.Add(attackBuffs_Multiple);
        lists.Add(writeSpeedBuffs);
        lists.Add(coolTimeBuffs);
        lists.Add(moveSpeedBuffs);
        lists.Add(eraseSpeedDebuffs);
        lists.Add(enemyAttackSpeedDebuffs);

    }


    private void Update()
    {
        foreach (List<float> buff in lists)
        {
            //バフの時間減少
            for (int i = 0; i < buff.Count; i++)
            {
                buff[i] -= Time.deltaTime;
            }

            //バフの削除　後ろから削除していく
            for (int i = buff.Count-1; i >= 0; i--)
            {
                if(buff[i] < 0)
                {
                    buff.Remove(i);
                }
            }
        }
    }

    *********************************/
}
