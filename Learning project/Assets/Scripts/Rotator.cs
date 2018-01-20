using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rotator : MonoBehaviour {
	public double force;

	void Update () {
		transform.Rotate(new Vector3( 15, 30, 45) * Time.deltaTime);
	}

}
  