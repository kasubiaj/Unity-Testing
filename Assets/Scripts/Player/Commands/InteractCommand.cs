using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCommand : Command
{
    private Transform transform;
    private IInteractable interactedWith;

    private const string LayerName = "Interactable";

    private void Awake()
    {
        transform = base.transform;
    }

    public override void Execute()
    {
        Physics.Raycast(transform.position + Vector3.up, transform.forward, out var hit, 2f, LayerMask.GetMask(LayerName));

        Debug.Log($"{gameObject.name} is trying to interact!");

        if (hit.collider == null)
        {
            return;
        }

        Debug.Log($"{gameObject.name} is interacted with {hit.collider.name}");

        interactedWith = hit.collider.GetComponent<IInteractable>();
        interactedWith?.Interact();

    }
}
