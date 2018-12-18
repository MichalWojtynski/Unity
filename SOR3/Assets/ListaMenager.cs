using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using miejscestruktow;

public class ListaMenager : MonoBehaviour {
    

    public void Set_id_miasta(int id)
    {
        GameObject.FindGameObjectWithTag("ServerConnection").GetComponent<ServerConnect>().Wyszukaj(id);
        SceneManager.LoadScene(4);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(1);
    }
}
