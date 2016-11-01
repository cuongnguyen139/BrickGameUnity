using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
	PaddleScript paddleObject;
	float oldspeed;
	void Start () {
		paddleObject = (PaddleScript) GameObject.Find("PlayerPaddle").GetComponent<PaddleScript>();
	}

	void Update () {
		float speed = rigidbody.velocity.magnitude;
		if (speed > 100f) rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, 100f);

		if (oldspeed == speed) return;
		oldspeed = speed;
		Debug.Log(speed);
	}

	public void Die() {
		Destroy(gameObject);
		paddleObject.LoseLife();
	}
}
