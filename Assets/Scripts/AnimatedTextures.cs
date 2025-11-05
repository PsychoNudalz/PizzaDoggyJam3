using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedTextures : MonoBehaviour
{

    [SerializeField]
    Animator animator;
    [SerializeField]
    string animationName;
    [SerializeField]
    Material renderMaterial;
    [SerializeField]
    Sprite currentSprite;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!string.IsNullOrEmpty(animationName))
        {
            animator.Play(animationName);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        renderMaterial.SetTexture("_MainTex", currentSprite.texture);
    }
}
