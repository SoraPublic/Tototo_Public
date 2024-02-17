using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TileButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    //��������
    //�����\��


    private TileState tileState;

    private Button button;

    private void Awake()
    {
        tileState = this.GetComponent<TileState>();

        //button = this.GetComponent<Button>();
        //button.onClick.AddListener(OnClick);
    }


    private void OnClick()
    {

    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        // �{�^�����������
        tileState.isPressing = true;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        // �{�^�����������
        tileState.isPressing = false;
    }
}
