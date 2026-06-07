using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    public void PopupResult(string message)
    {
        var tmp = resultPanel.GetComponentInChildren<TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.text = message;
            resultPanel.SetActive(true);
        }
    }
}
