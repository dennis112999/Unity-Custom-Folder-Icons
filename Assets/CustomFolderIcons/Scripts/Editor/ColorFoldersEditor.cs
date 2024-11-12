using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace Dennis.Tools.MineralEditor
{
    /// <summary>
    /// Handles custom folder icons in the Unity project window.
    /// </summary>
    [InitializeOnLoad]
    static class ColorFoldersEditor
    {
        static string s_iconName;

        static ColorFoldersEditor()
        {
            EditorApplication.projectWindowItemOnGUI -= OnGUI;
            EditorApplication.projectWindowItemOnGUI += OnGUI;
        }

        private static void OnGUI(string guid, Rect selectionRect)
        {
            Color backgroundColor;
            Rect folderRect = GetFolderRect(selectionRect, out backgroundColor);

            // Retrieve the icon GUID from EditorPrefs
            string iconGuid = MineralPrefs.GetString(guid, "");

            // Return if no valid icon is set
            // "00000000000000000000000000000000" is a specific value in Unity that represents an invalid or empty GUID.
            if (iconGuid == "" || iconGuid == "00000000000000000000000000000000") return;

            // Draw background color for the folder
            EditorGUI.DrawRect(folderRect, backgroundColor);

            // Load and draw the folder icon texture
            string folderIconPath = AssetDatabase.GUIDToAssetPath(iconGuid);
            Texture2D folderTex = AssetDatabase.LoadAssetAtPath<Texture2D>(folderIconPath);
            if (folderTex == null) return;

            GUI.DrawTexture(folderRect, folderTex);
        }

        /// <summary>
        /// Sets the icon name
        /// </summary>
        /// <param name="iconName">Name of the icon file</param>
        public static void SetIconName(string iconName)
        {
            string folderGuid = GetSelectedFolderGuid();
            if (string.IsNullOrEmpty(folderGuid)) return;

            // Construct icon path and retrieve its GUID
            // Demo Use
            string iconPath = "Assets/CustomFolderIcons/Icons/Colored/" + iconName + ".png";
            string iconGuid = AssetDatabase.GUIDFromAssetPath(iconPath).ToString();

            // Save the icon GUID in EditorPrefs for the selected folder
            MineralPrefs.SetString(folderGuid, iconGuid);

            // Update the stored icon name
            s_iconName = iconName;
        }

        /// <summary>
        /// Resets the custom icon for the selected folder to the default.
        /// </summary>
        public static void ResetFolderTex()
        {
            string folderGuid = GetSelectedFolderGuid();

            if (string.IsNullOrEmpty(folderGuid)) return;

            // Remove the saved icon GUID from EditorPrefs
            MineralPrefs.DeleteKey(folderGuid);
        }

        /// <summary>
        /// Determines the folder icon rectangle and background color based on selection rect.
        /// </summary>
        /// <param name="selectionRect">Original selection rectangle.</param>
        /// <param name="backgroundColor">Output parameter for background color.</param>
        /// <returns>Adjusted rectangle for folder icon drawing.</returns>
        private static Rect GetFolderRect(Rect selectionRect, out Color backgroundColor)
        {
            Rect folderRect;
            backgroundColor = new Color(.2f, .2f, .2f);

            // Define folder icon rectangle based on selection rectangle position and size
            if (selectionRect.x < 15)
            {
                folderRect = new Rect(selectionRect.x + 3, selectionRect.y, selectionRect.height, selectionRect.height);
            }
            else if (selectionRect.x >= 15 && selectionRect.height < 20)
            {
                folderRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.height, selectionRect.height);
                backgroundColor = new Color(.21f, .21f, .21f);
            }
            else
            {
                folderRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width, selectionRect.width);
            }

            return folderRect;
        }

        /// <summary>
        /// Retrieves the GUID of the currently selected folder.
        /// </summary>
        /// <returns>The GUID of the selected folder as a string.</returns>
        private static string GetSelectedFolderGuid()
        {
            string folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            return AssetDatabase.GUIDFromAssetPath(folderPath).ToString();
        }
    }
}
#endif
