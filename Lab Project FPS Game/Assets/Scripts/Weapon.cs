using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Details")]
    [Tooltip("Name of the weapon.")]
    [SerializeField] protected string gunName;
    [Tooltip("Current bullets loaded into the weapon.")]
    [SerializeField] protected int currentBullets;
    [Tooltip("The maximum amount of bullets this weapon can hold at once.")]
    [SerializeField] protected int maxBullets;

    [Header("References")]
    [Tooltip("Decal to use for bullet holes.")]
    [SerializeField] protected GameObject bulletHole;

    // Fire a bullet from the weapon.
    public virtual void Shoot()
    {
        // Check to make sure there are bullets loaded into the gun.
        if (currentBullets > 0)
        {
            // Define a RaycastHit object (this stores data about an object that a raycast hits.)
            RaycastHit hit;

            // Shoot the raycast from the main camera, in the forward direction the camera is facing, store the data in "hit" and the ray distance is infinite.
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
            {
                // Instantiate the decal gameobject for the bullet hole at the location the ray hit and save a reference to it.
                GameObject decal = Instantiate(bulletHole, hit.point, Quaternion.identity);
                // Make the forward (Z) axis of the decal face in the opposite direction of the normal vector of the thing that it hit.
                decal.transform.forward = -hit.normal;
                // Move the decal backwards 1 centimeters away from the surface that it hit.
                decal.transform.Translate(Vector3.back * 0.01f);

                // Save the world position of the decal.
                Vector3 worldPosition = decal.transform.position;
                // Save the world rotation of the decal.
                Quaternion worldRotation = decal.transform.rotation;
                // Make the object the ray hit the parent of the decal. This is necessary to make sure the bullethole stays on the surface of the object if the object moves.
                // NOTE: This will automatically move the bullethole to the incorrect spot because its location and rotation are now relative to the parent.
                decal.transform.SetParent(hit.transform);
                // Move the bullethole back to the right position it was in before.
                decal.transform.position = worldPosition;
                // Move the bullethole back to the right rotation it was in before.
                decal.transform.rotation = worldRotation;

                // Subtract one bullet from the bullets that are currently loaded in the weapon.
                currentBullets--;
            }
        }
    }

    // Reload the weapon.
    // NOTE: Change all of this to instead return the number of rounds used so when the player reloads it can subtract that many rounds from their inventory.
    public virtual int Reload(int rounds)
    {
        // Check to see if more bullets can be put into the weapon.
        if (currentBullets < maxBullets)
        {
            // How many bullets can be added to the weapon.
            // NOTE: This needs to be fixed. What if the gun has 20/30 rounds and you try to add only 5? It will say you used 10.
            int roundsUsed = maxBullets - currentBullets;
            // The number of rounds from the number of rounds attemtped to be added that weren't added.
            // NOTE: This needs to be fixed. What if you tried to add 5, it said you used 10 (above), now it will say you have -5 left over.
            int roundsLeftOver = rounds - roundsUsed;

            // If the bullets added exceeds the maximum capacity, set the current loaded amount to the max.
            if (currentBullets > maxBullets)
            {
                // Set current rounds loaded to the max.
                currentBullets = maxBullets;
            }
            // Return how many rounds didn't get used.
            return roundsLeftOver;
        }
        // This part is called if the weapon is already loaded with the maximum number of rounds.
        else
        {
            // Return the number of rounds attempted to be put in the weapon.
            return rounds;
        }
    }
}