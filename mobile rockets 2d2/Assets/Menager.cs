using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menager : MonoBehaviour {

    public GameObject Player;
    public GameObject StartButton;
    public GameObject ReplayButton;

    public GameObject samolocik;
    public GameObject bombowiec;
    public GameObject sciana;

    public GameObject pole1;
    public GameObject pole2;
    public GameObject pole3;
    public GameObject Win;
   
    public GameObject Intro;
    public GameObject slide;

    public GameObject zgoda;

    public GameObject Upomnienie;
    public GameObject przyciemnienie;
    bool czywyrazilzgode = false;
    bool once = false;
    bool isPlayerAlive = true;
    bool started = false;
    private float timer;

    public float timeBetweenAttack;


    //PreStart
    public void Start()
    {
       // Intro.SetActive(false);
     //   ReplayButton.SetActive(false);       
      //  podziekowanie.SetActive(false);
    }

  

    public void GameStart()
    {
        Intro.SetActive(false);       
        Player.SetActive(true);
        started = true;
        InvokeRepeating("Spawn", 2f, timeBetweenAttack);
        slide.GetComponent<ShowTimeScr>().startcounting();
    }

    public void WWWConnect()
    {
        Application.OpenURL("http://mobilerockets.pl/");
    }

    public void FbConnect()
    {
        Application.OpenURL("https://www.facebook.com/mobilerockets/");
    }

    public void InstaConnect()
    {
        Application.OpenURL("https://www.instagram.com/mobilerockets/");
    }




    //Start

    void Spawn()
    {
        int randomizer = Random.Range(0, 5);
        switch(randomizer)
        {
            case 0:
                Instantiate(samolocik, transform.position, transform.rotation);
                break;
            case 1:
                Instantiate(bombowiec, transform.position, transform.rotation);
                break;
            default:
                Instantiate(sciana, transform.position, transform.rotation);
                break;
        }

    }
    //InGame
    public void Update()
    {
        if (started)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 15f && !once && Player.activeSelf)
        {
            once = true;
            WonStuff();
        }
    }

    //Dead and afterdead
    public void ZmianaZgody()
    {
        if (!czywyrazilzgode)
        {
            zgoda.SetActive(true);
            czywyrazilzgode = true;
        }
        else if(czywyrazilzgode)
        {
            zgoda.SetActive(false);
            czywyrazilzgode = false;
        }
    }

  
   

    public void PlayerIsDead()
    {
        CancelInvoke();
        isPlayerAlive = false;
        ReplayButton.SetActive(true);
    }

    public void Replay()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void WonStuff()
    {
           Destroy(Player);
      //  Player.SetActive(false);
        CancelInvoke();
        pole1.SetActive(true);
        pole2.SetActive(true);
        pole3.SetActive(true);
        Win.SetActive(true);
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(go);
        }
       // Intro.SetActive(true);

    }

    public void Exit()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    
    public void Zatwierdzone()
    {
        if(!czywyrazilzgode)
        {
            przyciemnienie.SetActive(true);
            Upomnienie.SetActive(true);
            return;
        }
        GameObject.FindGameObjectWithTag("ServerConnectionMenager").GetComponent<ServerConnection>().AddClient(GameObject.FindGameObjectWithTag("NamePlace").GetComponent<Text>().text, GameObject.FindGameObjectWithTag("ScNamePlace").GetComponent<Text>().text, GameObject.FindGameObjectWithTag("EmailPlace").GetComponent<Text>().text);
     
     
        
       // ReplayButton.SetActive(true);
        //Destroy(pole1);
        //Destroy(pole2);
       // Destroy(pole3);
        //Destroy(text);
        //Destroy(button);
      //  podziekowanie.SetActive(true);
    }
}
