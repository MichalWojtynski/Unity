using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForce : MonoBehaviour {

    public float speed = 0.1f;
    public float angle_maxis = 0.01f;
    private float angle;

	// Use this for initialization
	void Start () {
        angle = Random.Range(-angle_maxis, angle_maxis);
        if (gameObject.name != "GameObject" )
        {
            Destroy(this.gameObject, 4f);
        }

	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(angle, speed, 0));
	}
}
