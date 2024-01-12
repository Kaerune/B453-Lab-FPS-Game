using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Fields
    [Header("Movement")]
	[Tooltip("The movement speed of the player in meters per second.")]
	[SerializeField] float speed; // Try a value of 6 to start.
	[Tooltip("The look sensitivity of the mouse in degrees per second.")]
    [SerializeField] float sensitivity; // Try a value of 2 to start.
	[Tooltip("The sprinting movement speed of the player in meters per second.")]
    [SerializeField] float sprintSpeed; // Try a value of 10 to start.
	
	private float moveFB; // Used to track forward or backward movement from -1 to 1.
	private float moveLR; // Used to track left or right movement from -1 to 1.
	private float rotX; // Rotation on the X axis for the mouse movement.
	private float rotY; // Rotation on the Y axis for the mouse movement.

	// References
	private CharacterController cc; // Reference to the CharacterController component on the gameobject this script is attached to.
	private Camera _camera; // Reference to a camera.
	
	private void Start()
	{
		// Lock the cursor to the game window (by default the cursor will also not be visible).
		Cursor.lockState = CursorLockMode.Locked;
		// Assign this object's CharacterController to the cc variable.
		cc = gameObject.GetComponent<CharacterController>();
		// Grab the first child of the gameobject with this script on it (which is the camera in this case), access its Camera component, and assign it to the _camera variable.
		_camera = gameObject.transform.GetChild(0).transform.gameObject.GetComponent<Camera>();
	}
	
	private void Update()
	{
		// Here we call Move() every single frame which checks for movement and applies it if necessary.
		// NOTE: We could also check for input in Update() and call the method depending on input, or we can call the method every frame in Update() and have the input checking in the method like we do here. There is no performance difference.
		Move();
	}
	
	// Method to check for input and move the character and camera accordingly.
	private void Move(){
		// Local variable set to the movement speed.
		float movementSpeed = speed;
		
		// Checking to see if the player is holding down the sprint key.
		// NOTE: GetKey returns true for every single frame that the button is held down. This differs from GetKeyDown and GetKeyUp which returns true only on the frame the button is pressed or released respectively.
		if (Input.GetKey(KeyCode.LeftShift))
		{
			// Set the movementSpeed local variable to the sprintSpeed.
		    movementSpeed = sprintSpeed;
		}
		
		// Checking to see if the player is no longer holding down the sprint key.
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			// When the player releases the sprint key, the movementSpeed is reset to the normal speed.
			movementSpeed = speed;
		}
		
		// Grabbing movement axis and mouse movement and multiplying them by the movementSpeed or sensitivity (for the mouse).
		moveFB = Input.GetAxis("Vertical") * movementSpeed;
		moveLR = Input.GetAxis("Horizontal") * movementSpeed;
		rotX = Input.GetAxis("Mouse X") * sensitivity;
		rotY -= Input.GetAxis("Mouse Y") * sensitivity;
		
		// Clamp the Y rotation. The Mathf.Clamp() method takes three arguments here. The value to clamp, the minimum allowed value, and the maximum allowed value.
		// So here, no matter how far the player moves the mouse up or down, it won't let it go lower than -60 degrees or higher than +60 degrees.
		rotY = Mathf.Clamp(rotY, -60f, 60f);
		
		// Creating the movement vector with a 0 Y value.
		Vector3 movement = new Vector3(moveLR, 0, moveFB);
		
		// Rotating the player's body on the Y axis only.
		transform.Rotate(0, rotX, 0);
		// Rotating the camera on the X axis only.
		// Unity uses Quaternions for rotation, using the Euler() method allows you to convert the quaternion values into degrees.
		_camera.transform.localRotation = Quaternion.Euler(rotY, 0, 0);
		
		// Move the player using the Move() method on the CharacterController component.
		// Notice that we are also multiplying the final movement vector by Time.deltaTime so that the movement speed is independent of the framerate.
		movement = transform.rotation * movement;
		cc.Move(movement * Time.deltaTime);
	}
}