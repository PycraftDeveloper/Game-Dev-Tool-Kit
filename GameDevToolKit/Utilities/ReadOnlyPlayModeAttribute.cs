using UnityEngine;

namespace GameDevToolKit
{
    /// <summary>
    /// Keeps the field editable in the editor, but locks it to read-only during Play Mode.
    /// </summary>
    public class ReadOnlyPlayModeAttribute : PropertyAttribute
    {
    }
}