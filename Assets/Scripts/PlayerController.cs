using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    FirstPersonController firstPersonController;
    [SerializeField]
    InteractSystem interactSystem;
    bool inputLock = false;


    public static PlayerController Instance;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        firstPersonController = GetComponent<FirstPersonController>();
        interactSystem = GetComponent<InteractSystem>();

        if (Instance)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void LockPlayerInput(bool lockInput)
    {
        Instance.inputLock = lockInput;
        Instance.firstPersonController.enabled = !lockInput;
        Instance.interactSystem.SetRaycast(!lockInput);
    }

    public static void DisablePlayerInput_Static(float duration)
    {
        Instance?.DisablePlayerInput(duration);
    }

    public void DisablePlayerInput(float duration)
    {
        StartCoroutine(WaitDisablePlayerInput(duration));
    }




    IEnumerator WaitDisablePlayerInput(float duration)
    {
        LockPlayerInput(true);
        yield return new WaitForSeconds(duration);
        LockPlayerInput(false);
    }

    public void Blink(float duration = 0.5f)
    {
        UIController.Blink(duration);
    }
    public static void Blink_Static(float duration = 0.5f)
    {
        UIController.Blink(duration);
    }

    public void OnInspectOff()
    {
        UIController.OnInspectOff_Static();
    }

    public static void TeleportPlayer_Static(Vector3 position)
    {
        Instance?.TeleportPlayer(position);
    }

    public void TeleportPlayer(Vector3 position)
    {
        if (firstPersonController.enabled)
        {
            StartCoroutine(TeleportRoutine(position));
        }
    }

    IEnumerator TeleportRoutine(Vector3 position)
    {
        firstPersonController.enabled = false;
        Blink();

        yield return new WaitForFixedUpdate();
        transform.position = position;

        yield return new WaitForFixedUpdate();
        firstPersonController.enabled = true;
    }


}
