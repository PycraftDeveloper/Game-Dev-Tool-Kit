using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This script allows the displaying of debug messages on a Canvas for debugging in-game. Only the
/// last 22 logs are displayed for performance reasons.
/// </summary>

public class DebugDisplay : MonoBehaviour
{
    private List<string> debugLogs = new List<string>();

    [Tooltip("The Text Mesh Pro that should be used to display the logs.")]
    public TMP_Text display;

    /// <summary>
    /// Starts listening for log events on start
    /// </summary>
    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    /// <summary>
    /// Stops listening for log events on finish
    /// </summary>
    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    /// <summary>
    /// This script handles the updating and rolling buffer of log messages to display.
    /// </summary>
    /// <param name="logString">The contents of the log message recieved.</param>
    /// <param name="stackTrace">The stack trace to where the log originated from.</param>
    /// <param name="type">The type of log</param>
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Log)
        {
            string[] splitString = logString.Split(char.Parse(":"));

            debugLogs.Add(logString);
        }

        if (debugLogs.Count > 22)
        {
            debugLogs.RemoveAt(0);
        }

        string displayText = "";
        foreach (string log in debugLogs)
        {
            displayText += log + "\n";
        }

        display.text = displayText;
    }
}