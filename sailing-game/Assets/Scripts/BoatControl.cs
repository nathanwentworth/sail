﻿using UnityEngine;
using System.Collections;

public class BoatControl : MonoBehaviour {

	//SERIALZED FIELD VARIABLES
	public int boatSpeedSet;
	public bool inputEnabled;
	public AudioSource slow;
	public AudioSource fast;

	//DECLARING PRIVATE VARIABLES
	private Rigidbody2D boatRigid;
	private float boatSpeed;
	private TrailRenderer trail;
	private int oldBoatSpeedSet;
	private bool playSailSound;

	void Start () {

		//INITIALIZING VARIABLES
		boatRigid = this.GetComponent<Rigidbody2D> ();
		trail = this.GetComponent<TrailRenderer> ();
		boatSpeed = 0;
		boatSpeedSet = 0;
		oldBoatSpeedSet = 0;
		inputEnabled = true;
	}

	void Update () {

		if (inputEnabled) {

			//BOAT ROTATION
			float moveY = Input.GetAxis ("Horizontal");
			if (moveY < 0) {
				transform.Rotate (35 * (-Vector3.forward * Time.deltaTime));
			} else if (moveY > 0) {
				transform.Rotate (35 * (Vector3.forward * Time.deltaTime));
			}

			//BOAT SPEED SET INPUT
			if (Input.GetButton ("X")) {
				boatSpeedSet = 0;
				playSailSound = true;
			}
			if (Input.GetButton ("Y")) {
				boatSpeedSet = 1;
				playSailSound = true;
			}
			if (Input.GetButton ("B")) {
				boatSpeedSet = 2;
				playSailSound = true;
			}
		}

		if (playSailSound) {
			if (oldBoatSpeedSet < boatSpeedSet) {
				fast.Play ();
				oldBoatSpeedSet = boatSpeedSet;
				playSailSound = false;
			} else if (oldBoatSpeedSet > boatSpeedSet) {
				slow.Play ();
				oldBoatSpeedSet = boatSpeedSet;
				playSailSound = false;
			}
		}
			
		switch (boatSpeedSet) {
		case 0:
			BoatStop ();
			break;
		case 1:
			BoatSlow ();
			break;
		case 2:
			BoatFast ();
			break;
		}

		if (Mathf.Abs (boatRigid.velocity.y) <= 0.05 && Mathf.Abs (boatRigid.velocity.x) <= 0.05) {
			trail.enabled = false;
		} else {
			trail.enabled = true;
		}

		//END UPDATE
	}

	private void BoatStop(){
		boatSpeed = Mathf.Lerp (boatSpeed, 0, Time.deltaTime);
		boatRigid.velocity = transform.up * boatSpeed;
	}

	private void BoatSlow(){
		boatSpeed = Mathf.Lerp (boatSpeed, -0.5f, Time.deltaTime);
		boatRigid.velocity = transform.up * boatSpeed;
	}

	private void BoatFast(){
		boatSpeed = Mathf.Lerp (boatSpeed, -1, Time.deltaTime);
		boatRigid.velocity = transform.up * boatSpeed;
	}
		
}
