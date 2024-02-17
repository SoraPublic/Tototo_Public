/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Enemy
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Create Enemy/SkillFactor", order = 4)]
    public class SkillFactor_Enemy : ScriptableObject
    {
        [SerializeField, Header("ターゲット種類")]
        private AimKindEnemyAttack target;
        public AimKindEnemyAttack GetKind_of_Target()
        {
            return target;
        }

        [SerializeField, Header("攻撃の遅延")]
        private bool delay;
        public bool GetIsDelay()
        {
            return delay;
        }

        [SerializeField, Header("遅延の長さ(秒)")]
        private float delayTime;
        public float GetDelay()
        {
            return delayTime;
        }

        [SerializeField, Header("ターゲット数(Random)"), Range(0, 25)]
        private int numberOfTargets;
        public int GetNum_of_Targets()
        {
            return numberOfTargets;
        }

        [SerializeField, Header("行, 列指定(Horizontal, Vertical, 5はランダム)"), Range(0, 5)]
        private int positionOfTargets;
        public int GetLine_of_Targets()
        {
            if (positionOfTargets == 5)
            {
                return Random.Range(0, 5);
            }

            return positionOfTargets;
        }

        [SerializeField, Header("座標指定(Selected)")]
        private int[] coordinate;
        public int[] GetCoordinate_of_Target()
        {
            return coordinate;
        }

        [SerializeField, Header("追加効果")]
        private EnemySkillEffect effect;
        public EnemySkillEffect GetEffect()
        {
            return effect;
        }

        [SerializeField, Header("回復量(追加効果がHealなら)")]
        private int point;
        public int GetPoint()
        {
            return point;
        }
    }
}

*/