using System;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }


    [SerializeField] private List<MissionObject> missionList = new List<MissionObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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
            mission.LoadMission();
            UIController.LoadMission(mission);
        }
        else Debug.LogWarning($"Mission '{name}' not found.");
    }

    public void LoadMissionByIndex(int index)
    {
        if (index >= 0 && index < missionList.Count)
        {
            missionList[index].LoadMission();
            UIController.LoadMission(missionList[index]);

        }

        else
            Debug.LogWarning($"Mission index {index} invalid.");
    }

    public void CompleteMissionByName(string name)
    {
        var mission = missionList.Find(m => m.MissionName == name);
        if (mission != null)
        {
            mission.CompleteMission();
            UIController.CompleteMission(mission);
        }
        else Debug.LogWarning($"Mission '{name}' not found.");
    }

    public void CompleteMissionByIndex(int index)
    {
        if (index >= 0 && index < missionList.Count)
        {
            missionList[index].CompleteMission();
            UIController.CompleteMission(missionList[index]);
        }
        else
            Debug.LogWarning($"Mission index {index} invalid.");
    }
}
