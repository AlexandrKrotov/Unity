using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float		speed;
	public Text			countText;
	public Text			winText;
	public Text			lossText;

	private Rigidbody	rb;
	private int			_count;

	void Start(){
		rb = GetComponent<Rigidbody>();
		_count = 0;
		SetCountText();
		winText.text = "";
		lossText.text = "";
	}
	void FixedUpdate(){

		float moveGorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveGorizontal, 0.0f, moveVertical);
		rb.AddForce(movement * speed);
		if (transform.position.y <= -1)
			lossText.text = "GAME OVER!!!";
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);
			_count++;
			SetCountText();
			if (_count >= 18)
				winText.text = "Win!!!";
		}
	}
	void SetCountText ()
	{
		countText.text = "Count: " + _count.ToString();
	}
}
