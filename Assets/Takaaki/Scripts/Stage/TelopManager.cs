using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TelopManager : MonoBehaviour
{
    [SerializeField] private RectTransform backGround;
    [SerializeField] private TextMeshProUGUI text;

    private RectTransform textTrans;
    private Vector2 size;

    private void Start()
    {
        this.gameObject.SetActive(false);
        size = backGround.sizeDelta;
        textTrans = text.GetComponent<RectTransform>();
    }

    public void DisplayTelop(string str, float time)
    {
        text.text = str;
        backGround.sizeDelta = new Vector2(backGround.sizeDelta.x, 0f);
        textTrans.DOLocalMoveX(550f, 0);

        this.gameObject.SetActive(true);

        StartCoroutine(DisplayCor(time));
    }

    private IEnumerator DisplayCor(float time)
    {
        backGround.DOSizeDelta(size,time* 1f/6f);
        yield return new WaitForSeconds(time* 1f/6f);

        textTrans.DOLocalMoveX(0f, time * 2f / 6f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(time * 2f / 6f);

        textTrans.DOLocalMoveX(-550f, time * 2f/6f).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(time * 2f / 6f);

        backGround.DOSizeDelta(new Vector2(backGround.sizeDelta.x, 0f), time * 1f / 6f);
        yield return new WaitForSeconds(time * 1f / 6f);

        this.gameObject.SetActive(false);
    }
}
