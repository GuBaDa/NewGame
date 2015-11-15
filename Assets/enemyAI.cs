using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {

	Transform Leader;
	float speed = 5;
	float maxDistance = 10;
	float minDistance = 4;

	void Start()	{
		Leader = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update()	{
		transform.LookAt (Leader);
		Follow();
	}
	void Follow()	{
		if (Vector3.Distance (transform.position, Leader.position) >= minDistance);
		{
			transform.position += transform.forward*speed*Time.deltaTime;
		}
	}

}