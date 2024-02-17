using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Create Enemy/AttackRule", order = 2)]
    public class EnemyAttackRule : ScriptableObject
    {
        public List<SkillDataSet> SkillDataSets = new List<SkillDataSet>();
        public SkillDataSet[] GetSkillDataSets()
        {
            return SkillDataSets.ToArray();
        }

        [Header("クールタイム(秒)")]
        public float CoolTime;
        public float GetCoolTime()
        {
            return CoolTime;
        }
    }

    [System.Serializable]
    public class SkillDataSet
    {
        [Header("スキル")]
        public EnemySkillData Data;
        [Header("スキルの選択割合")]
        public int Rate;
    }
}
