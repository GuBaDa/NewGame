using UnityEngine;
using System.Collections;
using XboxCtrlrInput;


public class FireProjectile : MonoBehaviour {

	// GameObjects
	public GameObject projectilePrefab;
	//private GameObject player;

	// 
	private int playerNumber;


	// Use this for initialization
	void Start () {
		//player = GetComponent<Transform> ();
		playerNumber = gameObject.GetComponentInParent<PlayerController> ().playerNumber;
	}
	
	// Update is called once per frame
	void Update () {
		Fire ();
	}

	void Fire(){
		if (XCI.GetAxis(XboxAxis.RightTrigger, playerNumber) > 0) {
			Rigidbody projectile;
			Vector3 firepoint = new Vector3(transform.position.x+1.5f ,transform.position.y,transform.position.z);
			projectile = Instantiate(projectilePrefab, firepoint, transform.rotation) as Rigidbody;
			projectile.velocity = transform.TransformDirection(Vector3.left * 100);
		}
	}
}
