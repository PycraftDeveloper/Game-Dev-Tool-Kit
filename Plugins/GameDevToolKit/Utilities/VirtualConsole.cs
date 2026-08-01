using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Flags]
public enum LogTypeFilter
{
    None = 0,
    Log = 1 << 0,
    Assert = 1 << 1,
    Warning = 1 << 2,
    Error = 1 << 3,
    Exception = 1 << 4,

    All = Log | Assert | Error | Exception | Warning
}

/// <summary>
/// This script allows the displaying of debug messages on a Canvas for debugging in-game. Only the
/// last 22 logs are displayed for performance reasons.
/// </summary>

public class DebugDisplay : MonoBehaviour
{
    private List<string> debugLogs = new List<string>();

    [Tooltip("The Text Mesh Pro that should be used to display the logs.")]
    [SerializeField] private TMP_Text LoggingTextElement;

    [Tooltip("Which log types to display")]
    [SerializeField] private LogTypeFilter LoggingTypes = LogTypeFilter.All;

    [Tooltip("How many logs to display?")]
    [SerializeField] private int NumberOfLogsToShow = 22;

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
        LogTypeFilter flag = type switch
        {
            LogType.Log => LogTypeFilter.Log,
            LogType.Assert => LogTypeFilter.Assert,
            LogType.Error => LogTypeFilter.Error,
            LogType.Exception => LogTypeFilter.Exception,
            LogType.Warning => LogTypeFilter.Warning,
            _ => LogTypeFilter.None
        };

        if ((LoggingTypes & flag) == 0)
            return;

        debugLogs.Add($"{type}: {logString}");

        if (debugLogs.Count > NumberOfLogsToShow)
        {
            debugLogs.RemoveAt(0);
        }

        string displayText = "";
        foreach (string log in debugLogs)
        {
            displayText += log + "\n";
        }

        LoggingTextElement.text = displayText;
    }
}