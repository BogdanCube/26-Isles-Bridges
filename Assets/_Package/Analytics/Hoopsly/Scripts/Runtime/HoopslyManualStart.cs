using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hoopsly.Internal;

public class HoopslyManualStart
{
    public static void StartSDK()
    {
        HoopslyLauncher.Instance.StartSDK();
    }

    public static void StartSDK(string uuid)
    {
        HoopslyLauncher.Instance.StartSDK(uuid);
    }
}
