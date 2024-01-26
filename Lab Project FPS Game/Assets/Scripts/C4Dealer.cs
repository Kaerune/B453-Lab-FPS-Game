using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Dealer : MonoBehaviour, IInteractable
{
    [SerializeField] int C4Amount;
    public float interactionRange = 5f;
    [SerializeField] ParticleSystem interacted;

    void Update()
    {
        // If player presses E...
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Casts a collider sphere to check for if the player is in range
            Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);

            // Goes through all things collided with
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    // If one of the colliders was a player, do the Interact method and play the particle system
                    Interact(collider.GetComponent<PlayerController>());
                    interacted.Play();
                }
            }
        }
    }


    public void Interact(PlayerController player)
    {
        player.GainC4(C4Amount);
    }
}
