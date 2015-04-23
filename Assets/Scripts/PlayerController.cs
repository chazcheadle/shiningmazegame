using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	void Update()
	{
		if (SystemInfo.deviceType == DeviceType.Desktop) 
		{
			// Exit condition for Desktop devices
			if (Input.GetKey("escape"))
				Application.Quit();
		}
		else
		{
			// Exit condition for mobile devices
			if (Input.GetKeyDown(KeyCode.Escape))
				Application.Quit();            
		}
	}

	void FixedUpdate ()
	{
		if (SystemInfo.deviceType == DeviceType.Desktop) { 
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			rb.AddForce (movement * speed);
		} else {
			// Player movement in mobile devices
			
			// Building of force vector 
			Vector3 movement = new Vector3 (Input.acceleration.x, 0.0f, Input.acceleration.y);
			// Adding force to rigidbody
			rb.AddForce (movement * speed);
		}
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 12)
		{
			winText.text = "You Win!";
		}
	}
}