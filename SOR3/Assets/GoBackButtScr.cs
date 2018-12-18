using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackButtScr : MonoBehaviour {

	public void LadujScene(int nr)
    {

        SceneManager.LoadScene(nr);
    }

  
}
