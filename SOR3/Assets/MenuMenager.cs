using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMenager : MonoBehaviour {

	
    public void LadujOcenianie()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.SecondSky.SORinfoMazowsze");
    }

    public void LadujInfo()
    {
        SceneManager.LoadScene(2);
    }

    public void LadujListe()
    {
        SceneManager.LoadScene(3);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    private void Update()
    {
           if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
