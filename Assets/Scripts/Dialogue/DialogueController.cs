using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] string speakerName;
    [SerializeField] List<DialogueOrder> dialogueList;
    [SerializeField] int dialogueIndex;

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
            if (dialogueOrder.isLoadMissionIndex)
            {
                MissionManager.LoadMission_Index(dialogueOrder.loadMissionIndex);
            }
            if (dialogueOrder.isCompleteMissionIndex)
            {
                MissionManager.CompleteMission_Index(dialogueOrder.completeMissionIndex);
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
