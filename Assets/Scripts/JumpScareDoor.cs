using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareDoor : MonoBehaviour
{
    [SerializeField]
    InteractDoorJumpScare interactDoorJumpScare;

    [SerializeField]
    float jumpScareRange = 5f;
    [SerializeField]
    float jumpScareDotThreshold_Camera = .9f;
    [SerializeField]
    Transform dotForward;
    [SerializeField]
    float jumpScareDotThreshold_Perpendicular = .9f;
    [SerializeField]
    bool isInJumpScareZone = false;


    Transform player;
    Transform mainCamera;
    [SerializeField]
    float dot;

    void Awake()
    {
        interactDoorJumpScare = GetComponent<InteractDoorJumpScare>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInJumpScareZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInJumpScareZone = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.Instance.transform;
        mainCamera = Camera.main?.transform;
        if (!interactDoorJumpScare)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isInJumpScareZone && player && mainCamera)
        {

            dot = Vector3.Dot(mainCamera.forward, dotForward.forward);
            if (dot > jumpScareDotThreshold_Camera)
            {
                if (interactDoorJumpScare)
                {
                    interactDoorJumpScare.OnInteract();
                    enabled = false;
                }
                else
                {
                    Debug.LogError("No InteractDoorJumpScare found");
                    interactDoorJumpScare = GetComponentInChildren<InteractDoorJumpScare>();

                }
            }
        }
    }
}
