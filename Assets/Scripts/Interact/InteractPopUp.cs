using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPopUp : MonoBehaviour
{
    [SerializeField] Animator animator;
    Transform mainCamera;

    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        if (mainCamera == null) return;

        Vector3 direction = transform.position - mainCamera.position;
        direction.y = 0; // optional: keep upright
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void ShowPopUp()
    {
        animator?.SetTrigger("ShowPopUp");
    }
}