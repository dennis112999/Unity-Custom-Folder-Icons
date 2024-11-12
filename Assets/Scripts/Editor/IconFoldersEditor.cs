using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace Dennis.Tools.MineralEditor
{
    /// <summary>
    /// Provides functionality to set custom icons for folders in the Unity project window.
    /// </summary>
    [InitializeOnLoad]
    static class IconFoldersEditor
    {
        static string selectedFolderGuid;
        static int controlID;

        static IconFoldersEditor()
        {
            EditorApplication.projectWindowItemOnGUI -= OnGUI;
            EditorApplication.projectWindowItemOnGUI += OnGUI;
        }

        private static void OnGUI(string guid, Rect selectionRect)
        {
            // Only process if the current asset's GUID matches the selected folder's GUID
            if (guid != selectedFolderGuid) return;

            // Check if the Object Picker is updated and matches our control ID
            if (Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == controlID)
            {
                Object selectedObject = EditorGUIUtility.GetObjectPickerObject();
                string folderIconGuid = AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(selectedObject)).ToString();

                EditorPrefs.SetString(selectedFolderGuid, folderIconGuid);
            }
        }

        /// <summary>
        /// Initiates the Object Picker to choose a custom icon for the currently selected folder.
        /// </summary>
        public static void ChooseCustomIcon()
        {
            selectedFolderGuid = Selection.assetGUIDs[0];

            // Generate a unique control ID for the Object Picker
            controlID = EditorGUIUtility.GetControlID(FocusType.Passive);

            // Show the Object Picker for selecting a Sprite as the custom folder icon
            EditorGUIUtility.ShowObjectPicker<Sprite>(null, false, "", controlID);
        }
    }
}
#endif
