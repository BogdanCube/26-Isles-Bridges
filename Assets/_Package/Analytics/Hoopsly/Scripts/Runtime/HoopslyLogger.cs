using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hoopsly.Settings;


public static class HoopslyLogger
{
    public static void LogMessage(string message, HoopslyLogLevel priority, H_LogType logType = H_LogType.Message)
    {
        if((int)priority <= (int)HoopslySettings.Instance.GeneralSettings.HoopslyEventsLogLevel)
        {
            switch (logType)
            {
                case H_LogType.Message:
                    Debug.Log(message);
                    break;
                case H_LogType.Warning:
                    Debug.LogWarning(message);
                    break;
                case H_LogType.Error:
                    Debug.LogError(message);
                    break;
                default:
                    break;
            }
        }
    }
}
public enum H_LogType { Message, Warning, Error };

