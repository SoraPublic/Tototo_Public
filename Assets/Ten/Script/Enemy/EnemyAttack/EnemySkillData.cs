using System;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Create Enemy/SkillData", order = 3)]
    public class EnemySkillData : ScriptableObject
    {
        [SerializeField]
        private List<EnemySkillFactor> _skillFactor = new List<EnemySkillFactor>();
        public List<EnemySkillFactor> SkillFactor
        {
            get { return _skillFactor; }
#if UNITY_EDITOR
            set { _skillFactor = value; }
#endif
        }
    }

    [System.Serializable]
    public class EnemySkillFactor
    {
        [SerializeField]
        private GameObject _direction;
        public GameObject Direction
        {
            get { return _direction; }
#if UNITY_EDITOR
            set { _direction = value; }
#endif
        }

        [SerializeField]
        private AimKindEnemyAttack _aimKind;
        public AimKindEnemyAttack AimKind
        {
            get { return _aimKind; }
#if UNITY_EDITOR
            set { _aimKind = value; }
#endif
        }

        [SerializeField]
        private int _aimCount;
        public int AimCount
        {
            get { return _aimCount; }
#if UNITY_EDITOR
            set { _aimCount = value; }
#endif
        }

        [SerializeField, Range(0, 24)]
        private List<int> _coordinate;
        public int[] Coordinate
        {
            get { return _coordinate.ToArray(); }
        }
#if UNITY_EDITOR
        public List<int> SetCoordinate
        {
            set { _coordinate = value; }
        }
#endif

        [SerializeField]
        private bool _isDelay;
        public bool IsDelay
        {
            get { return _isDelay; }
#if UNITY_EDITOR
            set { _isDelay = value; }
#endif
        }

        [SerializeField]
        private float _delayTime;
        public float DelayTime
        {
            get { return _delayTime; }
#if UNITY_EDITOR
            set { _delayTime = value; }
#endif
        }

        [SerializeField]
        private ReverseMode _reverseMode;
        public ReverseMode ReverseMode
        {
            get { return _reverseMode; }
#if UNITY_EDITOR
            set { _reverseMode = value; }
#endif
        }

        [SerializeField]
        private bool _isHeal;
        public bool IsHeal
        {
            get { return _isHeal; }
#if UNITY_EDITOR
            set { _isHeal = value; }
#endif
        }

        [SerializeField]
        private int _healPoint;
        public int HealPoint
        {
            get { return _healPoint; }
#if UNITY_EDITOR
            set { _healPoint = value; }
#endif
        }
    }

    public enum AimKindEnemyAttack
    {
        RandomPoint,        //ランダム
        RandomHorizontal,   //横直線
        RandomVertical,     //縦直線
        Select,             //選択
    }

    public enum ReverseMode
    {
        True,
        False,
        RANDOM
    }
}
