using System.Collections;
using UnityEngine;
using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class FBMenager : MonoBehaviour {

    private static FBMenager _instance;

    public static FBMenager Instance
    {
        get {
            if (_instance == null) {
                GameObject fbm = new GameObject("FBMenager");
                fbm.AddComponent<FBMenager>();
            }

            return _instance;
        }
    }

    public bool IsLoggedIn { get; set; }
    public String ProfileName { get; set; }
    public Sprite ProfilePic {get;set;}
   // public string AppLinkURL { get; set;}


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _instance = this;
        IsLoggedIn = true;
    }

    public void InitFB()
    {
        if (!FB.IsInitialized) {
            FB.Init(SetInit, onHideUnity);
        } else{
            IsLoggedIn = FB.IsLoggedIn;
           
        }
    }

    void SetInit()
    {
        if (FB.IsLoggedIn) {
            Debug.Log("FB is logged in");
            getProfile();
        } else{
            Debug.Log("FB is not logged in");
        }
        IsLoggedIn = FB.IsLoggedIn ;
    }

    void onHideUnity(bool isGameShown)
    {
        if (!isGameShown) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    public void getProfile()
    {
        FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
        FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
       // FB.GetAppLink(DealWithAppLink);
    }

    void DisplayUsername(IResult result)
    {
       // Text Username = DialogUsername.GetComponent<Text>();
        if (result.Error == null)
        {
            ProfileName = "Hi, " + result.ResultDictionary["first_name"] + " now you can share with your friends, how rich you are.";
        }
        else
        {
            Debug.Log(result.Error);
        }
    }
    void DisplayProfilePic(IGraphResult result)
    {
        if (result.Texture != null)
        { 
            ProfilePic = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
    }

    //void DealWithAppLink(IAppLinkResult result)
   // {
    //    if(!String.IsNullOrEmpty(result.Url))
     //   {
     //       AppLinkURL = result.Url;
     //   }
   // }

    public void Share()
    {
        // FB.FeedShare(
        //    string.Empty,
        //    new Uri("http://www.meridianpeakhypnosis.com/wp-content/uploads/2014/02/money-addiction.jpg"),
        //    "I'M RICH",
        //    "I bought app for 400 dollars, just becouse I can afford it",
        //   "Want to share that you are rich? -buy an app",
        //   new Uri("http://www.meridianpeakhypnosis.com/wp-content/uploads/2014/02/money-addiction.jpg"),
        //   string.Empty,
        //   ShareCallback
        // );

        //  FB.ShareLink(
        //     new Uri("http://www.meridianpeakhypnosis.com/wp-content/uploads/2014/02/money-addiction.jpg"),
        //      callback: ShareCallback
        //  );

        FB.FeedShare(
            string.Empty,
             new Uri("https://4.bp.blogspot.com/-g6t9uH8_ics/WlZTdiMpALI/AAAAAAAAA5Q/idLR9tdmfKM6yI7YdmY2H9LzCTq0k_7JQCLcBGAs/s320/app%2Bim%2Brich%2Bfin.jpg"),
            "I'M RICH, I bought app for 400 dollars, just becouse I can afford it",
            "https://4.bp.blogspot.com/-g6t9uH8_ics/WlZTdiMpALI/AAAAAAAAA5Q/idLR9tdmfKM6yI7YdmY2H9LzCTq0k_7JQCLcBGAs/s320/app%2Bim%2Brich%2Bfin.jpg",
             callback:ShareCallback
        );


    }

    void ShareCallback(IResult result)
    {
        if(result.Cancelled) {
            Debug.Log("ShareCancelled");
        }else if(!string.IsNullOrEmpty(result.Error)){
            Debug.Log("Error on share");
        }else if (!string.IsNullOrEmpty(result.RawResult)) {
            Debug.Log("Succes on share");
        }

    }


}
