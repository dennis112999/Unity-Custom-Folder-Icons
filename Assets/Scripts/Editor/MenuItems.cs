using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace Dennis.Tools.MineralEditor
{
    /// <summary>
    /// custom menu items under "Assets/Mineral" for setting and resetting folder icons.
    /// </summary>
    static class MenuItems
    {
        // The higher the priority value, the lower the position.
        const int MenuPriority = 100000;

        /// <summary>
        /// Demo - Menu item for setting a custom icon to red for the selected folder.
        /// </summary>
        [MenuItem("Assets/Mineral/Red", false, MenuPriority)]
        static void SetIconRed()
        {
            ColorFoldersEditor.SetIconName("Red");
        }

        /// <summary>
        /// Menu item for setting a custom icon
        /// 
        /// The offset of +11 in MenuPriority organizes this item among icon-setting options.
        /// </summary>
        [MenuItem("Assets/Mineral/Custom Icon", false, MenuPriority + 11)]
        static void CustomIcon()
        {
            IconFoldersEditor.ChooseCustomIcon();
        }

        /// <summary>
        /// Menu item for resetting the folder icon to default
        /// </summary>
        [MenuItem("Assets/Mineral/Reset Icon", false, MenuPriority + 23)]
        static void ResetIcon()
        {
            ColorFoldersEditor.ResetFolderTex();
        }

        /// <summary>
        /// Validation for the menu items to ensure only folders are selected
        /// </summary>
        /// <returns>Returns true if the selected item is a valid folder, otherwise false.</returns>
        [MenuItem("Assets/Mineral/Red", true)] // Demo
        [MenuItem("Assets/Mineral/Custom Icon", true)]
        [MenuItem("Assets/Mineral/Reset Icon", true)]
        static bool ValidateFolder()
        {
            // Check if a folder is selected
            if (Selection.activeObject == null) return false;

            Object selectedObject = Selection.activeObject;
            string objectPath = AssetDatabase.GetAssetPath(selectedObject);

            // Only enable menu items if the selected object is a valid folder
            return AssetDatabase.IsValidFolder(objectPath);
        }
    }
}
#endif
