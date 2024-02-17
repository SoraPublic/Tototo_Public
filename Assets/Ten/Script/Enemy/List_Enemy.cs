/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Create Enemy/List_Enemy", order = 0)]
    public class List_Enemy : ScriptableObject
    {
        [SerializeField, Header("敵の情報(番号の若い順に)")]
        private List<Info_Enemy> enemyDatas;
        public Info_Enemy GetInfo_Enemy(int num)
        {
            return enemyDatas.Find(enemy => enemy.GetEnemyID() == num);
        }
    }
}
*/