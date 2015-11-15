using UnityEngine;
using System.Collections;
using XboxCtrlrInput;


public class FireProjectile : MonoBehaviour {

	// GameObjects
	public GameObject projectilePrefab;
	public GameObject spawnPt; // holds the spawn point object
	public float projectilesPerSecond;
	public float projectileSpeed;

	// 
	private int playerNumber;
	private bool allowFire;

	// Use this for initialization
	void Start () {
		playerNumber = gameObject.GetComponentInParent<PlayerController> ().playerNumber;
		InvokeRepeating("Fire", 0.0f, 1.0f / projectilesPerSecond);
		allowFire = false;
	}
	
	// Update is called once per frame
	void Update () {
		allowFire = false;
		if (XCI.GetAxis(XboxAxis.RightTrigger, playerNumber) > 0) {
			allowFire = true;
		}
	}

	void Fire(){
		if (!allowFire)
			return;

		// Instantiate and rotate the projectile.
		GameObject projectile = Instantiate(projectilePrefab, spawnPt.transform.position, Quaternion.identity) as GameObject;
		Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.forward);
		projectile.transform.rotation = q * projectile.transform.rotation; 

		// accelerate it
		Rigidbody projectileRB = projectile.GetComponent<Rigidbody> ();
		projectileRB.velocity = transform.forward * projectileSpeed;
	}
}
