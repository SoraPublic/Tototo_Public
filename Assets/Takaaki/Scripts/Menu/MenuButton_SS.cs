using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton_SS : MonoBehaviour
{
    private Button button;

    private SceneLoader loader;

    private bool flag = false; 

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Menu);

        loader = new SceneLoader();
    }

    private void Menu()
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
    }
}
