using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;
public class CustomConsoleCommands
{
    [ConsoleMethod("m_debugger", "Open Applovin mediation debugger")]
    public static void OpenMediationDebugger()
    {
        MaxSdk.ShowMediationDebugger();
    }
}
