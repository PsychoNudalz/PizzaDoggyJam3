using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] string speakerName;
    [SerializeField] List<DialogueOrder> dialogueList;
    [SerializeField] int dialogueIndex;

    [SerializeField] List<SoundAbstract> soundList;
    [SerializeField]  int soundIndex = 0;

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
