using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using NCMB.Extensions;

namespace naichilab {
    public class RankingViewer : MonoBehaviour
    {
        private const string OBJECT_ID = "objectId";
        private const string COLUMN_SCORE = "score";
        private const string COLUMN_NAME = "name";

        [SerializeField] RectTransform scrollViewContent;
        [SerializeField] GameObject rankingNodePrefab;
        [SerializeField] GameObject readingNodePrefab;
        [SerializeField] GameObject notFoundNodePrefab;
        [SerializeField] GameObject unavailableNodePrefab;

        private RankingInfo _board;

        public void ChangeRanking(RankingInfo board)
        {
            _board = board;

            StartCoroutine(LoadRankingBoard());
        }

        /// <summary>
        /// �����L���O�擾���\��
        /// </summary>
        /// <returns>The ranking board.</returns>
        private IEnumerator LoadRankingBoard()
        {
            int nodeCount = scrollViewContent.childCount;
            for (int i = nodeCount - 1; i >= 0; i--)
            {
                Destroy(scrollViewContent.GetChild(i).gameObject);
            }

            var msg = Instantiate(readingNodePrefab, scrollViewContent);

            //2017.2.0b3�̕`�悳��Ȃ��o�O�b��Ή�
            MaskOffOn();

            var so = new YieldableNcmbQuery<NCMBObject>(_board.ClassName);
            so.Limit = 30;
            if (_board.Order == ScoreOrder.OrderByAscending)
            {
                so.OrderByAscending(COLUMN_SCORE);
            }
            else
            {
                so.OrderByDescending(COLUMN_SCORE);
            }

            yield return so.FindAsync();

            Debug.Log("�f�[�^�擾 : " + so.Count.ToString() + "��");
            Destroy(msg);

            if (so.Error != null)
            {
                Instantiate(unavailableNodePrefab, scrollViewContent);
            }
            else if (so.Count > 0)
            {
                int rank = 0;
                foreach (var r in so.Result)
                {
                    var n = Instantiate(rankingNodePrefab, scrollViewContent);
                    var rankNode = n.GetComponent<RankingNode>();
                    rankNode.NoText.text = (++rank).ToString();
                    rankNode.NameText.text = r[COLUMN_NAME].ToString();

                    var s = _board.BuildScore(r[COLUMN_SCORE].ToString());
                    rankNode.ScoreText.text = s != null ? s.TextForDisplay : "�G���[";

                    //                    Debug.Log(r[COLUMN_SCORE].ToString());
                }
            }
            else
            {
                Instantiate(notFoundNodePrefab, scrollViewContent);
            }
        }

        private void MaskOffOn()
        {
            //2017.2.0b3�łȂ���ScrollViewContent��ǉ����Ă��`�悳��Ȃ��ꍇ������B
            //�emask��OFF/ON����ƒ���̂Ŗ������E�E�E
            var m = scrollViewContent.parent.GetComponent<Mask>();
            m.enabled = false;
            m.enabled = true;
        }
    }
}
