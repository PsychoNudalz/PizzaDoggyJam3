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
    List<SoundAbstract> animationSounds;
    [SerializeField]
    string animationName;
    [SerializeField]
    CinemachineVirtualCamera  virtualCamera;
    [SerializeField]
    MeshRenderer mirrorRenderer;
    [SerializeField]
    Material m_mirrorOriginal;
    // [SerializeField]
    // bool useReflected = false;
    [SerializeField]
    Material m_mirrorReflected;
    [SerializeField]
    Camera mirrorCamera;
    [SerializeField]
    [Range(0f,1f)]
    float hallucinateAmount;

    void Start()
    {
        gameManager = GameManager.Instance;
        if (m_mirrorOriginal)
        {
            mirrorRenderer.material = (m_mirrorOriginal);
        }
        mirrorCamera?.gameObject.SetActive(false);
    }

    void Update()
    {
        if (hallucinateAmount > 0.01f)
        {
            UIController.SetHallucination(hallucinateAmount);
        }
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

    public void EnableMirror()
    {
        mirrorCamera.gameObject.SetActive(true);
        mirrorCamera.enabled = true;
        mirrorRenderer.material = m_mirrorReflected;
    }

    public void PlaySound(int i)
    {
        if (animationSounds.Count > i)
        {
            animationSounds[i].Play();
        }
    }


}
