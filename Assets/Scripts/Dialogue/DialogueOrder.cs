using UnityEngine;
using System;


[System.Serializable]
public class DialogueOrder
{
    [SerializeField] public DialogueObject dialogue;

    [SerializeField] public bool isIncreaseIndex;
    [SerializeField] public bool isJumpIndex;
    [SerializeField] public int jumpToIndex = -1;


    [SerializeField] public bool isLoadMissionIndex;
    [SerializeField] [Tooltip("Load Mission by Index")]
    public int loadMissionIndex = -1;

    [SerializeField] public bool isCompleteMissionIndex;
    [SerializeField] [Tooltip("Complete Mission by Index")]
    public int completeMissionIndex = -1;

    public DialogueOrder()
    {
        this.dialogue = null;
        this.isIncreaseIndex = false;
        this.jumpToIndex = -1;
        this.loadMissionIndex = -1;
        this.completeMissionIndex = -1;
    }


    // public DialogueOrder(DialogueObject dialogue, bool increaseIndex, int jumpToIndex = -1, int loadMissionIndex = -1, int completeMissionIndex = -1)
    // {
    //     this.dialogue = dialogue;
    //     this.increaseIndex = increaseIndex;
    //     this.jumpToIndex = jumpToIndex;
    //     this.loadMissionIndex = loadMissionIndex;
    //     this.completeMissionIndex = completeMissionIndex;
    // }
}
