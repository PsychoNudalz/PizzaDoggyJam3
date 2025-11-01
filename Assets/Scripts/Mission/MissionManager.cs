using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }

    [SerializeField] private List<MissionObject> missionList = new List<MissionObject>();

    [Header("End of day")]
    [SerializeField] private MissionObject endOfDayMission;
    [SerializeField] private UnityEvent onEndOfDay;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        List<MissionObject> temp = new List<MissionObject>();
        foreach (MissionObject missionObject in missionList)
        {
            temp.Add(Instantiate(missionObject));
        }
        missionList = temp;
    }

    // ---------- STATIC ACCESSORS ----------

    public static void LoadMission_Name(string name)
    {
        Instance?.LoadMissionByName(name);
    }

    public static void LoadMission_Index(int index)
    {
        Instance?.LoadMissionByIndex(index);
    }

    public static void LoadMission_Object(MissionObject mission)
    {
        Instance?.LoadMissionByObject(mission);
    }

    public static void CompleteMission_Name(string name)
    {
        Instance?.CompleteMissionByName(name);
    }

    public static void CompleteMission_Index(int index)
    {
        Instance?.CompleteMissionByIndex(index);
    }

    public static void CompleteMission_Object(MissionObject mission)
    {
        Instance?.CompleteMissionByObject(mission);
    }

    // ---------- INSTANCE METHODS ----------

    public void LoadMissionByName(string name)
    {
        var mission = missionList.Find(m => m.MissionName == name);
        if (mission != null)
            LoadMission(mission);
        else
            Debug.LogWarning($"Mission '{name}' not found.");
    }

    public void LoadMissionByIndex(int index)
    {
        if (index >= 0 && index < missionList.Count)
            LoadMission(missionList[index]);
        else
            Debug.LogWarning($"Mission index {index} invalid.");
    }

    public void LoadMissionByObject(MissionObject mission)
    {
        LoadMissionByName(mission.MissionName);

    }

    private void LoadMission(MissionObject mission)
    {
        mission.LoadMission();
        UIController.LoadMission(mission);
    }

    public void CompleteMissionByName(string name)
    {
        var mission = missionList.Find(m => m.MissionName == name);
        if (mission != null)
            CompleteMission(mission);
        else
            Debug.LogWarning($"Mission '{name}' not found.");
    }

    public void CompleteMissionByIndex(int index)
    {
        if (index >= 0 && index < missionList.Count)
            CompleteMission(missionList[index]);
        else
            Debug.LogWarning($"Mission index {index} invalid.");
    }

    public void CompleteMissionByObject(MissionObject mission)
    {
        CompleteMissionByName(mission.MissionName);
    }

    private void CompleteMission(MissionObject mission)
    {
        mission.CompleteMission();
        UIController.CompleteMission(mission);
        GameManager.Instance?.OnCompleteMission(this, mission);

        bool hasAllMissionCleared = true;
        foreach (MissionObject missionObject in missionList)
        {
            if (!missionObject.IsCompleted)
                hasAllMissionCleared = false;
        }

        if (hasAllMissionCleared)
        {
            TriggerEndOfDay();
        }
    }
    public void TriggerEndOfDay()
    {

        LoadMission(endOfDayMission);
        onEndOfDay.Invoke();
    }
}
