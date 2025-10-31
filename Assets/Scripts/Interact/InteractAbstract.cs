using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractAbstract : MonoBehaviour
{
    [SerializeField] private bool IsFocus = false;

    public virtual void OnFocus_Enter()
    {
        IsFocus = true;
        print(gameObject.name + " is focused");
    }

    public virtual void OnFocus_Exit()
    {
        IsFocus = false;
        print(gameObject.name + " exit focuse");

    }
}
