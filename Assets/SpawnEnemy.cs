using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	public GameObject spawnPoint;
	public GameObject enemyPrefab;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", 1, 1f); 
	}
	
	// Update is called once per frame
	void Update () {
	}


	void Spawn(){
		Rigidbody Enemy;
		Enemy = Instantiate(enemyPrefab, spawnPoint.transform.position, transform.rotation) as Rigidbody;
	}
}