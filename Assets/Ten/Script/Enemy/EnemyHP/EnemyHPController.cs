using UnityEngine;

//敵の体力管理クラス
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
            //Debug.Log(value + "ダメージ");
        }

        return HitPoint;
    }

    public int Heal(int value)
    {
        if (HitPoint < MaxHP)
        {
            HitPoint += value;
            //Debug.Log(value + "回復");
        }

        return HitPoint;
    }

    public int GetHP()//現在のHPの値を取得する
    {
        return HitPoint;
    }
    public int GetMaxHP()
    {
        return MaxHP;
    }
}
