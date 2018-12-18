using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dislajkscr : MonoBehaviour
{

    bool klikniety = false;
    public Color czerwony;

    public void klik()
    {
        if (!klikniety)
        {
            GetComponent<Image>().color = czerwony;
            klikniety = true;
        }
        else
            unklik();
    }

    public void unklik()
    {
        klikniety = false;
        GetComponent<Image>().color = Color.white;
    }
}
