using AEM.Managers.Tilemanager;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AStarTileManager))]
[CanEditMultipleObjects]
public class AStarTilemanagerEditor : TilemanagerEditor
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void BeforeDefaultInspector()
    {
        base.BeforeDefaultInspector();
    }

    protected override void AfterDefaultInspector()
    {
        Tilemanager t = target as Tilemanager;

        ShowDisabledList(serializedObject.FindProperty("Sets"));
        ShowDisabledList(serializedObject.FindProperty("Rows"));
        ShowDisabledList(serializedObject.FindProperty("Tiles"));

        foreach(string key in t.TilesetSpawned.Keys)
        {
            if(t.TilesetSpawned[key] == null)
            {
                if (GUILayout.Button("Spawn Tileset: \""+ key+"\""))
                {
                    t.SpawnTileset(key,key,Vector3.zero,OriginAnchor.BOTTOMLEFT);
                    break;
                }
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Despawn Tileset: \""+ key+"\"",EditorStyles.miniButtonLeft))
                {
                    t.RemoveTileset(key);
                    break;
                }
                if (GUILayout.Button("ReSize",EditorStyles.miniButtonRight))
                {
                    t.ReSize(key);
                    break;
                }    
                EditorGUILayout.EndHorizontal();
            }
            
            t.Tilesets[key].TileSize = EditorGUILayout.Vector2Field("Tileset: \""+key+"\" TileSize",t.Tilesets[key].TileSize);
                                
        }

        if (GUILayout.Button("Reset Tiles"))
            t.Reset();
    }
}

