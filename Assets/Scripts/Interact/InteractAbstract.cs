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
    [Header("Pop Up")]
    [SerializeField] bool showPopUp = true;
    [SerializeField] Transform popUpTransform;
    [SerializeField] Vector3 popUpOffset;
    [Space(10)]
    [Header("Debug")]
    [SerializeField] bool isDebug = false;



    public virtual void OnFocus_Enter()
    {
        IsFocus = true;

        if (showPopUp)
        {
            if (!popUpTransform)
            {
                popUpTransform = transform;
            }

            Vector3 position = popUpTransform.position + popUpOffset;
            InteractManager.EnablePopUp(position,this);
        }

        if(isDebug)
            print(gameObject.name + " is focused");
    }

    public virtual void OnFocus_Exit()
    {
        IsFocus = false;
        if (showPopUp)
        {
            InteractManager.DisablePopUp();
        }

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