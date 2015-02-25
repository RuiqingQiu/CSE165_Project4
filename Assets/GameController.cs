using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject jenga;
	public GameObject camera;
	public GameObject left_hand;
	public float rotate_speed = 20.0f;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		for ( uint i = 0; i < 2; i++ )
		{
			if ( SixenseInput.Controllers[i] != null )
			{
				if ( SixenseInput.Controllers[i].Enabled )
				{
					Debug.Log (SixenseInput.Controllers[i].JoystickX);
					Debug.Log (camera.transform.forward);
					if(SixenseInput.Controllers[1].JoystickX == -1.0f)
					{
						camera.transform.RotateAround (Vector3.zero, jenga.transform.up, rotate_speed * Time.deltaTime);
					}
					else if(SixenseInput.Controllers[1].JoystickX == 1.0f)
					{
						camera.transform.RotateAround (Vector3.zero, jenga.transform.up, -rotate_speed * Time.deltaTime);

					}
					else if(SixenseInput.Controllers[1].JoystickY == -1.0f)
					{
						camera.transform.position = camera.transform.position + (camera.transform.forward * -Time.deltaTime);
						//camera.transform.Translate(camera.transform.forward * -Time.deltaTime);
					}
					else if(SixenseInput.Controllers[1].JoystickY == 1.0f)
					{
						camera.transform.position = camera.transform.position + (camera.transform.forward * Time.deltaTime);

						//camera.transform.Translate(camera.transform.forward * Time.deltaTime);

					}
				}
			}
		}
		//camera.transform.Rotate(jenga.transform.up * 5.0f * Time.deltaTime);

	}
}
