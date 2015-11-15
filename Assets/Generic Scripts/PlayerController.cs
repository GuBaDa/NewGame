using UnityEngine;
using System.Collections;
using XboxCtrlrInput;


[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {
	
	// Handling
	public int playerNumber = 0;
	public float rotationSpeed;
	public float walkSpeed;
	public float runSpeed;

	// Hidden public
	[HideInInspector]
	public Quaternion viewRotation;
	public Vector3 direction;
	
	// System
	private Vector3 motion; 
	
	// Components
	private CharacterController controller;
	
	void Start () {
		playerNumber = Mathf.Clamp(playerNumber, 1, 4);
		controller = GetComponent<CharacterController>();
		motion = Vector3.zero;
	}
	
	void Update () {

		// Get coordinates from left joystick.
		float axisX_L = XCI.GetAxis(XboxAxis.LeftStickX, playerNumber);
		float axisY_L = XCI.GetAxis(XboxAxis.LeftStickY, playerNumber);
		Vector3 leftInput = new Vector3(axisX_L,0,axisY_L);

		// Get coordinates from right joystick.
		float axisX_R = XCI.GetAxis(XboxAxis.RightStickX, playerNumber);
		float axisY_R = XCI.GetAxis(XboxAxis.RightStickY, playerNumber);
		Vector3 rightInput = new Vector3(axisX_R,0,axisY_R);

		// If the right joystick is in use, the character will strafe.
		bool strafe = (rightInput != Vector3.zero);

		LeftStick (leftInput, strafe);
		RightStick (rightInput);

	}

	void LeftStick(Vector3 input, bool strafe) {

		// Only change direction and move if the joystick is not at its neutral position.
		if (input != Vector3.zero) {

			// Only change viewing direction if you're not strafing.
			if( !strafe){
				viewRotation = Quaternion.LookRotation (input, Vector3.up);
				controller.transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,viewRotation.eulerAngles.y,rotationSpeed * Time.deltaTime);
			}

			// Move in the direction you're looking, with higher speed if the Run button is held down.
			motion = input;
			motion *= (XCI.GetButton(XboxButton.RightBumper,playerNumber))?runSpeed:walkSpeed;

			// If strafing, move a bit slower.
			if( strafe) {
				motion = motion/2;
			}
			controller.Move(motion * Time.deltaTime);

			direction = input;
		}
	}

	void RightStick(Vector3 input) {
		
		// Only change direction if the joystick is not at its neutral position.
		if (input != Vector3.zero) {
			viewRotation = Quaternion.LookRotation (input, Vector3.up);
			controller.transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,viewRotation.eulerAngles.y,rotationSpeed * Time.deltaTime);
			direction = input;
		}
	}
}