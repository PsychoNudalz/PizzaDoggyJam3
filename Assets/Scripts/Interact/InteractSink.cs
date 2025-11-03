using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSink : InteractObject
{
    [Header("Sink")]
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    Animator animator;
    [SerializeField]
    string animationName;
    [SerializeField]
    CinemachineVirtualCamera  virtualCamera;

    void Start()
    {
        gameManager = GameManager.Instance;
    }
    protected override void InteractLogic()
    {
        base.InteractLogic();
        PlayerController.LockPlayerInput(true);
        virtualCamera.Priority = 40;
        animator.Play(animationName);
    }
    void LoadNextScene()
    {
        gameManager?.LoadNextScene();
    }
}
