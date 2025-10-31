using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractAbstract : MonoBehaviour
{
    private bool IsFocus = false;
    private bool IsInteracting = false;

    [Header("Events")]
    [SerializeField] UnityEvent OnInteractEvents;
    [Header("Debug")]
    [SerializeField] bool isDebug = false;



    public virtual void OnFocus_Enter()
    {
        IsFocus = true;
        if(isDebug)
            print(gameObject.name + " is focused");
    }

    public virtual void OnFocus_Exit()
    {
        IsFocus = false;
        if(isDebug)
            print(gameObject.name + " exit focuse");
    }

    public virtual void OnInteract()
    {
        OnInteractEvents.Invoke();
    }

    public void TestPrintEvent()
    {
        print("Interact Print");
    }
}