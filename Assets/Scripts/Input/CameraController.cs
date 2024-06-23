using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*
     * deve ser capaz de girar a câmera em torno do próprio eixo com base em offset de mouse
     * deve ser capaz de mover o personagem com base em um vetor direção
    */
    [SerializeField] CameraInput cameraInput;

    [SerializeField] Transform cameraTransform;
    [SerializeField] float moveSpeed = 0.5f;

    void Update()
    {
        RotateCameraBasedOnMouseOffset(cameraInput.GetMouseOffset());

        MoveCamera(cameraInput.GetMovementVector() * moveSpeed* Time.deltaTime);
    }
    private void MoveCamera(Vector3 displacement)
    {
        cameraTransform.Translate(displacement, Space.Self);
    }
    private void RotateCameraBasedOnMouseOffset(Vector2 mouseOffset)
    {
        RotateCameraOverPoint(-mouseOffset.y, mouseOffset.x);
    }
    private void RotateCameraOverPoint(float pitch, float yaw)
    {
        cameraTransform.Rotate(pitch, 0f, 0f, Space.Self);
        cameraTransform.Rotate(0f, yaw, 0f, Space.World);
    }
}
