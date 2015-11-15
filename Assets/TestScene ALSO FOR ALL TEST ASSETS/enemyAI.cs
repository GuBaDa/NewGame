using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {

	private Transform Leader;
	private Transform myTransform;
	public float moveSpeed = 5;
	public float maxDistance = 10;
	public float minDistance = 1;
	public float rotationSpeed = 2;

	void Awake()
	{
		myTransform = transform; //cache transform data for easy access/preformance
	}

	void Start()	{
		Leader = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update()	{
		transform.LookAt (Leader);
		Follow();
	}
	void Follow()	{
		if (Vector3.Distance (transform.position, Leader.position) >= minDistance)
		{
			
			//move towards the player
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
			Quaternion.LookRotation(Leader.position - myTransform.position), rotationSpeed*Time.deltaTime);
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

		} else {
			Destroy(gameObject);
		}
	}

}