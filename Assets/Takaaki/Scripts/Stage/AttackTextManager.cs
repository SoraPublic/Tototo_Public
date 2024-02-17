using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackTextManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private List<GameObject> attackTexts = new List<GameObject>();

    public void Attack(int damage)
    {
        GameObject gameObject;
        if(attackTexts.Count == 0)
        {
            gameObject = Instantiate(prefab);
            gameObject.transform.SetParent(this.transform);
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

        }
        else if (attackTexts[0].activeSelf)
        {
            gameObject = Instantiate(prefab);
            gameObject.transform.SetParent(this.transform);
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        }
        else
        {
            gameObject = attackTexts[0];
            gameObject.SetActive(true);
            attackTexts.RemoveAt(0);
        }

        attackTexts.Add(gameObject);

        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();

        text.text = "" + damage + " É_ÉÅÅ[ÉW";
    }
}
