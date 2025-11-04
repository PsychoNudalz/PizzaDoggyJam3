using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] string speakerName;
    [SerializeField] List<DialogueOrder> dialogueList;
    [SerializeField] int dialogueIndex;
    [SerializeField]
    bool playOnStart = false;

    [SerializeField] List<SoundAbstract> soundList;
    [SerializeField] int soundIndex = 0;


    void Start()
    {
        if (playOnStart)
        {
            LoadDialogue();
        }
    }
    public void LoadDialogue()
    {
        if (dialogueList.Count > 0)
        {
            DialogueOrder dialogueOrder = dialogueList[dialogueIndex];
            UIController.LoadDialogue(new DialogueStruct(speakerName, dialogueOrder.dialogue));
            if (dialogueOrder.isIncreaseIndex)
            {
                IncreaseIndex();
            }


            if (dialogueOrder.isJumpIndex)
            {
                SetIndex(dialogueOrder.jumpToIndex);
            }

            if (soundList.Count > soundIndex)
            {
                soundList[soundIndex].PlayF();
            }


            dialogueOrder.ProcessMissions();

            if (dialogueOrder.isLoadNextDialogue)
            {
                IncreaseIndex();
                LoadDialogue();
            }

        }
    }

    public void NextDialogue()
    {
        IncreaseIndex();
        LoadDialogue();
    }

    public void IncreaseIndex()
    {
        dialogueIndex = (dialogueIndex + 1) % dialogueList.Count;
    }

    public void SetIndex(int index)
    {
        dialogueIndex = index;
    }
}
