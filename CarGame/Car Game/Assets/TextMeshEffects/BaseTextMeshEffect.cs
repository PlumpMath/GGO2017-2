using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseTextMeshEffect : BaseMeshEffect
{

    public int StartIndex;
    public int EndIndex;
    private Graphic targetGraphic;

    protected override void Start()
    {
        targetGraphic = GetComponent<Graphic>();
    }

    public override void ModifyMesh(VertexHelper vh)
    {
        List<UIVertex> vertices = new List<UIVertex>();
        List<UIVertex> newVerts = new List<UIVertex>();

        vh.GetUIVertexStream(vertices);

        for (int i = 0; i < (vertices.Count / 6); i++)
        {
            if (i >= StartIndex && i < EndIndex)
            {
                newVerts.AddRange(ProcessCharacters(vertices.GetRange(i * 6, 6), i));
            }
            else
            {
                newVerts.AddRange(vertices.GetRange(i * 6, 6));
            }
        }

        vh.Clear();
        vh.AddUIVertexTriangleStream(newVerts);

    }

    protected virtual List<UIVertex> ProcessCharacters(List<UIVertex> verts, int characterindex)
    {



        return verts;

    }


	
}
