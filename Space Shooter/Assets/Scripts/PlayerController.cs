using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Rigidbody rb;
	public float speed;
	public float tilt;
	public Boundary boundary;
	public Velocity velocity;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	private void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.AddForce(movement * speed);
		rb.velocity = velocity.UpdateVelocity(rb.velocity);
		rb.position = new Vector3
			(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
			);
		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

[System.Serializable]
public class Velocity
{
	public float xMin, xMax, zMin, zMax;

	public Vector3 UpdateVelocity(Vector3 rbVelocity)
	{
		Vector3 ret = new Vector3
			(
			Mathf.Clamp(rbVelocity.x, xMin, xMax),
			0.0f,
			Mathf.Clamp(rbVelocity.z, zMin, zMax)
			);
		ret = new Vector3
			(
			Mathf.MoveTowards(ret.x, 0.0f, 0.05f),
			0.0f,
			Mathf.MoveTowards(ret.z, 0.0f, 0.1f)
			);
		return ret;
	}
}
