using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }



    [SerializeField] private List<MissionObject> missionList = new List<MissionObject>();

    [Header("End of day")]
    [SerializeField]
    MissionObject endOfDayMission;
    [SerializeField]
    UnityEvent onEndOfDay;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        List<MissionObject> temp =  new List<MissionObject>();
        foreach (MissionObject missionObject in missionList)
        {
            temp.Add(Instantiate(missionObject));
        }
        missionList = temp;
    }

    void Start()
    {

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

    public static void CompleteMission_Name(string name)
    {
        Instance?.CompleteMissionByName(name);
    }

    public static void CompleteMission_Index(int index)
    {
        Instance?.CompleteMissionByIndex(index);
    }

    // ---------- INSTANCE METHODS ----------

    public void LoadMissionByName(string name)
    {
        var mission = missionList.Find(m => m.MissionName == name);
        if (mission != null)
        {
            LoadMission(mission);
        }
        else Debug.LogWarning($"Mission '{name}' not found.");
    }
    void LoadMission(MissionObject mission)
    {

        mission.LoadMission();
        UIController.LoadMission(mission);
    }

    public void LoadMissionByIndex(int index)
    {
        if (index >= 0 && index < missionList.Count)
        {
            LoadMission(missionList[index]);

        }

        else
            Debug.LogWarning($"Mission index {index} invalid.");
    }

    public void CompleteMissionByName(string name)
    {
        var mission = missionList.Find(m => m.MissionName == name);
        if (mission != null)
        {
            CompleteMission(mission);
        }
        else Debug.LogWarning($"Mission '{name}' not found.");
    }
    void CompleteMission(MissionObject mission)
    {
        mission.CompleteMission();
        UIController.CompleteMission(mission);

        bool hasAllMissionCleared = true;
        foreach (MissionObject missionObject in missionList)
        {
            if (!missionObject.IsCompleted)
            {
                hasAllMissionCleared = false;
            }
        }

        if (hasAllMissionCleared)
        {
            LoadMission(endOfDayMission);
            onEndOfDay.Invoke();
        }
    }

    public void CompleteMissionByIndex(int index)
    {
        if (index >= 0 && index < missionList.Count)
        {
            CompleteMission(missionList[index]);
        }
        else
            Debug.LogWarning($"Mission index {index} invalid.");
    }




}
