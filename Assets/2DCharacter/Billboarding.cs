using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    [SerializeField] private Camera _mainCam;

    private void LateUpdate()
    {
        Vector3 cameraPosition = _mainCam.transform.position;
        cameraPosition.y = transform.position.y;
        transform.LookAt(cameraPosition);
        transform.Rotate(0f, 180f, 0f);
    }
}