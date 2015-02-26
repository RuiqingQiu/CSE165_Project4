using UnityEngine;
using System.Collections;

public class PullerController : MonoBehaviour {
	float speed = 2.0f;
	bool stick = false;
	Collider connected_to;
	// Use this for initialization
	void Start () {
		gameObject.renderer.material.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		float y = Input.GetAxis ("Jump") * Time.deltaTime * speed;
		float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
		transform.Translate(x, y, z);
		//Change to a button on controller
		if (Input.GetKeyDown ("space")) {
			if(stick){
				stick = false;
				connected_to.transform.parent = null;
				gameObject.renderer.material.color = Color.green;

			}
		}
	}
	void OnTriggerEnter(Collider other) {
		if (!stick) {
			//Stick two together
			stick = true;
			other.transform.parent = gameObject.transform;
			connected_to = other;
			gameObject.renderer.material.color = Color.red;

		}
	}
}
