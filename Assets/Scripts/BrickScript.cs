using UnityEngine;
using System.Collections;

public class BrickScript : MonoBehaviour {
	public int pointValue = 1;
	public int hitPoints = 1;
	public int powerUpChance = 3;
	public int level = 1;
	public GameObject[] powerUpPrefabs;

	static int numBricks = 0;
	static PaddleScript paddleScript;
	// Use this for initialization
	void Start () {
		numBricks++;
		if (paddleScript == null) paddleScript = GameObject.Find("PlayerPaddle").GetComponent<PaddleScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision col) {
		hitPoints--;

		if (hitPoints <= 0) {
			Die();
		}
	}
	
	void Die() {
		Destroy(gameObject);
		paddleScript.AddPoint(pointValue);
		numBricks--;
		
		if (numBricks <= 0) {
			// Load a new level??
			++level;
			Application.LoadLevel("level" + level.ToString());
		}
	}
}
