using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Selection : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		//https://www.youtube.com/watch?v=6zNHaLdVk80
		//http://www.stormtek.geek.nz/rts_tutorial/part6.php 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (1)) {
			GameObject P1 = GameObject.FindWithTag ("Player1");
			P1.GetComponent<PointClickMovement> ().enabled = true;

			GameObject P2 = GameObject.FindWithTag ("Player2");
			P2.GetComponent<PointClickMovement> ().enabled = false;

			GameObject P3 = GameObject.FindWithTag ("Player3");
			P3.GetComponent<PointClickMovement> ().enabled = false;

			GameObject P4 = GameObject.FindWithTag ("Player4");
			P4.GetComponent<PointClickMovement> ().enabled = false;
		}
	}
}
