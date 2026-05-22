using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionSlot : MonoBehaviour
{
    public Mission mission;
    public TextMeshProUGUI textMeshPro;
    public void Init(Mission mission)
    {
        this.mission = mission;
        if (mission == null)
            return;

        textMeshPro=GetComponent<TextMeshProUGUI>();
        textMeshPro.text = mission.content;

    }

}
