using UnityEngine;
using System.Collections;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public GameObject jenga;
	public GameObject camera;
	public GameObject left_hand;
	public float rotate_speed = 20.0f;
	public GameObject block1_prefab;
	public GameObject block2_prefab;
	public List<GameObject> block_list = new List<GameObject>();

	public static bool show_loose = false;
	private float block1x = 0.0f;
	private float block1y = 0.9f;
	private float block1z = -3.0f;
	private float block2z = -2.75f;

	private GameObject hitObject;

	public GameObject hands;
	public GameObject pusher;
	public GameObject puller;
	//Keep track which tool to use
	private int state;

	// Use this for initialization
	void Start () {
		buildJenga ();


	}
	public void buildJenga(){
		block1x = 0.0f;
		block1y = 0.9f;
		block1z = -3.0f;
		block2z = -2.75f;
		float scale_offset = 0.05f;
		//Init a jenga block
		//Loop through the level
		for (int i = 0; i < 54/3; i++) {
			//block1z = -2.75f;

			//Loop through the blocks on that level
			if(i%2 == 0){
				block1x = -2.75f;
				for(int j = 0; j < 3; j++){
					Vector3 location = new Vector3(block1x, block1y, block1z);
					Debug.Log (location);
					GameObject block = (GameObject) Instantiate(block1_prefab, location, Quaternion.Euler(0, 90, 0));
					block_list.Add (block);
					block.transform.localScale += new Vector3(Random.Range(scale_offset,-scale_offset), Random.Range(scale_offset,-scale_offset), Random.Range(scale_offset,-scale_offset)); 
					block1x += 2.75f;
				}
			}
			else{
				block2z = -5.5f;
				for(int j = 0; j < 3; j++){
					Vector3 location = new Vector3(0.0f, block1y, block2z);
					Quaternion rotation = new Quaternion(0,0,0,0);
					GameObject block = (GameObject) Instantiate(block2_prefab, location, Quaternion.identity);
					block_list.Add (block);
					block.transform.localScale += new Vector3(Random.Range(scale_offset,-scale_offset), Random.Range(scale_offset,-scale_offset), Random.Range(scale_offset,-scale_offset)); 
					block2z += 2.75f;
				}
			}
			block1y += 1.8f;
		}
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("return")){
			state = (state + 1) % 3;
			switch(state){
			case 0:
				hands.SetActive(true);
				pusher.SetActive(false);
				puller.SetActive(false);
				break;
			case 1:
				hands.SetActive(false);
				pusher.SetActive(true);
				puller.SetActive(false);
				break;
			case 2:
				hands.SetActive(false);
				pusher.SetActive(false);
				puller.SetActive(true);
				break;
			}
		}
		for ( uint i = 0; i < 2; i++ )
		{
			if ( SixenseInput.Controllers[i] != null )
			{
				if ( SixenseInput.Controllers[i].Enabled )
				{
					//Debug.Log (SixenseInput.Controllers[i].JoystickX);
					//Debug.Log (camera.transform.forward);
					if(i == 1){
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
							camera.transform.position = camera.transform.position + 4*(camera.transform.forward * -Time.deltaTime);
							//camera.transform.Translate(camera.transform.forward * -Time.deltaTime);
						}
						else if(SixenseInput.Controllers[1].JoystickY == 1.0f)
						{
							camera.transform.position = camera.transform.position + 4*(camera.transform.forward * Time.deltaTime);

							//camera.transform.Translate(camera.transform.forward * Time.deltaTime);
						}
						//Restart the game
						else if(SixenseInput.Controllers[1].GetButtonDown(SixenseButtons.ONE)){
							for(int j = 0; j < block_list.Count; j++){
								Destroy(block_list[j]);
							}
							buildJenga();
						}
						//Button to check which tool to use
						else if(SixenseInput.Controllers[1].GetButtonDown(SixenseButtons.THREE)){
							Debug.Log ("switch tool");
							state = (state + 1) % 3;
							switch(state){
								case 0:
									hands.SetActive(true);
									pusher.SetActive(false);
									puller.SetActive(false);
									break;
								case 1:
									hands.SetActive(false);
									pusher.SetActive(true);
									puller.SetActive(false);
									break;
								case 2:
									hands.SetActive(false);
									pusher.SetActive(false);
									puller.SetActive(true);
									break;
							}
						}
						else if(SixenseInput.Controllers[1].GetButtonDown(SixenseButtons.TWO)){
							show_loose = !show_loose;
						}
					}
					else if(i == 0){
						if(SixenseInput.Controllers[0].JoystickY == 1.0f){
							camera.transform.position = camera.transform.position + 4 * new Vector3(0,1,0) * Time.deltaTime;

						}
						else if(SixenseInput.Controllers[0].JoystickY == -1.0f){
							camera.transform.position = camera.transform.position + -4 * new Vector3(0,1,0) * Time.deltaTime;
							
						}
					}
				}
			}
		}


		if(Input.GetMouseButtonDown(0))  
		{
				
				RaycastHit hit;
				
				// Cast a ray
				
				if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
				{
					hitObject = hit.collider.gameObject;
					hitObject.transform.parent = gameObject.transform;
				}
		}
		
//		if(Input.GetButtonDown("Release")) // This will release the object 
//		{
//			hitObject.transform.parent = null;
//			hitObject = null;
//		}

		//camera.transform.Rotate(jenga.transform.up * 5.0f * Time.deltaTime);

	}
}
