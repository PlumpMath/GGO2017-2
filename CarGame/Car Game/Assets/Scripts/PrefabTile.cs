using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "PrefabTile", menuName = "PrefabTile")]
public class PrefabTile :TileBase
{
    public Sprite m_Sprite;
    public Sprite m_Preview;
    public GameObject Prefab;

    public Vector3 offset;

    public Matrix4x4 m_transform = Matrix4x4.identity;
    public TileFlags flags;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        

        if (go != null)
        {

            go.transform.position += tilemap.GetTile<PrefabTile>(position).offset;
            go.transform.rotation = tilemap.GetTransformMatrix(position).rotation;

        }
       
        return base.StartUp(position, tilemap, go);

    }

    public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
    {

        return false;
    }

   


    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        if (Prefab != null)
        {
           
            //switch to m_Sprite for build
            tileData.sprite = m_Preview;


            tileData.color = Color.white;
            tileData.transform = this.m_transform;
            tileData.gameObject = Prefab;
            tileData.flags = this.flags;
        }
    }

}
