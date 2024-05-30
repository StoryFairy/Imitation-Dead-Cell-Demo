using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void BeginInteract();
    void Interact();
    void EndInteract();
    bool IsInteractionAllowed();
}
