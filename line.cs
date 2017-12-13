using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class line : MonoBehaviour
{
    public List<Vector3> v3 = new List<Vector3>();
    private Mesh mesh1;
    private RectTransform rectTransform;
    private float LineThickness;

    void Start()
    {
        LineThickness = DataSheet.DataSheet1.LineThickness;
        v3.Add(new Vector3(-20, -20, 0));
        v3.Add(new Vector3(-20, 20, 0));
        v3.Add(new Vector3(20, -20, 0));
        v3.Add(new Vector3(20, 20, 0));
        triangles = new int[2*3] {0, 3, 1, 0, 2, 3};
        rectTransform = GetComponent<RectTransform>();
        Debug.Log(LineThickness);
    }

    private int[] triangles;

    void Update()
    {
        var y1 = rectTransform.localPosition.y + rectTransform.rect.height/2;
        var y2 = rectTransform.localPosition.y - rectTransform.rect.height/2;
        var x1 = rectTransform.localPosition.x + rectTransform.rect.width/2;
        var x2 = rectTransform.localPosition.x - rectTransform.rect.width/2;

        v3[2] = new Vector3(x1 + LineThickness, y1 - LineThickness*0.3f);
        v3[3] = new Vector3(x1, y1);

        v3[0] = new Vector3(x2 + LineThickness, y2 - LineThickness*0.3f);
        v3[1] = new Vector3(x2, y2);

        mesh1 = new Mesh();

        mesh1.SetVertices(v3);
        mesh1.triangles = triangles;

        var mesh = GetComponent<CanvasRenderer>();
        mesh.SetMesh(mesh1);
    }
}