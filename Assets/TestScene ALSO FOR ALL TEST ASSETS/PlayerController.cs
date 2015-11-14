using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {
	
	// Handling
	public float rotationSpeed;
	public float walkSpeed;
	public float runSpeed;
	
	// System
	private Quaternion viewRotation;
	private Quaternion targetRotation;
	private Vector3 motion; 
	
	// Components
	private CharacterController controller;
	
	void Start () {
		controller = GetComponent<CharacterController>();
		motion = Vector3.zero;
	}
	
	void Update () {

		LeftStick ();
		RightStick ();

	}

	void LeftStick() {
	
		// Get coordinates from joystick.
		float cordX = Input.GetAxis("Horizontal");
		float cordZ = Input.GetAxis("Vertical");
		Vector3 input = new Vector3(cordX,0,cordZ);

		// Only change direction if the joystick is not at its neutral position.
		if (input != Vector3.zero) {
			viewRotation = Quaternion.LookRotation (input, Vector3.up);
			controller.transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,viewRotation.eulerAngles.y,rotationSpeed * Time.deltaTime);
			motion = input;
			motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;
			controller.Move(motion* Time.deltaTime);
		}
	}

	void RightStick() {
		
		// Get coordinates from joystick.
		float cordX = Input.GetAxis("Horizontal2");
		float cordZ = Input.GetAxis("Vertical2");
		Vector3 input = new Vector3(cordX,0,cordZ);
		
		// Only change direction if the joystick is not at its neutral position.
		if (input != Vector3.zero) {
			viewRotation = Quaternion.LookRotation (input, Vector3.up);
			controller.transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,viewRotation.eulerAngles.y,rotationSpeed * Time.deltaTime);
			//motion = input;
			//motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;
			//controller.Move(motion* Time.deltaTime);
		}
	}
}