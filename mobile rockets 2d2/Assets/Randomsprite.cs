using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomsprite : MonoBehaviour {

    public Sprite ast1;
    public Sprite ast2;
    public Sprite ast3;
     public  float speed;
    public int randomizer;
    float x;
	// Use this for initialization
	void Start () {
        x = speed = Random.Range(0f, 180f);
        speed = Random.Range(-5f, 5f);
        randomizer = Random.Range(0, 3);
        if(randomizer == 0)transform.GetComponent<SpriteRenderer>().sprite = ast1;
        if (randomizer == 1) transform.GetComponent<SpriteRenderer>().sprite = ast2;
        if (randomizer ==2) transform.GetComponent<SpriteRenderer>().sprite = ast3;
    }
	
	// Update is called once per frame
	void Update () {
        
        x += Time.deltaTime * 40 * speed;
        transform.rotation = Quaternion.Euler(0, 0, x);
    }
}
