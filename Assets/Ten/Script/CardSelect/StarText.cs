using TMPro;
using UnityEngine;

public class StarText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI term_text;
    private ResultEntity entity;
    // Start is called before the first frame update
    void Start()
    {
        entity = StageManager.stageEntity.resultEntity;
        term_text.text = "・" + entity.clear.ToString("d2") + "回以上クリア\n"
            + "・被弾回数" + entity.hit.ToString("d2") + "回以下\n"
            + "・討伐時間" + (entity.clearTime / 60).ToString("d2") + ":" + (entity.clearTime % 60).ToString("d2") + "以内";
    }
}
