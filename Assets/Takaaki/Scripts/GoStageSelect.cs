using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GoStageSelect : MonoBehaviour
{
    private Button button;
    private SceneLoader loader;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Load);

        loader = new SceneLoader();

    }

    private void Load()
    {
        loader.LoadStageSelect();
    }
}
