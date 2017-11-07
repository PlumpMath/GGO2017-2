using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveText : BaseTextMeshEffect
{
    public float speed;
    public float density;
    public float magnitude;

    protected override List<UIVertex> ProcessCharacters(List<UIVertex> verts, int characterindex)
    {
        for (int i = 0; i < verts.Count; i++)
        {
            UIVertex c = verts[i];
            c.position = c.position + new Vector3(1, magnitude * Mathf.Sin((Time.timeSinceLevelLoad * speed) + (characterindex * density)), 1);
            verts[i] = c;

        }


        return base.ProcessCharacters(verts, characterindex);
    }

    void Update()
    {
        GetComponent<Text>().SetVerticesDirty();
    }
}
