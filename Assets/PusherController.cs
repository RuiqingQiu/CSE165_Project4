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
					if(i == 1)
						transform.localPosition = SixenseInput.Controllers[1].Position;
				}
			}
		}
	}
}
