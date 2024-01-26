using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering;

public class AmmoPickup : MonoBehaviour, IPickupable
{

    [SerializeField] int ammo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(PlayerController player)
    {
        player.GainAmmo(ammo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PickUp(other.gameObject.GetComponent<PlayerController>());
        }
        Destroy(gameObject);
    }
}
