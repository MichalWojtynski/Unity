using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector3 fingerPos;
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    public GameObject eksplozja;
    public bool isalive = true;
    void Update()
    {

        if (Input.touchCount>0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
               Debug.Log("Touch began");
            }
             else  if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
            //    Debug.Log("Touch is moving");
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Debug.Log("Touch ended");
            }
        }

        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
        }

        transform.position = Vector2.Lerp(transform.position, new Vector2(mousePosition.x,-4.25f), moveSpeed);
    }

    void shoot()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag =="Enemy_bullet" || col.tag =="Enemy")
        {
            Debug.Log("Dostal");
            isalive = false;
            GameObject.FindGameObjectWithTag("Menager").GetComponent<Menager>().PlayerIsDead();
            Instantiate(eksplozja, transform.position, transform.rotation);
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
