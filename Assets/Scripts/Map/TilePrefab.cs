using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Tilemaps;


using UnityEditor;
public class TilePrefab : Tile
{
    [MenuItem("Assets/Create/TilePrefab")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<TilePrefab>();
    }

    
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        bool result = base.StartUp(position, tilemap, go);
        if (go)
        {
            Tile3D tile3D = go.GetComponent<Tile3D>();
            if(tile3D)
            {
                tile3D.spriteRenderer.sprite = this.sprite;
            }
            go.transform.localEulerAngles = new Vector3(0, 0, 0);
            Debug.Log("tilemap:" + go.transform.parent.name);
            go.transform.localScale = Vector3.one;
        }
        return true;
    }
}
