using UnityEngine;


using System.Collections;

public class PullerController : MonoBehaviour {
	float speed = 2.0f;
	bool stick = false;
	Collider connected_to;
	SpringJoint joint;
	float startTime = 0.0f;
	Quaternion collider_q;
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
		if (Input.GetKeyDown ("1")) {
			if(stick){
				stick = false;
				connected_to.transform.parent = null;
				gameObject.renderer.material.color = Color.green;
				Destroy(joint);
				collider.rigidbody.isKinematic = false;
				collider.rigidbody.freezeRotation = false;
				Destroy(gameObject.GetComponent<Rigidbody>());
				startTime = 0.0f;
			}
		}
		for ( uint i = 0; i < 2; i++ )
		{
			if ( SixenseInput.Controllers[i] != null )
			{
				if ( SixenseInput.Controllers[i].Enabled )
				{
					if(i == 0){
						Vector3 position = SixenseInput.Controllers[0].Position;
						position.y = position.y * 0.1f;
						position.x = position.x * 0.1f - 5.0f;
						position.z = position.z * 0.2f + 60.0f;
						Debug.Log (position);
						transform.localPosition = position;
						transform.localRotation = SixenseInput.Controllers[0].Rotation;
						if(SixenseInput.Controllers[i].GetButtonDown(SixenseButtons.ONE)){
							if(stick){
								stick = false;
								connected_to.transform.parent = null;
								gameObject.renderer.material.color = Color.green;
								Destroy(joint);
								collider.rigidbody.freezeRotation = false;

								collider.rigidbody.isKinematic = false;
								Destroy(gameObject.GetComponent<Rigidbody>());
								startTime = 0.0f;
								collider.transform.rotation = collider_q;
							}
						}
					}
				}
			}
		}
		startTime += Time.deltaTime;
	}
	void OnTriggerEnter(Collider other) {
		//Wait for one second
		if (!stick && startTime >= 1.0f) {
			//Stick two together
			stick = true;
			other.transform.parent = gameObject.transform;
			connected_to = other;
			collider_q = collider.transform.rotation;
			joint = gameObject.AddComponent<SpringJoint>();
			joint.connectedBody = other.rigidbody;
			collider.rigidbody.isKinematic = true;
			collider.rigidbody.freezeRotation = true;

			gameObject.renderer.material.color = Color.red;

		}
	}
}
