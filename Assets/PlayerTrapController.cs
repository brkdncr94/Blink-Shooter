﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrapController : MonoBehaviour {

	public GameObject sphereObject;
	public float offset;
	public int numberSpheres;
	public float angularSpeed;
	public GameObject player;
	public float preparationTime;
	public float pauseTime;
	public float persistenceTime;
	private float activationTime;
	public float sphereSpeed;
	private List<GameObject> sphereList;

	// Use this for initialization
	void Start () {
		activationTime = Time.time + preparationTime;
		persistenceTime = Time.time + persistenceTime;
		sphereList = new List<GameObject>();

		for (int i = 0; i < numberSpheres; i++) {
			float angle = 2*Mathf.PI * i / numberSpheres;
			Vector3 direction = new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle), 0);
			print (direction);
			Vector3 shieldPosition = transform.TransformPoint (direction * offset );
			GameObject sphere = Instantiate<GameObject> (sphereObject, shieldPosition, Quaternion.Euler(0,0, Mathf.Rad2Deg*angle));
//			sphere.transform.localScale *= transform.lossyScale.x;
			sphere.transform.parent = transform;
			sphereList.Add (sphere);
			sphere.GetComponent<CircleCollider2D>().enabled = false;
			sphere.GetComponent<ShotAttributes> ().setTeamID(2);

		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position;
		transform.Rotate (0, 0, angularSpeed);

		if (Time.time >= activationTime) {
			foreach (GameObject sphere in sphereList){
				sphere.GetComponent<EnergyActivation> ().Prepare (player.transform.position);
			}
			Destroy (this);
		}
	}
}
