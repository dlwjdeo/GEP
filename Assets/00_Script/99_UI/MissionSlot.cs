using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionSlot : MonoBehaviour
{
    public Mission mission;
    public TextMeshProUGUI textMeshPro;

    [SerializeField] private Image image;
    public void Init(Mission mission)
    {
        this.mission = mission;
        if (mission == null)
            return;
        
        //미션은 SO로 관리되므로, 초기화 시점에 isCompleted를 false로 설정
        this.mission.isCompleted = false;

        textMeshPro=GetComponent<TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();

        textMeshPro.text = mission.content;
        
        image.fillAmount = 0f;

        GameManager.Instance.MissionManager.OnMissionExecuted += ExecuteMission;
    }

    private void OnDestroy()
    {
        GameManager.Instance.MissionManager.OnMissionExecuted -= ExecuteMission;
    }

    private void ExecuteMission(Mission mission)
    {
        if (this.mission != mission)
            return;
        
        if(this.mission.isCompleted)
            return;

        StartCoroutine(MissionCompleteEffect());
        this.mission.isCompleted = true;
    }

    IEnumerator MissionCompleteEffect()
    {
        float duration = 2f;
        float elapsed = 0f;

        if (image == null)
            yield break;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            image.fillAmount = Mathf.Lerp(0f, 1f, elapsed / duration);
            yield return null;
        }

        image.fillAmount = 1f;
    }

}
