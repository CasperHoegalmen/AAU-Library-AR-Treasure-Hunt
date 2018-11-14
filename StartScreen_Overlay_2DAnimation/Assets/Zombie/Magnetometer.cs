using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetometer : MonoBehaviour {

    public GameObject arrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        arrow.transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading, 0);

    }
}
