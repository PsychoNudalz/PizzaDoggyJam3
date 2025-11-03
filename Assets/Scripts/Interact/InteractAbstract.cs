using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractAbstract : MonoBehaviour
{
    protected bool isFocus = false;
    protected bool isInteracting = false;

    [Header("Settings")]
    [SerializeField] protected bool canInteract = true;

    [SerializeField] float interactCooldown = 1f;

    [SerializeField] [Tooltip("Does it disables after interacting")]
    protected bool oneOffInteract = false;

    [Space(5)]
    [SerializeField] bool isAutoTrigger = false;

    [Header("Events")]
    [SerializeField] protected UnityEvent OnInteractEvents;
    [Header("One-off Dialogue")]
    [SerializeField]
    [Tooltip("Ignore the index next stuff")]
    DialogueOrder singleDialogueOrder;

    [Header("Pop Up")]
    [SerializeField] protected bool showPopUp = true;
    [SerializeField] protected string popUpText = "";
    [SerializeField] protected Transform popUpTransform;
    [SerializeField] protected Vector3 popUpOffset;

    [Space(10)]
    [Header("Debug")]
    [SerializeField]
    protected bool isDebug = false;

    public bool CanInteract() => canInteract && !isInteracting;

    public void SetInteract(bool b)
    {
        canInteract = b;
    }
    public virtual bool OnFocus_Enter()
    {
        if (!canInteract || isInteracting)
        {
            return false;
        }

        isFocus = true;

        if (showPopUp)
        {
            if (!popUpTransform)
            {
                popUpTransform = transform;
            }

            Vector3 position = popUpTransform.position + popUpOffset;
            InteractManager.EnablePopUp(position, this,popUpText);
        }

        if (isDebug)
            print(gameObject.name + " is focused");
        return true;
    }

    public virtual void OnFocus_Exit()
    {
        isFocus = false;
        if (showPopUp)
        {
            InteractManager.DisablePopUp();
        }

        if (isDebug)
            print(gameObject.name + " exit focuse");
    }


    public virtual void OnInteract()
    {
        if (isInteracting||isAutoTrigger) return;
        InteractLogic();
    }

    protected virtual void InteractLogic()
    {
        OnInteractEvents.Invoke();
        if (oneOffInteract)
        {
            canInteract = false;
        }

        if (interactCooldown > 0&&gameObject.activeSelf)
        {
            StartCoroutine(InteractCooldownCoroutine());
        }

        if (singleDialogueOrder.dialogue)
        {
            LoadSingleDialogue();
        }
        OnFocus_Exit();
    }

    IEnumerator InteractCooldownCoroutine()
    {
        isInteracting = true;
        yield return new WaitForSeconds(interactCooldown);
        isInteracting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTiggerEnter_Interact();
    }

    private void OnTiggerEnter_Interact()
    {
        if (isAutoTrigger&& !isInteracting && canInteract)
        {
            InteractLogic();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isAutoTrigger && !isInteracting && canInteract)
        {
            OnTiggerEnter_Interact();

        }
    }

    private void OnTriggerExit(Collider other)
    {

    }

    public void TestPrintEvent()
    {
        print("Interact Print");
    }


    protected virtual void LoadSingleDialogue()
    {
        UIController.LoadDialogue(new DialogueStruct("",singleDialogueOrder.dialogue));

        singleDialogueOrder.ProcessMissions();



    }
}
