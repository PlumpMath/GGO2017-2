using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientText : BaseTextMeshEffect
{
    public Color topColor = Color.white;
    public Color bottomColor = Color.black;

 
    protected override List<UIVertex> ProcessCharacters(List<UIVertex> verts, int characterindex)
    {
        for (int i = 0; i < 6; i++)
        {
            UIVertex c;
            c = verts[i];
            if (i == 0 ||
                i == 1 ||
                i == 5)
            {
                c.color = topColor;   
            }
            else
            {
                c.color = bottomColor;
            }
            verts[i] = c;
        }
        return base.ProcessCharacters(verts, characterindex);
    }

   

}
