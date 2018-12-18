using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horizforce : MonoBehaviour {

    public float speed = 0.1f;
     float angle;

    // Use this for initialization
    void Start()
    {
        angle = Random.Range(-0.01f, 0.01f);
        Destroy(this.gameObject, 5f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed, angle, 0));
    }
}
