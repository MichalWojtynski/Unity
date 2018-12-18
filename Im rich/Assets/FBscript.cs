using System.Collections;
using UnityEngine;
using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class FBscript : MonoBehaviour {

    public GameObject DialogLoggedIn;
    public GameObject DialogLoggedOut;
    public GameObject DialogUsername;
    public GameObject DialogProfilePic;

    public string AppLinkURL { get; set; }

    void Awake()  {

        FBMenager.Instance.InitFB();
        DealWithMenus(FB.IsLoggedIn);
    }


    public void FBlogin()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");

        FB.LogInWithReadPermissions(permissions, AuthCallBack);
    }

    void AuthCallBack(IResult result)
    {
        if(result.Error != null)  {
            Debug.Log(result.Error);
        }else {
            if (FB.IsLoggedIn) {
                FBMenager.Instance.IsLoggedIn = true;
                FBMenager.Instance.getProfile();
                Debug.Log("FB is logged in");
            } else {
                Debug.Log("FB is not logged in");
            }
            DealWithMenus(FB.IsLoggedIn);
        }
    }
    void DealWithMenus(bool isLoggedIn)
    {
        if (isLoggedIn) {
            DialogLoggedIn.SetActive(true);
            DialogLoggedOut.SetActive(false);

            if(FBMenager.Instance.ProfileName!= null)  {
                Text UserName = DialogUsername.GetComponent<Text>();
                UserName.text = FBMenager.Instance.ProfileName;
            } else {
                StartCoroutine("WaitForProfileName");
            }

            if (FBMenager.Instance.ProfilePic != null)
            {
                Image ProfiPic = DialogProfilePic.GetComponent<Image>();
                ProfiPic.sprite = FBMenager.Instance.ProfilePic;
            }
            else
            {
                StartCoroutine("WaitForProfilePic");
            }

        } else {
            DialogLoggedIn.SetActive(false);
            DialogLoggedOut.SetActive(true);
        }
    }

    IEnumerator WaitForProfileName()
    {
        while(FBMenager.Instance.ProfileName == null)  {
            yield return null;
        }
        DealWithMenus(FB.IsLoggedIn);

    }
    IEnumerator WaitForProfilePic()
    {
        while (FBMenager.Instance.ProfilePic == null)
        {
            yield return null;
        }
        DealWithMenus(FB.IsLoggedIn);

    }

    public void Share()
    {
        FBMenager.Instance.Share();
    }
}
