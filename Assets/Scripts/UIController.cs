using DefaultNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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


    [Space(10)]
    [Header("Mission")]
    [SerializeField] Transform missionRoot;
    [SerializeField] TMP_Text[] missionDescriptions;
    [SerializeField] TMP_Text missionBigText;
    [SerializeField] Dictionary<MissionObject, TMP_Text> currentMissionDictionary = new Dictionary<MissionObject, TMP_Text>();
    [SerializeField] Animator missionAnimator;

    [Space(10)]
    [Header("Vaccine")]
    [SerializeField] Transform vaccineRoot;
    [SerializeField] TMP_Text vaccineCountText;


    [Space(10)]
    [Header("Blink")]
    [SerializeField] Transform blinkRoot;
    [SerializeField] Animator blinkAnimator;

    [Space(10)]
    [Header("Inspecting")]
    [SerializeField] Transform inspectingRoot;
    Dictionary<ItemEnum, UIInspectItem> inspectItems;

    [Space(10)]
    [Header("Hallucination")]
    [SerializeField] Transform hallucinationRoot;
    [SerializeField] Image hallucinationImage;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance.gameObject);
        }
        InitialiseItems();
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

        vaccineRoot?.gameObject.SetActive(false);
        inspectingRoot?.gameObject.SetActive(false);
        hallucinationRoot?.gameObject.SetActive(false);

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

                if (mission.ScreenFlashText != "")
                {
                    Instance.missionBigText.text = mission.ScreenFlashText;
                }
                else
                {
                    Instance.missionBigText.text = mission.Description;
                }
                Instance.missionBigText.color = mission.FlashColor;

                if (!mission.IsFlash)
                {
                    missionDescription.text = mission.Description;
                    missionDescription.gameObject.SetActive(true);
                    missionDescription.color = mission.FlashColor;


                    Instance.currentMissionDictionary.Add(mission, missionDescription);
                }
                else
                {

                }

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

    //VACCINE
    public static void ShowVaccine(int vaccineCount)
    {
        if (!Instance) return;
        if (vaccineCount > 0)
        {
            Instance.vaccineRoot.gameObject.SetActive(true);
            Instance.vaccineCountText.text = vaccineCount.ToString();
        }
        else
        {
            Instance.vaccineRoot.gameObject.SetActive(false);
        }
    }

    //BLINK
    public static void Blink(float duration = .5f)
    {
        if (!Instance) return;

        Instance.StartCoroutine(Instance.BlinkCoroutine(duration));

    }

    IEnumerator BlinkCoroutine(float duration)
    {
        blinkAnimator?.SetBool("Blink", true);
        yield return new WaitForSeconds(duration);
        blinkAnimator?.SetBool("Blink", false);

    }

    //Inspect

    void InitialiseItems()
    {
        UIInspectItem[] itemsFound = GetComponentsInChildren<UIInspectItem>(true);
        inspectItems = new Dictionary<ItemEnum, UIInspectItem>();
        foreach (UIInspectItem item in itemsFound)
        {
            inspectItems.Add(item.ItemEnum, item);
            item.gameObject.SetActive(false);
        }
    }

    public void InspectItem(ItemEnum itemEnum)
    {
        StartCoroutine(InspectItem_Delay(itemEnum));
    }
    public static void InspectItem_Static(ItemEnum itemEnum)
    {
        Instance.InspectItem(itemEnum);

    }

    IEnumerator InspectItem_Delay(ItemEnum itemEnum)
    {
        yield return new WaitForFixedUpdate();
        Debug.Log("Number of items found: " + inspectItems.Count);
        if (inspectItems.ContainsKey(itemEnum))
        {
            inspectingRoot.gameObject.SetActive(true);
            inspectItems[itemEnum].gameObject.SetActive(true);
            PlayerController.LockPlayerInput(true);
        }
    }

    public void OnInspectOff()
    {
        Debug.Log("Inspect Off");
        foreach (UIInspectItem inspectItemsValue in inspectItems.Values)
        {
            inspectItemsValue.gameObject.SetActive(false);
        }
        inspectingRoot.gameObject.SetActive(false);
        PlayerController.LockPlayerInput(false);
    }


    public static void OnInspectOff_Static()
    {
        Instance.OnInspectOff();
    }

    // Hallucination
    public static void SetHallucination(float strength = 0)
    {
        if (strength > 0.01f)
        {
            Instance.hallucinationRoot.gameObject.SetActive(true);
            var color = Instance.hallucinationImage.color;
            color.a = strength;
            Instance.hallucinationImage.color = color;
        }
        else
        {
            Instance.hallucinationRoot.gameObject.SetActive(false);
            var color = Instance.hallucinationImage.color;
            color.a = 0;
            Instance.hallucinationImage.color = color;
        }

    }

}
