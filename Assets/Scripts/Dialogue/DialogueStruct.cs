using System;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct DialogueStruct
{
    public string speakerName;
    public DialogueObject dialogue;

    public DialogueStruct(string speakerName, DialogueObject dialogue)
    {
        this.speakerName = speakerName;
        this.dialogue = dialogue;
    }
}
