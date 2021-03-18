using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogUtils
{
    public string getClassName()
    {
        return this.GetType().Name;
    }

    // ========== MARK: - Fields and properties ==========
    private static LogUtils _instance;
    public static LogUtils instance
    {
        get
        {
            if (_instance == null)
                _instance = new LogUtils();
            return _instance;
        }
    }

    // ========== MARK: - MonoBehaviour Methods ==========
    private LogUtils()
    {

    }

    // ========== MARK: - Public Methods ==========
    public void Log(params object[] logs)
    {
        if (!GameSetting.isLog)
            return;
        string logString = "";
        for (int i = 0; i < logs.Length; i++)
        {
            logString += logs[i].ToString();
            if (i < logs.Length - 1)
                logString += " ";
        }
        Debug.Log(getClassName() + " LOG: " + logString);
    }
}
