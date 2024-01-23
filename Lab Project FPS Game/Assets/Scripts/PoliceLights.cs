using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceLights : MonoBehaviour
{
    [Header("Light Specs")]
    [Tooltip("Delay in seconds between flashes.")]
    [SerializeField] float flashRate;
    [Tooltip("Lights will start flashing after offset time has passed.")]
    [SerializeField] float offsetDelay;

    // Reference to the light component.
    private Light light;


    // Start is called before the first frame update
    void Start()
    {
        // Setting light to be the light component on the gameobject this script is attached to.
        light = GetComponent<Light>();
        // Continually flash the lights. The first argument is what method to repeat, the second
        // is the delay after the object spawns before it starts calling the method.
        // And the third argument is how long to wait before the method is called again.
        InvokeRepeating("ToggleLights", offsetDelay, flashRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to toggle the lights on and off.
    void ToggleLights()
    {
        // The light state is set to the opposite of what it currently is.
        light.enabled = !light.enabled;
    }
}