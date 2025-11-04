using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "Dialogue", order = 1)]
public class DialogueObject : ScriptableObject
{
    [TextArea] [SerializeField] string dialogue = "";

    [SerializeField] [Tooltip("Time for displaying dialogue")]
    private float dialogueDuration = 2;

    [SerializeField] [Tooltip("Time after displaying dialogue")]
    private float endDuration = 1;



    public string Dialogue => dialogue;

    public float DialogueDuration => dialogueDuration;

    public float EndDuration => endDuration;


    // public float WordInterval => 0.1f;
}