using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {

	private Transform Player;
	private Transform Goal;
	private Transform myTransform;
	private float moveSpeed;
	public float maxDistance = 10;
	public float minDistance = 1;
	public float rotationSpeed = 2;
	public float baseSpeed = 5;


	void Awake()
	{
		moveSpeed = Random.Range(baseSpeed,(baseSpeed+0.5f));
		myTransform = transform; //cache transform data for easy access/preformance
	}

	void Start()	{
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
		Goal = GameObject.FindGameObjectWithTag ("Goal").transform;
	}

	void Update()	{
	
	if (Vector3.Distance (transform.position, Player.position) >= maxDistance) {
			transform.LookAt (Goal);
			FollowGoal();
		} else {

			transform.LookAt (Player);
			FollowPlayer();
		}

	}
	void FollowPlayer()	{

		if (Vector3.Distance (transform.position, Player.position) >= minDistance) {
			
			//move towards the Player

			myTransform.rotation = Quaternion.Slerp (myTransform.rotation,
			Quaternion.LookRotation (Player.position - myTransform.position), rotationSpeed * Time.deltaTime);
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

		} else {
			Destroy (gameObject);
		}
	}
	// 2 functies omdat in de toekomst mogelijk ander gedrag moet worden toegevoegd
	void FollowGoal()	{
		if (Vector3.Distance (transform.position, Goal.position) >= minDistance)
		{
			
			//move towards the Goal
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
			Quaternion.LookRotation(Goal.position - myTransform.position), rotationSpeed*Time.deltaTime);
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
			
		} else {
			Destroy(gameObject);
		}
	}

}