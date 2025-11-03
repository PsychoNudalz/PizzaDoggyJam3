using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager Instance;
    public InteractPopUp interactPopUp;

    private InteractAbstract currentInteractObject;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (interactPopUp)
        {
            interactPopUp.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (currentInteractObject)
        {
            interactPopUp.transform.position = currentInteractObject.PopUpPosition();
        }
    }

    public static void EnablePopUp(Vector3 position, InteractAbstract interactObject, string text="")
    {
        if (Instance == null || Instance.interactPopUp == null) return;

        Instance.interactPopUp.ShowPopUp(position,text);
        Instance.currentInteractObject = interactObject;
    }

    public static void DisablePopUp()
    {
        if (Instance == null || Instance.interactPopUp == null) return;

        Instance.interactPopUp.HidePopUp();
        Instance.currentInteractObject = null;
    }
}

