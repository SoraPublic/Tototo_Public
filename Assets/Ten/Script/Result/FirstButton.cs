using UnityEngine;

public class FirstButton : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    ResultWriter writer;
    ResultManager manager;

    private void Start()
    {
        GameObject parent = transform.parent.parent.gameObject;
        animator = parent.GetComponent<Animator>();
        manager = new ResultManager();
    }

    public void OnClick()
    {
        animator.SetTrigger("next");
        StartCoroutine(writer.SecondAnim(manager.LoadResultData(Application.dataPath + "/" + SavePathName.CurrentStageFile).star));//"/ResultData/" + StageManager.instance.stageEntity.name + "_Data.json").star));
    }
}
