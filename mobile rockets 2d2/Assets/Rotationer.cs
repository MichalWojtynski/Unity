using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotationer : MonoBehaviour {

    int randomizer;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 15f);
        randomizer = Random.Range(0, 10);
        if(randomizer <5)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
