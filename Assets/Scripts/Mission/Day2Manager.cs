using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Day2Manager : GameManager
{
    [Space(5)]
    [Header("Day 2")]
    [SerializeField]
    bool isPlayerUseSyringe = false;
    [Space(5)]
    [SerializeField]
    int numberOfVaccine = 2;
    [SerializeField]
    List<MissionObject> vaccineMissionNames;
    [SerializeField]
    InteractUseVaccine[] vaccinePoints;
    [Space(5)]
    [SerializeField]
    bool isPlayerDropSyringe = false;
    [SerializeField]
    List<int> patientSaved =  new List<int>();

    [SerializeField]
    DialogueObject dialogueObject;
    [SerializeField]
    SoundAbstract screamSound;

    [SerializeField]
    [Tooltip("After patients 1,2,3 are treated, open patient 4 door")]
    UnityEvent onFinishTreating;
    [SerializeField]
    UnityEvent onDropSyringe;

    // [SerializeField]
    // InteractObject patient4Door;

    public override void OnCompleteMission(MissionManager missionManager, MissionObject mission)
    {
        base.OnCompleteMission(missionManager, mission);

        // If all the patients are treated, open P4's door
        if (vaccineMissionNames.Contains(mission))
        {
            vaccineMissionNames.Remove(mission);
        }

        if (vaccineMissionNames.Count == 0)
        {
            onFinishTreating.Invoke();
        }
    }

    public void UseVaccine(int patientID)
    {
        numberOfVaccine--;
        UIController.ShowVaccine(numberOfVaccine);
        patientSaved.Add(patientID);
        if (numberOfVaccine == 0)
        {
            foreach (InteractUseVaccine interactObject in vaccinePoints)
            {
                interactObject.gameObject.SetActive(false);
                interactObject.SetInteract(false);
            }
        }
    }

    public void LoadVaccine()
    {
        UIController.ShowVaccine(numberOfVaccine);
        isPlayerUseSyringe = true;

    }

    public void DropSyringe()
    {
        numberOfVaccine = 0;
        UIController.ShowVaccine(numberOfVaccine);
        isPlayerDropSyringe = true;
        onDropSyringe.Invoke();
        PlayerController.Blink_Static(1);
    }

    public override void LoadNextScene()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 1; i<=3; i++)
        {
            if (patientSaved.Contains(i))
            {
                PlayerPrefs.SetInt(TreatedPPrefName+i.ToString(),1);
            }
            else
            {
                PlayerPrefs.SetInt(TreatedPPrefName+i.ToString(),0);

            }
        }

        base.LoadNextScene();
    }

    public void Patient2Scream()
    {
        Debug.Log("Patient 2 scream");
        UIController.LoadDialogue(new DialogueStruct("Patient 2", dialogueObject));
        screamSound.Play();
    }

}
