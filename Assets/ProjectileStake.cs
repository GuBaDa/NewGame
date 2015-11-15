using UnityEngine;
using System.Collections;

public class ProjectileStake : MonoBehaviour {

	private GameObject colObject; 

	// Use this for initialization
	void Start () {
		colObject = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision){
		GameObject hitObject = collision.gameObject;
		if (hitObject.tag == "Projectile" ) {
			collision.collider.enabled = false;
			hitObject.transform.SetParent(colObject.transform);
			Destroy(hitObject.GetComponent<Rigidbody>());
		}


		
	}
}
