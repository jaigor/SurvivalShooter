using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;		// element to reference the camera
	public float smoothing = 5f;	// movement camera speed

	Vector3 offset;					// initial offset from the target

	void Start()
	{
		// initial offset calculation, distance between positions
		offset = transform.position - target.position;
	}

	void FixedUpdate()
	{
		// create a position the camera is aiming for based on the offset from the target
		Vector3 targetCamPos = target.position + offset;

		// Smoothly interpolate between position of the camera and target (passing also the speed)
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
