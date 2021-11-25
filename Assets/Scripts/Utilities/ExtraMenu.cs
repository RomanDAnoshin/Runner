using Road;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace Utilities
{
    public class ExtraMenu : MonoBehaviour
    {
        [MenuItem("ProjectFunctional/Regenerate RoadBlocks Mirrors")]
        private static void RegenerateRoadBlocksMirrors()
        {
            ClearMirrorsDirectory();

            var prefabs = Resources.LoadAll<GameObject>("RoadBlocks/Common");
            foreach(var block in prefabs) {
                if (!block.GetComponent<RoadBlockData>().IsSymmetrical) {
                    CreateMirrorBlockPrefab(block);
                }
            }
        }

        private static void CreateMirrorBlockPrefab(GameObject origin)
        {
            var originPath = AssetDatabase.GetAssetPath(origin);
            var newPath = "Assets/Resources/RoadBlocks/Mirrors/" + origin.name + " (Mirror)" + ".prefab";
            AssetDatabase.CopyAsset(originPath, newPath);
            var copy = PrefabUtility.LoadPrefabContents(newPath);
            copy.GetComponent<RoadBlockController>().RotateObjectsMirrored();
            PrefabUtility.SaveAsPrefabAsset(copy, newPath);
            PrefabUtility.UnloadPrefabContents(copy);
        }

        private static void ClearMirrorsDirectory()
        {
            var path = "Assets/Resources/RoadBlocks/Mirrors";
            if (Directory.Exists(path)) {
                Directory.Delete(path);
            }
            Directory.CreateDirectory(path);
        }
    }
}
