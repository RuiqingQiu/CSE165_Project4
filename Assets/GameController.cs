using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject jenga;
	public GameObject camera;
	public float rotate_speed = 20.0f;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		//camera.transform.Rotate(jenga.transform.up * 5.0f * Time.deltaTime);
		camera.transform.RotateAround (Vector3.zero, jenga.transform.up, rotate_speed * Time.deltaTime);

	}
}
