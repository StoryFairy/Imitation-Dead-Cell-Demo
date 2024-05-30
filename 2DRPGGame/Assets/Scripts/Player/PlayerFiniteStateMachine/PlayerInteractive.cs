using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractive : MonoBehaviour
{
    public Player player;
    private IInteractable interactableInFocus;

    public void Update()
    {
        if (interactableInFocus != null)
        {
            if (interactableInFocus.IsInteractionAllowed())
            {
                interactableInFocus.Interact();
                player.InputHandler.UseInteractInput();
            }
            else
            {
                interactableInFocus.EndInteract();
                interactableInFocus = null;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        var interactable = collider.GetComponent<InteractableBase>();
        
        if (interactable == null || !interactable.IsInteractionAllowed())
        {
            return;
        }
        interactableInFocus?.EndInteract();
        interactableInFocus = interactable;
        interactableInFocus.BeginInteract();
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        var interactable = collider.GetComponent<IInteractable>();
        if (interactable == interactableInFocus)
        {
            interactableInFocus?.EndInteract();
            interactableInFocus = null;
        }
    }
}
