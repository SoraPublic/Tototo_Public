using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Create Enemy/Info_Enemy", order = 1)]
    public class Info_Enemy : ScriptableObject
    {
        [SerializeField, Header("敵の名前")]
        private string enemyName;
        public string GetEnemy_Name()
        {
            return enemyName;
        }

#if UNITY_EDITOR
        public void SetEnemyName(string value)
        {
            enemyName = value;
        }
#endif

        [SerializeField, Header("最大HP")]
        private int enemyHP;
        public int GetEnemyHP()
        {
            return enemyHP;
        }

#if UNITY_EDITOR
        public void SetEnemyHP(int value)
        {
            enemyHP = value;
        }
#endif

        [SerializeField]
        private List<EnemyPattern> patternList = new List<EnemyPattern>();
        public EnemyPattern[] GetEnemyPattern()
        {
            return patternList.ToArray();
        }
#if UNITY_EDITOR
        public List<EnemyPattern> PatternList
        {
            get { return patternList; }
            set { patternList = value; }
        }
#endif

        [SerializeField, Header("敵のモデル")]
        private GameObject enemyModel;
        public GameObject GetEnemyModel()
        {
            return enemyModel;
        }
#if UNITY_EDITOR
        public void SetEnemyModel(GameObject model)
        {
            enemyModel = model;
        }
#endif
    }

    [System.Serializable]
    public class EnemyPattern
    {
        [Header("行動パターン")]
        public EnemyAttackRule Rule;
        [Header("行動パターンを変更するHP(○○%以下で発動)")]
        public float Condition;
    }
}

