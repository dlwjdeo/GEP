using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class MissionManager : MonoBehaviour
{
    [SerializeField]
    private List<Mission> missions = new List<Mission>();

    private Dictionary<int, MissionSlot> missionSlotTable = new Dictionary<int, MissionSlot>();

    [SerializeField]
    private GameObject missionSlot;
    [SerializeField]
    private VerticalLayoutGroup missionLayout;


    
   

    private void Start()
    {
        foreach (var mission in missions)
        {
            GameObject obj = Instantiate(missionSlot, missionLayout.gameObject.transform);
            if(obj.TryGetComponent<MissionSlot>(out MissionSlot ms))
            {
                ms.Init(mission);
                missionSlotTable.Add(mission.id, ms);
            }
        }
        missions.Clear();


    }

    public void CompleteMission(int id)
    {
        if (missionSlotTable.TryGetValue(id, out MissionSlot slot))
        {
            if (slot.mission.isCompleted)
                return;
            slot.mission.isCompleted = true;
            slot.textMeshPro.alpha = 0.1f;
        }

        //여기에 이벤트함수 구독해서 연출 코루틴 실행
    }


}
