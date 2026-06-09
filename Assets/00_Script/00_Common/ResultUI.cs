using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button button;
    public void PopupResult(string message, float timer) //timer를 0으로 설정 시 표기 안함
    {
        if (resultText != null && timerText!=null)
        {
            timerText.text = BuildResultText(timer);
            resultText.text = message;
        }

        resultPanel.SetActive(true);
    }
    private string BuildResultText(float timer)
    {
        StringBuilder builder = new StringBuilder();

        var missions = GameManager.Instance.MissionManager.Missions;

        foreach (var mission in missions)
        {
            string completedText = mission.isCompleted ? "O" : "X";
            builder.AppendLine($"{mission.content} {completedText}");
        }

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        builder.AppendLine($"Clear Time : {minutes:00} : {seconds:00}");

        return builder.ToString();
    }
    public void OnClickRestart()
    {
        FindObjectOfType<GameManager>().Restart();
    }
}
