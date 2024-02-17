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
        [SerializeField, Header("�^�[�Q�b�g���")]
        private AimKindEnemyAttack target;
        public AimKindEnemyAttack GetKind_of_Target()
        {
            return target;
        }

        [SerializeField, Header("�U���̒x��")]
        private bool delay;
        public bool GetIsDelay()
        {
            return delay;
        }

        [SerializeField, Header("�x���̒���(�b)")]
        private float delayTime;
        public float GetDelay()
        {
            return delayTime;
        }

        [SerializeField, Header("�^�[�Q�b�g��(Random)"), Range(0, 25)]
        private int numberOfTargets;
        public int GetNum_of_Targets()
        {
            return numberOfTargets;
        }

        [SerializeField, Header("�s, ��w��(Horizontal, Vertical, 5�̓����_��)"), Range(0, 5)]
        private int positionOfTargets;
        public int GetLine_of_Targets()
        {
            if (positionOfTargets == 5)
            {
                return Random.Range(0, 5);
            }

            return positionOfTargets;
        }

        [SerializeField, Header("���W�w��(Selected)")]
        private int[] coordinate;
        public int[] GetCoordinate_of_Target()
        {
            return coordinate;
        }

        [SerializeField, Header("�ǉ�����")]
        private EnemySkillEffect effect;
        public EnemySkillEffect GetEffect()
        {
            return effect;
        }

        [SerializeField, Header("�񕜗�(�ǉ����ʂ�Heal�Ȃ�)")]
        private int point;
        public int GetPoint()
        {
            return point;
        }
    }
}

*/