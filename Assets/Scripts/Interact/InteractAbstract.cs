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

    [Header("Events")]
    [SerializeField] protected UnityEvent OnInteractEvents;

    [Header("Pop Up")]
    [SerializeField] protected bool showPopUp = true;

    [SerializeField] protected Transform popUpTransform;
    [SerializeField] protected Vector3 popUpOffset;

    [Space(10)]
    [Header("Debug")]
    [SerializeField]
    protected bool isDebug = false;


    public virtual void OnFocus_Enter()
    {
        if (!canInteract||isInteracting)
        {
            return;
        }

        isFocus = true;

        if (showPopUp)
        {
            if (!popUpTransform)
            {
                popUpTransform = transform;
            }

            Vector3 position = popUpTransform.position + popUpOffset;
            InteractManager.EnablePopUp(position, this);
        }

        if (isDebug)
            print(gameObject.name + " is focused");
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
        OnInteractEvents.Invoke();
        if (oneOffInteract)
        {
            canInteract = false;
        }

        if (interactCooldown > 0)
        {
            StartCoroutine(InteractCooldownCoroutine());
        }
    }

    IEnumerator InteractCooldownCoroutine()
    {
        isInteracting = true;
        yield return new WaitForSeconds(interactCooldown);
        isInteracting = false;
    }

    public void TestPrintEvent()
    {
        print("Interact Print");
    }
}