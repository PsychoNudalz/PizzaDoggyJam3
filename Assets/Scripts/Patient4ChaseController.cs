using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Patient4ChaseController : MonoBehaviour
{

    [Serializable]
    enum ChaseState
    {
        idle, chasing, chasing_back,
        dead
    }

    [SerializeField]
    GameObject patient4Chaser;
    [SerializeField]
    ChaseState chaseState = ChaseState.idle;
    [SerializeField]
    Animator patient4Animator;
    [Header("From Room")]
    [SerializeField]
    UnityEvent onChase_FromRoom;
    [SerializeField]
    GameObject EuthanizeSyringe;
    [SerializeField]
    MissionObject chaseMission;
    [Header("From Office")]
    [SerializeField]
    UnityEvent onChase_FromOffice;
    [SerializeField]
    MissionObject findSyringeMission;
    [Header("Kill")]
    [SerializeField]
    GameObject deadPatient4;
    [SerializeField]
    GameObject patient4EuthanizeZone;

    Day3Manager day3Manager;


    void Awake()
    {
        day3Manager = FindObjectOfType<Day3Manager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        patient4EuthanizeZone.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void StartChase()
    {
        Chase_FromRoom();
        day3Manager.NotifyStartChase();
    }

    public void Chase_FromRoom()
    {
        chaseState = ChaseState.chasing;
        MissionManager.LoadMission_Object(chaseMission);

        patient4Animator.Play("Chase_Room");
        onChase_FromRoom.Invoke();
    }public void Chase_FromOffice()
    {
        chaseState = ChaseState.chasing_back;
        MissionManager.LoadMission_Object(findSyringeMission);


        patient4Animator.Play("Chase_Office");
        onChase_FromRoom.Invoke();
    }

    public void EnableKillZone()
    {

        patient4EuthanizeZone.SetActive(true);
    }

    public void KillPatient4()
    {
        chaseState = ChaseState.dead;
        patient4Chaser.SetActive(false);
        deadPatient4.SetActive(true);
        MissionManager.CompleteMission_Object(chaseMission);
        MissionManager.CompleteMission_Object(findSyringeMission);
    }
}
