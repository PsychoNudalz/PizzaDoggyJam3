using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class InteractAnimator : InteractAbstract
{
    [Header("Animator")]
    [SerializeField] Animator animator;

    [SerializeField] private bool isToggle = true;
    private bool toggleFlag = false;
    [SerializeField] string animatiorTrigger_Interact = "Interact";
    [SerializeField] string animatiorTrigger_ToggleOff = "InteractOff";
    // [SerializeField]

    private void Awake()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
    }

    protected override void InteractLogic()
    {
        if(isInteracting) return;

        if (!isToggle)
        {
            animator?.SetTrigger(animatiorTrigger_Interact);
        }
        else
        {
            if (toggleFlag)
            {
                toggleFlag = false;
                animator?.SetTrigger(animatiorTrigger_ToggleOff);
            }
            else
            {
                animator?.SetTrigger(animatiorTrigger_Interact);
                toggleFlag = true;
            }
        }

        base.InteractLogic();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}