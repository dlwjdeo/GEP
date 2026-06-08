using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button button;
    public void PopupResult(string message, float timer) //timer를 0으로 설정 시 표기 안함
    {
        if (resultText != null)
        {
            resultText.text = message;
        }
        if (timerText != null)
        {
            if (timer > 0f)
            {
                timerText.text = $"Time: {timer:F2}s";
            }
            else
            {
                timerText.text = string.Empty;
            }
        }
        resultPanel.SetActive(true);
    }

    public void OnClickRestart()
    {
        FindObjectOfType<GameManager>().Restart();
    }
}
