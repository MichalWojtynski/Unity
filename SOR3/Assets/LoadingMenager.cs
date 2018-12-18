using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingMenager : MonoBehaviour {

    float timer = 0f;
    public int speed = 10;
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    

    timer += Time.deltaTime * speed;
        GetComponent<Slider>().value = timer;
        if (timer >= 100f)
            SceneManager.LoadScene(1);
	}


}
