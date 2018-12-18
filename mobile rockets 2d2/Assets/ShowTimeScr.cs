using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTimeScr : MonoBehaviour {
    public float time;
    public float speed;
    // Use this for initialization
    void Start () {

      

	}
	
	// Update is called once per frame
	void Update () {
        if (time >= 0)
        {
            time -= Time.deltaTime / speed;
            GetComponent<Slider>().value = time;
        }

	}

    public void startcounting()
    {
        time = 1f;
    }
}
