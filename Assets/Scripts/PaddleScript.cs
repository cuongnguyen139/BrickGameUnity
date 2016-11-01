using UnityEngine;
using System.Collections;

public class PaddleScript : MonoBehaviour {

	public float paddleSpeed = 100f;
	public GameObject ballPrefab;
	GameObject attachedBall = null;
	
	int lives = 3;
	GUIText guiLives;
	
	int score = 0;
	
	public GUISkin scoreboardSkin;
	
	
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(GameObject.Find("guiLives"));
		
		guiLives = GameObject.Find("guiLives").GetComponent<GUIText>();
		guiLives.text = "Lives: " + lives;
		
		SpawnBall();
	}
	
	public void OnLevelWasLoaded(int level) {
		SpawnBall();
	}
	
	public void AddPoint(int v) {
		score += v;
	}
	
	public void LoseLife() {
		lives--;
		guiLives.text = "Lives: " + lives;
		if ( lives > 0 )
			SpawnBall();
		else {
			Destroy(gameObject);
			Application.LoadLevel("gameOver");
		}
	}
	
	public void SpawnBall() {
		// Spawn/Instantiate new ball
		if( ballPrefab == null ) {
			return;
		}
		
		attachedBall = (GameObject)Instantiate( ballPrefab, transform.position + new Vector3(0, .75f, 0), Quaternion.identity );
	}
	
	void OnGUI() {
		GUI.skin = scoreboardSkin;
		GUI.Label( new Rect(0,10,300,100), "Score: " + score);
	}
	
	// Update is called once per frame
	void Update () {
		// Left-Right
		transform.Translate( paddleSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0, 0 );
		
		if (transform.position.x > 42f) transform.position = new Vector3( 42f, transform.position.y, transform.position.z );
		if (transform.position.x < -42f) transform.position = new Vector3( -42f, transform.position.y, transform.position.z );
		
		if(attachedBall) {
			Rigidbody ballRigidbody = attachedBall.rigidbody;
			ballRigidbody.position = transform.position + new Vector3(0, 2.75f, 0);
			
			if (Input.GetButtonDown("Jump")) { // Fire the ball
				ballRigidbody.isKinematic = false;
				ballRigidbody.AddForce(5000f * Input.GetAxis("Horizontal"), 5000f, 0);
				attachedBall = null;
			}
		}
	}

	void OnCollisionEnter( Collision col ) {
		foreach (ContactPoint contact in col.contacts) {
			if( contact.thisCollider == collider ) {
				// This is the paddle's contact point
				float english = contact.point.x - transform.position.x;
				contact.otherCollider.rigidbody.AddForce(1000f * english, 0, 0);

				break;
			}
		}
	}
}
