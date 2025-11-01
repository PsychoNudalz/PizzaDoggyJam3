using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("Dialogue")]
    [SerializeField] Transform dialogueRoot;

    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text speakerNameText;

    [SerializeField] bool isDialogue = false;
    [SerializeField] DialogueStruct currentDialogue;
    [SerializeField] List<DialogueStruct> dialogueQueue;
    [SerializeField] List<string> dialogueWords;

    [SerializeField] [Tooltip("Time between each word")]
    float wordInterval;

    Coroutine dialogueCoroutine;


    [Space(14)]
    [Header("Mission")]
    [SerializeField] Transform missionRoot;
    [SerializeField] TMP_Text[] missionDescriptions;
    [SerializeField] TMP_Text missionBigText;
    [SerializeField] Dictionary<MissionObject,TMP_Text> currentMissionDictionary =  new Dictionary<MissionObject,TMP_Text>();
    [SerializeField] Animator missionAnimator;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    private void Start()
    {
        if (dialogueRoot)
        {
            dialogueRoot.gameObject.SetActive(false);
        }
        foreach (TMP_Text missionDescription in Instance.missionDescriptions)
        {
            missionDescription.gameObject.SetActive(false);
        }
    }


    // DIALOGUE
    public static void LoadDialogue(DialogueStruct dialogue)
    {
        if (!Instance)
        {
            Debug.LogError("UIController is not initialized");
            return;
        }

        Instance.QueueDialogue(dialogue);
    }

    public void QueueDialogue(DialogueStruct dialogue)
    {
        dialogueQueue.Add(dialogue);
        if (!isDialogue)
        {
            LoadNextDialogue();
        }
        // if (currentDialogue)
    }

    void LoadNextDialogue()
    {
        StopDialogueCoroutine();

        if (dialogueQueue.Count > 0)
        {
            currentDialogue = dialogueQueue[0];
            dialogueQueue.RemoveAt(0);

            dialogueCoroutine = StartCoroutine(DialogueCoroutine());
        }
    }

    private void StopDialogueCoroutine()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
            dialogueCoroutine = null;
        }

        isDialogue = false;
        dialogueRoot.gameObject.SetActive(false);
    }

    IEnumerator DialogueCoroutine()
    {
        isDialogue = true;
        dialogueRoot.gameObject.SetActive(true);

        DialogueObject currentDialogueDialogue = currentDialogue.dialogue;
        dialogueText.text = "";
        speakerNameText.text = currentDialogue.speakerName;
        Debug.Log("Start Dialogue: " + currentDialogueDialogue.Dialogue);

        dialogueWords = new List<string>(currentDialogueDialogue.Dialogue.Split(" "));
        wordInterval = currentDialogueDialogue.DialogueDuration / dialogueWords.Count;
        while (dialogueWords.Count > 0)
        {
            dialogueText.text += dialogueWords[0];
            if (dialogueWords.Count > 1)
            {
                dialogueText.text += " ";
            }

            dialogueWords.RemoveAt(0);
            yield return new WaitForSeconds(wordInterval);
        }

        yield return new WaitForSeconds(currentDialogueDialogue.EndDuration);

        LoadNextDialogue();
    }


    // MISSION

    public static void LoadMission(MissionObject mission)
    {
        if (Instance.currentMissionDictionary.ContainsKey(mission))
        {
            Debug.Log("Mission Already loaded: " + mission);
            return;
        }

        foreach (TMP_Text missionDescription in Instance.missionDescriptions)
        {
            if (!missionDescription.gameObject.activeSelf)
            {
                Instance.currentMissionDictionary.Add(mission, missionDescription);
                Instance.missionBigText.text = mission.Description;
                missionDescription.text = mission.Description;
                missionDescription.gameObject.SetActive(true);
                Instance.missionAnimator.SetTrigger("Mission");

                return;
            }
        }

        Debug.LogError("Mission Array full");
    }

    public static void CompleteMission(MissionObject mission)
    {
        if (!Instance.currentMissionDictionary.ContainsKey(mission))
        {
            Debug.LogError("mission not in UI: " + mission);
            return;
        }

        Instance.currentMissionDictionary[mission].gameObject.SetActive(false);
    }
}