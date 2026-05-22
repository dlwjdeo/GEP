using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class MissionManager : MonoBehaviour
{
    [SerializeField]
    private List<Mission> missions = new List<Mission>();

    private Dictionary<int, MissionSlot> missionSlotTable = new Dictionary<int, MissionSlot>();
    private Dictionary<MissionType, MissionSlot> missionSlotByType = new Dictionary<MissionType, MissionSlot>();

    [SerializeField]
    private GameObject missionSlot;
    [SerializeField]
    private VerticalLayoutGroup missionLayout;

    public Action<Mission> OnMissionExecuted;

    private void Start()
    {
        foreach (var mission in missions)
        {
            GameObject obj = Instantiate(missionSlot, missionLayout.gameObject.transform);
            if(obj.TryGetComponent<MissionSlot>(out MissionSlot ms))
            {
                ms.Init(mission);
                missionSlotTable.Add(mission.id, ms);

                if (!missionSlotByType.ContainsKey(mission.type))
                {
                    missionSlotByType.Add(mission.type, ms);
                }
            }
        }
        //missions.Clear();
    }

    public void CompleteMission(int id)
    {
        if (missionSlotTable.TryGetValue(id, out MissionSlot slot))
        {
            if (slot.mission.isCompleted)
                return;
            ExecuteMission(slot.mission);
        }
    }

    public void CompleteMission(MissionType type)
    {
        if (missionSlotByType.TryGetValue(type, out MissionSlot slot))
        {
            if (slot.mission.isCompleted)
                return;
            ExecuteMission(slot.mission);
        }
    }

    public void ExecuteMission(Mission mission)
    {
        OnMissionExecuted?.Invoke(mission);
    }
}

public enum MissionType
{
    Craft,
    Fish,
    Chop,
}