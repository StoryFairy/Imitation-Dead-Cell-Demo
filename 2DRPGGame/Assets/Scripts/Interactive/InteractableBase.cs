using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class InteractableBase : MonoBehaviour, IInteractable
{ 
    public GameObject interactableImage;
    public Player player;
    
    public virtual void BeginInteract()
    {
        interactableImage.gameObject.SetActive(true);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }


    public virtual void Interact()
    {

    }

    public virtual void EndInteract()
    {
        interactableImage.gameObject.SetActive(false);
    }

    public virtual bool IsInteractionAllowed()
    {
        return gameObject.activeSelf;
    }

    public void OnDisable()
    {
        EndInteract();
    }
}
