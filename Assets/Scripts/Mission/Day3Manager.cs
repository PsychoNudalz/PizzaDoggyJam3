using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day3Manager : GameManager
{

    [Header("Patient Kits")]
    [SerializeField]
    List<PatientKit> patientKits = new List<PatientKit>();
    [SerializeField]
    List<int> savedPatients = new List<int>();

    [Header("Patient 4")]
    [SerializeField]
    Patient4ChaseController patient4ChaseController;
    [SerializeField]
    bool startChasing = false;
    [SerializeField]
    MissionObject useSyringeMission;
    // Start is called before the first frame update
    void Start()
    {
        if (patientKits.Count > 0)
        {

            for (int i = 1; i <= 3; i++)
            {
                int isCurrentPatientAlive = PlayerPrefs.GetInt(TreatedPPrefName + i.ToString());
                patientKits[i - 1].SetDead(isCurrentPatientAlive == 0);
                savedPatients.Add(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NotifyStartChase()
    {
        startChasing = true;
    }

    public override void OnCompleteMission(MissionManager missionManager, MissionObject mission)
    {
        base.OnCompleteMission(missionManager, mission);
        if (mission.Equals(useSyringeMission))
        {
            patient4ChaseController.EnableKillZone();
        }
    }


}
