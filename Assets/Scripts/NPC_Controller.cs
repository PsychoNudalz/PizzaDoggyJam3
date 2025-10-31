using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class NPC_Controller : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform spriteTransform;
    private Camera _camera;

#if UNITY_EDITOR
    private void Reset()
    {
        characterController = GetComponent<CharacterController>();
        if (!characterController)
            characterController = gameObject.AddComponent<CharacterController>();
    }
#endif

    private void Awake()
    {
        if (!characterController)
        {
            characterController = GetComponent<CharacterController>();
            if (!characterController)
                characterController = gameObject.AddComponent<CharacterController>();
        }

        if (!spriteTransform)
        {
            spriteTransform = GameObject.Find("Sprite_Parent").transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        spriteTransform.LookAt(_camera.transform);
        spriteTransform.rotation = Quaternion.Euler(0, spriteTransform.rotation.eulerAngles.y, 0);
    }
}