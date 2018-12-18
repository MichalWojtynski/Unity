using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour {

    public int health;
    SpriteRenderer sprite;
    public Color dmgcolor;
    public GameObject eksplozja;


	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        sprite.color = Color.Lerp(sprite.color, Color.white, 5 * Time.deltaTime);
		if(health<=0)
        {
            Destroy(gameObject);
            Instantiate(eksplozja, transform.position, transform.rotation);
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet")
        {
            sprite.color = dmgcolor;
            health -= 20;
           // Destroy(col,0f);
        }
    }

}

