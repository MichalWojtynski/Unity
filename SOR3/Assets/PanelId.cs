using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using miejscestruktow;

public class PanelId : MonoBehaviour {

    private int ID=0;

    public void SetID(int getID)
    {
        ID = getID;
    }

    public void LadujKomentarze()
    {
       
        if(ID ==0)
            Debug.Log("ID = 0 nie sciagam komentarzy. ID_olacowki: " + ID);
        else
        {
            Debug.Log("sciagam komentarze. ID_placowki: " + ID);
            GameObject.FindGameObjectWithTag("ServerConnection").GetComponent<ServerConnect>().sciagnij_komentarze(ID);

        }
    }
}
