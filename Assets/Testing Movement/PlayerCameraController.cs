using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("References")]
    //[SerializeField] private CharacterController controller = null;
    //[SerializeField] private MovementHandler movementHandler = null;

    [Header("Camera")]
    [SerializeField] private Vector2 cameraVelocity = new Vector2(1f, 1f);
    [SerializeField] private Transform playerTransform = null;

    private PlayerInputActions playerInput;

    private float xRotation = 0f;
    private PlayerInputActions Controls
    {
        get
        {
            if (playerInput != null) 
            { 
                return playerInput; 
            }
            return playerInput = new PlayerInputActions();
        }
    }

    public void Awake()
    {
        Controls.Player.MouseAim.performed += ctx => Look(ctx.ReadValue<Vector2>());
    }

    private void OnEnable() => Controls.Enable();
    private void OnDisable() => Controls.Disable();

    private void Look(Vector2 lookAxis)
    {
        float deltaTime = Time.deltaTime;

        float mouseX = lookAxis.x * cameraVelocity.x * deltaTime;
        float mouseY = lookAxis.y * cameraVelocity.y * deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTransform.Rotate(0f, mouseX, 0f);
    }
}
