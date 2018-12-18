using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombaspawn : MonoBehaviour {

    float timer;
    public float timeBetweenExplosions;
    public GameObject grenade;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (timer >= timeBetweenExplosions)
        {
            Shoot();
        }

    }

    void Shoot()
    {
        timer = 0f;
        for(int i = 0; i<=20; i++)
        {
      //      Instantiate(grenade, transform.position, new Ro(transform.rotation.x, transform.rotation.y, transform.rotation.w));
        }
    }
}
