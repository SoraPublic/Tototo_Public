using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private TileManager tileManager;
    private PlayerManager player;
    private ResultManager resultManager;

    private void Awake()
    {
        resultManager = new ResultManager();
    }


    public void SetUp()
    {
        tileManager = StageManager.instance.tileManager;
        player = StageManager.instance.playerManager;
    }

    public void Attack(SkillDataSet[] dataSet)
    {
        List<int> rates = new List<int>();
        foreach (SkillDataSet s in dataSet)
        {
            rates.Add(s.Rate);
        }
        EnemySkillData data = dataSet[Choose(rates)].Data;

        foreach (EnemySkillFactor factor in data.SkillFactor)
        {
            ChoiceAttack(factor);
        }
    }

    private void ChoiceAttack(EnemySkillFactor factor)
    {
        if (!factor.IsDelay)
        {
            switch (factor.AimKind)
            {
                case AimKindEnemyAttack.RandomPoint:
                    AttackRandomPoints(factor);
                    break;

                case AimKindEnemyAttack.RandomHorizontal:
                    AttackRandomHorizontal(factor);
                    break;

                case AimKindEnemyAttack.RandomVertical:
                    AttackRandomVertical(factor);
                    break;

                case AimKindEnemyAttack.Select:
                    AttackSelected(factor);
                    break;
            }
        }
        else
        {
            switch (factor.AimKind)
            {
                case AimKindEnemyAttack.RandomPoint:
                    StartCoroutine(DelayAttackRandomPoints(factor));
                    break;

                case AimKindEnemyAttack.RandomHorizontal:
                    StartCoroutine(DelayAttackRandomHorizontal(factor));
                    break;

                case AimKindEnemyAttack.RandomVertical:
                    StartCoroutine(DelayAttackRandomVertical(factor));
                    break;

                case AimKindEnemyAttack.Select:
                    StartCoroutine(DelayAttackSelected(factor));
                    break;
            }
        }

        return;
    }

    private void AttackRandomPoints(EnemySkillFactor factor)//ランダムな座標に攻撃
    {
        List<int> points = new List<int>();

        for (int i = 0; i < 25; i++)
        {
            points.Add(i);
        }

        int roopCount = 0;
        for (int i = 0; i < factor.AimCount; i++)
        {
            roopCount++;
            int index = points[Random.Range(0, points.Count)];
            if (DoAttack(index, factor))
            {
                points.Remove(index);
                continue;
            }
            else //障害物等で攻撃が成立しなかった場合
            {
                points.Remove(index);
                i--;
            }

            if (roopCount > 30)
            {
                break;
            }
        }

        return;
    }

    private void AttackRandomHorizontal(EnemySkillFactor factor)//横一直線(ランダム)
    {
        List<int> lines = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            lines.Add(i);
        }

        for(int i = 0; i < factor.AimCount; i++)
        {
            int aimLine = lines[Random.Range(0, lines.Count)];
            for (int j = 5 * aimLine; j < 5 * (aimLine + 1); j++)
            {
                DoAttack(j, factor);
                lines.Remove(aimLine);
            }
        }

        return;
    }

    private void AttackRandomVertical(EnemySkillFactor factor)//縦一直線(ランダム)
    {
        List<int> lines = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            lines.Add(i);
        }

        for (int i = 0; i < factor.AimCount; i++)
        {
            int aimLine = lines[Random.Range(0, lines.Count)];
            for (int j = aimLine; j < 25; j += 5)
            {
                DoAttack(j, factor);
                lines.Remove(aimLine);
            }
        }

        return;
    }

    private void AttackSelected(EnemySkillFactor factor)//あらかじめ選択
    {
        List<int> target = new List<int>();
        target.AddRange(factor.Coordinate);

        while (target.Count > 0)
        {
            DoAttack(target[0], factor);
            target.RemoveAt(0);
        }

        return;
    }

    //コルーチンは遅延が欲しいとき
    private IEnumerator DelayAttackRandomPoints(EnemySkillFactor factor)//ランダム
    {
        List<int> points = new List<int>();

        for (int i = 0; i < 25; i++)
        {
            points.Add(i);
        }

        int roopCount = 0;
        for (int i = 0; i < factor.AimCount; i++)
        {
            roopCount++;
            int index = points[Random.Range(0, points.Count)];
            if (DoAttack(index, factor))
            {
                points.Remove(index);
                yield return new WaitForSeconds(factor.DelayTime);
                continue;
            }
            else //障害物等で攻撃が成立しなかった場合
            {
                points.Remove(index);
                i--;
            }

            if(roopCount > 30)
            {
                break;
            }
        }

        yield break;
    }

    private IEnumerator DelayAttackRandomHorizontal(EnemySkillFactor factor)//横一直線
    {
        List<int> lines = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            lines.Add(i);
        }

        bool isReverse = false;
        switch (factor.ReverseMode)
        {
            case ReverseMode.True:
                isReverse = true; 
                break;

            case ReverseMode.False:
                isReverse = false;
                break;

            case ReverseMode.RANDOM:
                int direction = Random.Range(0, 2);
                if(direction == 0)
                {
                    isReverse = false;
                }
                else
                {
                    isReverse = true;
                }
                break;
        }

        for (int i = 0; i < factor.AimCount; i++)
        {
            int aimLine = lines[Random.Range(0, lines.Count)];
            if (isReverse)
            {
                for (int j = 5 * aimLine; j < 5 * (aimLine + 1); j++)
                {
                    DoAttack(j, factor);
                    lines.Remove(aimLine);
                    yield return new WaitForSeconds(factor.DelayTime);
                }
            }
            else
            {
                for (int j = 5 * (aimLine + 1) - 1; j >= 5 * aimLine; j--)
                {
                    DoAttack(j, factor);
                    lines.Remove(aimLine);
                    yield return new WaitForSeconds(factor.DelayTime);
                }
            }            
        }

        yield break;
    }

    private IEnumerator DelayAttackRandomVertical(EnemySkillFactor factor)//縦一直線
    {
        List<int> lines = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            lines.Add(i);
        }

        bool isReverse = false;
        switch (factor.ReverseMode)
        {
            case ReverseMode.True:
                isReverse = true;
                break;

            case ReverseMode.False:
                isReverse = false;
                break;

            case ReverseMode.RANDOM:
                int direction = Random.Range(0, 2);
                if (direction == 0)
                {
                    isReverse = false;
                }
                else
                {
                    isReverse = true;
                }
                break;
        }

        for (int i = 0; i < factor.AimCount; i++)
        {
            int aimLine = lines[Random.Range(0, lines.Count)];
            if (isReverse)
            {
                for (int j = aimLine; j < 25; j += 5)
                {
                    DoAttack(j, factor);
                    lines.Remove(aimLine);
                    yield return new WaitForSeconds(factor.DelayTime);
                }
            }
            else
            {
                for (int j = aimLine + 20; j >= 0; j -= 5)
                {
                    DoAttack(j, factor);
                    lines.Remove(aimLine);
                    yield return new WaitForSeconds(factor.DelayTime);
                }
            }
        }

        yield break;
    }

    private IEnumerator DelayAttackSelected(EnemySkillFactor factor)//あらかじめ選択
    {
        List<int> target = new List<int>();
        target.AddRange(factor.Coordinate);

        bool isReverse = false;
        switch (factor.ReverseMode)
        {
            case ReverseMode.True:
                isReverse = true;
                break;

            case ReverseMode.False:
                isReverse = false;
                break;

            case ReverseMode.RANDOM:
                int direction = Random.Range(0, 2);
                if (direction == 0)
                {
                    isReverse = false;
                }
                else
                {
                    isReverse = true;
                }
                break;
        }

        if (isReverse)
        {
            while (target.Count > 0)
            {
                DoAttack(target[0], factor);
                target.RemoveAt(0);
                yield return new WaitForSeconds(factor.DelayTime);
            }
        }
        else
        {
            while (target.Count > 0)
            {
                DoAttack(target[target.Count - 1], factor);
                target.RemoveAt(target.Count - 1);
                yield return new WaitForSeconds(factor.DelayTime);
            }
        }

        yield break;
    }


    private bool DoAttack(int num, EnemySkillFactor factor)//攻撃実行（攻撃できるかどうかはココで判定）
    {
        if (tileManager.GetSituation(num) != TileState.TileSituation.Normal)
        {
            return false;
        }
        StartCoroutine(DoAttackDirection(num, factor));
        return true;
    }

    private IEnumerator DoAttackDirection(int num, EnemySkillFactor factor)
    {
        float time = 0;

        while(time < StageManager.instance.enemy.alertTime)
        {
            time += Time.deltaTime;
            tileManager.tileStates[num].EnemyAttackING(time / StageManager.instance.enemy.alertTime);
            yield return null;
        }

        Coroutine coroutine = StartCoroutine(tileManager.tileStates[num].DoEnemyAttack(factor.Direction));

        int nowXY = player.nowX + 5 * player.nowY;

        if (nowXY == num)
        {
            StartCoroutine(StageManager.instance.StopPlayer());
            if (factor.IsHeal)
            {
                StageManager.instance.enemy.HitPointHeal(factor.HealPoint);
            }
            ResultData data = resultManager.LoadResultData(Application.dataPath + "/" + SavePathName.CurrentStageFile);//"/ResultData/Current_Data.json");
            data.hit++;
            resultManager.SaveResultData(data, Application.dataPath + "/" + SavePathName.CurrentStageFile); //"/ResultData/Current_Data.json");
        }

        yield return new WaitForSeconds(2f);

        StopCoroutine(coroutine);

        tileManager.tileStates[num].FinishEnemyAttack();

        yield break;
    }


    private int Choose(List<int> probs) //割合に従い選択
    {
        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Count; i++)
        {
            if (randomPoint <= probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }

        return probs.Count - 1; //便宜上
    }
}
