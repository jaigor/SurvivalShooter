using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	// movement player speed
	public float speed = 6f;

	// components to store references
	Vector3 movement; // vector (X,Y(not used in this case),Z)
	Animator anim;
	Rigidbody playerRigidbody;

	// limitation with the floor, layer mask
	int floorMask;
	// and distance with the main camera
	float camRayLenght = 100f;

	// asigns reference into the components (Start function)
	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}


	void FixedUpdate()
	{
		//input axes
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// make the player move around
		Move (h, v);

		// turns the player to face mouse cursor
		Turning ();

		// animates player
		Animating (h, v);
	}

	void Move(float h, float v)
	{
		// sets the movement vector with passed parameters 
		movement.Set(h, 0f,  v);

		// normalized the movement and speed for all moves
		movement = movement.normalized * speed * Time.deltaTime;

		// move the player to the actual position
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning ()
	{
		// instance a ray from mouse cursor to camera direction
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		// variable to store information about what was hit by the ray
		RaycastHit floorHit;

		// Perform raycast and if it hits something on the floor layer...
		if(Physics.Raycast(camRay, out floorHit, camRayLenght, floorMask))
		{
			// create a vector from player to point on the floor
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			//  create a quaternion (rotation) based on looking  down the vector from player to the mouse
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

			// set player's rotation to this new one
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	void Animating (float h, float v)
	{
		// creates variable if any of the axes is non-zero
		bool walking = h != 0f || v != 0f;

		// tells the animator if the player is walking 
		anim.SetBool ("IsWalking", walking);
	}
}
