using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debuggerscr : MonoBehaviour
{

	
     string myLog;
    Queue myLogQueue = new Queue();
    private GUIStyle guiStyle = new GUIStyle();

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        string newString = "\n [" + type + "] : " + myLog;
        myLogQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue(newString);
        }
        myLog = string.Empty;
        foreach (string mylog in myLogQueue)
        {
            myLog += mylog;
        }
    }

    void OnGUI()
    {
        guiStyle.fontSize = 20;
       

        GUILayout.Label(myLog, guiStyle);
    }
}