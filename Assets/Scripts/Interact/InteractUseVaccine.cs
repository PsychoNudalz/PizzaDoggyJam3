using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUseVaccine : InteractObject
{
    [Header("Vaccine")]
    [SerializeField]
    int patientID = 0;
    Day2Manager day2Manager;
    [SerializeField]
    MissionObject vaccineMissionObject;

    // Start is called before the first frame update
    void Start()
    {
        day2Manager = FindObjectOfType<Day2Manager>();
    }

    public override void OnInteract()
    {
        base.OnInteract();
        day2Manager.UseVaccine(patientID);
        MissionManager.CompleteMission_Object(vaccineMissionObject);
    }
}
