using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] string speakerName;
    [SerializeField] List<DialogueObject> dialogueList;
    [SerializeField] int dialogueIndex;

    public void LoadDialogue()
    {
        if (dialogueList.Count > 0)
        {
            UIController.LoadDialogue(new DialogueStruct(speakerName, dialogueList[dialogueIndex]));
        }
    }

    public void NextDialogue()
    {
        IncreaseIndex();
        LoadDialogue();
    }

    public void IncreaseIndex()
    {
        dialogueIndex= (dialogueIndex + 1) % dialogueList.Count;
    }

    public void SetIndex(int index)
    {
        dialogueIndex = index;
    }
}
