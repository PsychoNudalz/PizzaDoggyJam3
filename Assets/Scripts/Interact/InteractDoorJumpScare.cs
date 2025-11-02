using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoorJumpScare : InteractAnimator
{
    [Header("Jump Scare")]
    [SerializeField]
    CinemachineVirtualCamera virtualCamera;


    public override void OnInteract()
    {
        base.OnInteract();
        StartCoroutine(JumpScare());
    }


    IEnumerator JumpScare()
    {
        virtualCamera.Priority = 30;
        // PlayerController.Blink_Static();
        PlayerController.DisablePlayerInput_Static(5);
        animator.Play("JumpScare");
        yield return new WaitForSeconds(6.3f);
        PlayerController.Blink_Static(.5f);
        yield return new WaitForSeconds(.5f);
        GameManager.ResetScene();
        virtualCamera.Priority = 10;

    }
}
