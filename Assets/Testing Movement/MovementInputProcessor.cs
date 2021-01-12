using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInputProcessor : MonoBehaviour, IMovementModifier
{
    [Header("References")]
    [SerializeField] private CharacterController controller = null;
    [SerializeField] private MovementHandler movementHandler = null;
    [SerializeField] private Camera playerCamera = null;
    [SerializeField] private PlayerInputActions playerInput;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float acceleration = 0.1f;

    private float currentSpeed;

    private Vector3 previousVelocity;
    private Vector2 previousInputDirection;

    private Transform mainCameraTransform;

    public Vector3 Value { get; private set; }

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

    private void OnEnable()
    {
        movementHandler.AddModifier(this);
        Controls.Enable();
    }

    private void OnDisable()
    {
        movementHandler.RemoveModifier(this);
        Controls.Disable();
    }

    private void Start()
    {
        mainCameraTransform = playerCamera.transform;
    }

    private void Update()
    {
        Move();
    }

    public void Awake()
    {
        Controls.Player.Movement.performed += ctx => SetMovementInput(ctx.ReadValue<Vector2>());
    }

    public void SetMovementInput(Vector2 inputDirection)
    {
        previousInputDirection = inputDirection;
    }

    private void Move()
    {
        float targetSpeed = movementSpeed * previousInputDirection.magnitude;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        Vector3 forward = mainCameraTransform.forward;
        Vector3 right = mainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movementDirection;

        //Debug.Log(targetSpeed);

        if(targetSpeed != 0f)
        {
            movementDirection = forward * previousInputDirection.y + right * previousInputDirection.x;
        }
        else
        {
            //movementDirection = previousInputDirection;
            movementDirection = new Vector3(0f, 0f, 0f);
        }

        Value = movementDirection * currentSpeed;

        previousVelocity = new Vector3(controller.velocity.x, 0f, controller.velocity.z);

        currentSpeed = previousVelocity.magnitude;
    }
}
