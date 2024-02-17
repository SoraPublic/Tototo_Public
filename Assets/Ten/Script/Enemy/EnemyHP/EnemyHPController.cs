using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enemy;

//�G�̗̑͊Ǘ��N���X
public class EnemyHPController : MonoBehaviour
{
    private int HitPoint;
    private int MaxHP;
    public HPGaugeController hPGaugeSurface;

    public void SetUp()
    {
        MaxHP = StageManager.instance.enemy.enemy.GetEnemyHP();
        HitPoint = MaxHP;
        hPGaugeSurface.SetUp(GetHP());
    }


    public int Damage(int value)
    {
        if (HitPoint > 0)
        {
            HitPoint -= value;
            Debug.Log(value + "�_���[�W");
        }

        return HitPoint;
    }

    public int Heal(int value)
    {
        if (HitPoint < MaxHP)
        {
            HitPoint += value;
            Debug.Log(value + "��");
        }

        return HitPoint;
    }

    public int GetHP()//���݂�HP�̒l���擾����
    {
        return HitPoint;
    }
    public int GetMaxHP()
    {
        return MaxHP;
    }
}
