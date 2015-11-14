using UnityEngine;
using System.Collections;

public class SplitScreen : MonoBehaviour {

	public GameObject pl1;
	public GameObject pl2;

	public GameObject mask1;
	public GameObject mask2;

	Camera cam;


	Vector3 anglePlayers;
	LineRenderer line;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 posPl1 = pl1.transform.position;
		Vector3 posPl2 = pl2.transform.position;

		Vector3 posScreenP1 = GetComponent<Camera>().WorldToScreenPoint(posPl1);
		Vector3 posScreenP2 = GetComponent<Camera>().WorldToScreenPoint(posPl2);
		//Vector3 posPl1 =  cam.WorldToScreenPoint(pl1.transform.position); 
		//Vector3 posPl2 =  cam.WorldToScreenPoint(pl2.transform.position); 

		anglePlayers = new Vector3 (posScreenP1.x - posScreenP2.x, posScreenP1.y - posScreenP2.y, 0);
		float ang = getAngle(posScreenP1, posScreenP2);
		Debug.Log (ang);
		Debug.DrawLine (posPl1, posPl2);

		mask1.transform.localRotation = Quaternion.Euler(0, 0, ang);
		mask2.transform.localRotation = Quaternion.Euler(0, 0, ang);


	}

	float getAngle(Vector2 fromVector2, Vector2 toVector2){
		
		float ang = Vector2.Angle(fromVector2, toVector2);
		Vector3 cross = Vector3.Cross(fromVector2, toVector2);
		
		if (cross.z > 0){
			ang = 360 - ang;
		}
		return ang;
	}
}
