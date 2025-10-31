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
    }

    void LoadNextDialogue()
    {
        if (dialogueQueue.Count > 0)
        {
            currentDialogue = dialogueQueue[0];
            dialogueQueue.RemoveAt(0);
            StopDialogueCoroutine();

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
    }

    IEnumerator DialogueCoroutine()
    {
        dialogueText.text = "";
        DialogueObject currentDialogueDialogue = currentDialogue.dialogue;
        dialogueWords = new List<string>(currentDialogueDialogue.Dialogue.Split(" "));
        wordInterval = currentDialogueDialogue.DialogueDuration / dialogueWords.Count;
        while (dialogueWords.Count > 0)
        {
            dialogueText.text = dialogueWords[0];
            dialogueWords.RemoveAt(0);
            yield return new WaitForSeconds(wordInterval);
        }

        yield return new WaitForSeconds(currentDialogueDialogue.EndDuration);

        StopDialogueCoroutine();
    }

    void UpdateDialogueText()
    {

    }
}