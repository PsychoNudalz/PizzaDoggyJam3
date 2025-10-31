using UnityEngine;
using System;


[System.Serializable]
public class DialogueOrder
{
    [SerializeField] public DialogueObject dialogue;

    [SerializeField] public bool increaseIndex;

    [SerializeField] public int jumpToIndex = -1;

    public DialogueOrder(DialogueObject dialogue, bool increaseIndex, int jumpToIndex = -1)
    {
        this.dialogue = dialogue;
        this.jumpToIndex = jumpToIndex;
        this.increaseIndex = increaseIndex;
    }
}