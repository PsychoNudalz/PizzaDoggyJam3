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
    }

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

    void UpdateDialogueText()
    {
    }
}