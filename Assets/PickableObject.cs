using UnityEngine;
using System.Collections;
using System;

public class PickableObject : MonoBehaviour {
	float red;
	// Use this for initialization
	void Start () {
		red = gameObject.renderer.material.color.r;
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnCollisionEnter(Collision collision)
	{

		if (GameController.show_loose) {
			Vector3 velocity = collision.relativeVelocity;
			Vector3 impactForce = collision.impactForceSum;
//			Debug.Log (impactForce);
			float total = 0.0f;
			total = Math.Abs (impactForce.x) + Math.Abs (impactForce.y) + Math.Abs (impactForce.z);
			impactForce.Normalize ();
			Color c = gameObject.renderer.material.color;
			c.r = red + total;
			Debug.Log(c.a);
			gameObject.renderer.material.color = c;
			//gameObject.renderer.material.color = new Color (impactForce.x, impactForce.y, impactForce.z, 1.0f);
		}
	}


}
