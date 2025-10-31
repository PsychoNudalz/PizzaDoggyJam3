using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class DialogueObject : ScriptableObject
{
    [TextArea]
    [SerializeField] string dialogue = "";

    [SerializeField] private float duration = 3;

    public string Dialogue => dialogue;

    public float Duration => duration;
}
