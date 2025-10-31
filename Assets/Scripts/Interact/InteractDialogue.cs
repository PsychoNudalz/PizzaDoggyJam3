using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]
public class InteractDialogue : InteractAbstract
{
    [SerializeField] DialogueController dialogueController;

    private void Awake()
    {
        if (!dialogueController)
        {
            dialogueController = GetComponent<DialogueController>();
        }
    }

    public override void OnInteract()
    {
        base.OnInteract();

    }
}
