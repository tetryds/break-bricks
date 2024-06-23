using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour
{
    /*
     * classe que vai prover a CameraController:
     * mouseDelta
     * vetor movimento
    */

    const int rightMouseButtonId = 1;
    Vector2 previousMousePosition;
    [SerializeField] Vector2 lookSensitivity;
    void Update()
    {
        previousMousePosition = Input.mousePosition;
    }
    public Vector2 GetMouseOffset()
    {
        if (!IsRightMouseButtonDown())
        {
            return Vector2.zero;
        }
        return ((Vector2)Input.mousePosition - previousMousePosition) * lookSensitivity;
    }
    public Vector3 GetMovementVector()
    {
        Vector3 movementVector = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movementVector.x -= 1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movementVector.z += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector.z -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementVector.x += 1f;
        }

        return movementVector.normalized;
    }
    static private bool IsRightMouseButtonDown()
    {
        return Input.GetMouseButton(rightMouseButtonId);
    }
}