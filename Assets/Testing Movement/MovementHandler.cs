using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController controller = null;

    private readonly List<IMovementModifier> modifiers = new List<IMovementModifier>();

    public void AddModifier(IMovementModifier modifier)
    {
        modifiers.Add(modifier);
    }

    public void RemoveModifier(IMovementModifier modifier)
    {
        modifiers.Remove(modifier);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = Vector3.zero;

        foreach(IMovementModifier modifier in modifiers)
        {
            movement += modifier.Value;
        }

        // Debug.Log(movement);

        controller.Move(movement * Time.deltaTime);
    }
}
