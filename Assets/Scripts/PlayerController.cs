using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;
    bool inputLock = false;


    public static PlayerController Instance;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

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
        Instance.playerInput.enabled = !lockInput;
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
}
