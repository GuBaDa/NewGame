using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform player1;
	public Transform player2;

	public float minZoom;
	public float maxZoom;
	public float zoomFactor;
	public float smooth;
	public float maxDistance;

	public GameObject cam2;
	

	private Vector3 cameraPos;
	private Vector2 cameraMid;
	private Camera cam1;


	private bool shearCams;
	private float distance;

	private Plane[] planes;

	private Vector3 focus1;
	private Vector3 focus2;

	private float aspectRatio;

	private Transform mask1;
	private Transform mask2;
	private float angleMask;

	private Vector3 wanderTarget1; 
	private Vector3 wanderTarget2; 


	// Use this for initialization
	void Start () {
		cam1 = gameObject.transform.GetChild(0).GetComponent<Camera>();
		//set cam2
		cam2.transform.position = cam1.transform.position;
		cam2.transform.GetChild (0).GetComponent<Camera>().orthographicSize = maxZoom;
		mask1  =  gameObject.transform.GetChild(0).GetChild(0);
		if (mask1 == null){
			Debug.Log("NULL");
		}
		mask2  =  cam2.transform.GetChild(0).GetChild(0);
		//focus = player1.position;
		aspectRatio = (float)cam1.pixelHeight/(float)cam1.pixelWidth;

		//get camera mid screen position
		cameraMid = new Vector2 (cam1.pixelWidth/2, cam1.pixelHeight/2);

	}


	
	// Update is called once per frame
	void Update () {
		//get distance between playes
		distance = Vector3.Distance (player1.position, player2.position);

		//check if camera needs sheering
		if (distance > maxDistance){
			//shearCams = true;
		}
		else {
				//shearCams = false;
			}

		//calculate middle position between players;


		//zooming
			//zoom camera according to distance between players
		cam1.orthographicSize = distance*zoomFactor;
			if (cam1.orthographicSize < minZoom){
				cam1.orthographicSize = minZoom;
			}
			else if (cam1.orthographicSize > maxZoom){
				cam1.orthographicSize =  maxZoom;
			}
		cam2.GetComponentInChildren<Camera>().orthographicSize = cam1.orthographicSize;


		checkDistanceScreen();
		shearCameras();
		//calculateCameraFocus();
		gameObject.transform.position = Vector3.Lerp(transform.position, focus1, smooth * Time.deltaTime);
		cam2.transform.position = Vector3.Lerp(cam2.transform.position, focus2, smooth * Time.deltaTime);
		Debug.Log(focus1 + " : " + focus2);
		wandering();

	}

	void checkDistanceScreen(){
		if (cam1.orthographicSize == maxZoom){
			Vector2 player1ScreenPos = cam1.WorldToScreenPoint(player1.position);
			Vector2 player2ScreenPos = cam1.WorldToScreenPoint(player2.position);
			
			Vector2 playersVector = player1ScreenPos-player2ScreenPos;
			playersVector.Normalize();

			Vector2 _playersVector = new Vector2(playersVector.x, playersVector.y * aspectRatio);
			
			_playersVector *= cam1.pixelWidth/4f;

			if ((player1ScreenPos - cameraMid).magnitude > _playersVector.magnitude ||
			    (player2ScreenPos - cameraMid).magnitude > _playersVector.magnitude){ 

				//if player distance gets too big from center screen
				shearCams = true;
			}
		}
		else {
			shearCams = false;
		}



	}

	void shearCameras(){
		if (shearCams){
			//get delta distance of worldspace on screenport plane in world space (ja je verwacht het niet!!)
			Vector2 pl1ScreenPos = cam1.WorldToScreenPoint(player1.position);
			Vector3 pl1ScreenWorldPos = cam1.ScreenToWorldPoint(pl1ScreenPos);

			Vector2 pl2ScreenPos = cam2.GetComponentInChildren<Camera>().WorldToScreenPoint(player2.position);
			Vector3 pl2ScreenWorldPos = cam2.GetComponentInChildren<Camera>().ScreenToWorldPoint(pl2ScreenPos);

			Vector3 delta1 = pl1ScreenWorldPos - calculateCameraFocus();
			Vector3 delta2 = pl2ScreenWorldPos - calculateCameraFocus();
			// change focus accordingly to delta difference
			focus1 = player1.position + delta1;
			focus2 = player2.position - delta1;

			// mask and second camera visible and rotate mask
			mask1.gameObject.SetActive(true);
			mask2.gameObject.SetActive(true);
			cam2.transform.GetChild(0).GetComponent<Camera>().enabled = true;
			mask1.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 360 - angleMask - 90);
			mask2.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 360 - angleMask + 90);

		}
		else {
			float x = (player1.position.x+player2.position.x)/2;
			float y = (player1.position.y+player2.position.y)/2;
			float z = (player1.position.z+player2.position.z)/2;
			focus1 = new Vector3(x,y,z);
			focus2 = focus1;
		//}

			//mask and second camera invisible
		Vector2 cam1pos = new Vector2(transform.position.x, transform.position.z);
		Vector2 cam2pos = new Vector2(cam2.transform.position.x, cam2.transform.position.z);

		//if (Vector2.Distance(cam1pos, cam2pos) < 1){
			mask1.gameObject.SetActive(false);
			mask2.gameObject.SetActive(false);
			cam2.transform.GetChild(0).GetComponent<Camera>().enabled = false;
		//}
		}

	}

	Vector3 calculateCameraFocus(){

		Vector2 player1ScreenPos = cam1.WorldToScreenPoint(player1.position);
		Vector2 player2ScreenPos = cam1.WorldToScreenPoint(player2.position);

		Vector2 playersVector = player1ScreenPos-player2ScreenPos;
		playersVector.Normalize();

		angleMask = Mathf.Atan2(playersVector.x, playersVector.y);
		angleMask *= Mathf.Rad2Deg;
		Debug.Log (angleMask);
		Vector2 _playersVector = new Vector2(playersVector.x, playersVector.y * aspectRatio);

		_playersVector *= cam1.pixelWidth/2f;

		Vector2 screenEndVector = cameraMid + _playersVector;

		//Debug.Log (_playersVector.magnitude);


		//////
		//draw player vectors from cameraMid with maximum magnitude
		Vector3 cameraMidWorld = cam1.ScreenToWorldPoint(cameraMid);
		Vector3 cameraFocusPosWorld = cam1.ScreenToWorldPoint(screenEndVector);

		Debug.DrawLine(cameraMidWorld, cameraFocusPosWorld, Color.green);


		return cameraFocusPosWorld;

	}

	void wandering(){
		player2.position = Vector3.MoveTowards(player2.position, wanderTarget1, 6 * Time.deltaTime);
		player1.position = Vector3.MoveTowards(player1.position, wanderTarget2, 4 * Time.deltaTime);
		if (Vector3.Distance (player2.position, wanderTarget1) < 1){
			float x = player1.position.x + Random.Range(-20f,20f);
			float z = player1.position.z + Random.Range(-20f,20f);
			wanderTarget1 = new Vector3 (x, 0f ,z);
		}
		if (Vector3.Distance (player1.position, wanderTarget2) < 1){
			float x = player2.position.x + Random.Range(-10f,10f);
			float z = player2.position.z + Random.Range(-10f,10f);
			wanderTarget2 = new Vector3 (x, 0f ,z);
		}
	}
}
