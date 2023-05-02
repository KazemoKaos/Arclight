using System.IO;
using UnityEngine;

public class ThumbnailCreator : MonoBehaviour
{

    /// <summary>
    /// 
    /// </summary>
    public Transform[] Entities;

    /// <summary>
    /// 
    /// </summary>
    public string TargetPath = "Icons";

#if UNITY_EDITOR
    /// <summary>
    /// 
    /// </summary>
    public void generateEntityIcons()
    {
        Debug.Log("Generating icons for entities...");
        string path = Application.dataPath + "/" + TargetPath;
        foreach (Transform e in Entities)
        {
            Texture2D icon = UnityEditor.AssetPreview.GetAssetPreview(e.gameObject);

            if (icon == null)
            {
                int count = 3;
                while (icon == null && count > 0)
                {
                    --count;
                    icon = (Texture2D)UnityEditor.AssetDatabase.GetCachedIcon(UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.PrefabUtility.GetPrefabParent(e.gameObject)));
                    System.Threading.Thread.Sleep(300);
                }
            }
            if (icon == null)
            {
                Debug.LogWarning("Could not load icon for " + e.name);
                continue;
            }


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fullPath = path + "/" + e.name + ".png";
            File.WriteAllBytes(fullPath, icon.EncodeToPNG());
            Debug.Log("Generated icon for " + e.name);
        }

        Debug.Log("Generation complete - Saved icons to path: " + path);
    }
#endif


}
