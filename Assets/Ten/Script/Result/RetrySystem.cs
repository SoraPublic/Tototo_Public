using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrySystem : MonoBehaviour
{
    private SceneLoader loader = new SceneLoader();
    public void OnClick()
    {
        loader.LoadBattleScene(StageManager.stageEntity);
    }
}
