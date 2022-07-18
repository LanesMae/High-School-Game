using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P4Selection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
	if (Input.GetMouseButton (1)) {
					
			GameObject P4 = GameObject.FindWithTag ("Player4");
			P4.GetComponent<PointClickMovement> ().enabled = true;
		
			GameObject P2 = GameObject.FindWithTag ("Player2");
			P2.GetComponent<PointClickMovement> ().enabled = false;

			GameObject P3 = GameObject.FindWithTag ("Player3");
			P3.GetComponent<PointClickMovement> ().enabled = false;

			GameObject P1 = GameObject.FindWithTag ("Player1");
			P1.GetComponent<PointClickMovement> ().enabled = false;
		}
	}
}