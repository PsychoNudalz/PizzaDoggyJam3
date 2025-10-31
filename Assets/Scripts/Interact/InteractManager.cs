using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager Instance;
    public GameObject interactPopUp;

    private static InteractAbstract currentInteractObject;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (interactPopUp)
        {
            interactPopUp.SetActive(false);
        }
    }

    public static void EnablePopUp(Vector3 position, InteractAbstract interactObject)
    {
        if (Instance == null || Instance.interactPopUp == null) return;

        Instance.interactPopUp.SetActive(true);
        Instance.interactPopUp.transform.position = position;
        currentInteractObject = interactObject;
    }

    public static void DisablePopUp()
    {
        if (Instance == null || Instance.interactPopUp == null) return;

        Instance.interactPopUp.SetActive(false);
        currentInteractObject = null;
    }
}

