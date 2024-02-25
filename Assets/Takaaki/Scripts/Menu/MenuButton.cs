using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private Button button;

    private static bool flag = false;
    private SceneLoader loader;


    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Menu);
        loader = new SceneLoader();
    }

    private void Menu()
    {
        if(StageManager.instance == null)
        {
            if (flag)
            {
                flag = false;
                loader.UnLoadMenu();
            }
            else
            {
                flag = true;
                loader.LoadMenu();
            }
            return;
        }
        StageManager.instance.MenuControll();
    }
}
