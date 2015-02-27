using UnityEngine;
using System.Collections;

public class PusherController : MonoBehaviour {

	float speed = 2.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		float y = Input.GetAxis ("Jump") * Time.deltaTime * speed;
	    float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
		transform.Translate(x, y, z);
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
						transform.localRotation = SixenseInput.Controllers[0].Rotation;
						Debug.Log (position);
						transform.localPosition = position;
					}
				}
			}
		}
	}
}
