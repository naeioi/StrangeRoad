using UnityEngine;
using System.Collections;

public class QuadCenterColor : MonoBehaviour
{

    public Material bgMat;
    public Color color0, color1, color2, color3, color4, color5, color6, color7, color8, color9, color10, color11;
    public float edge;
    public float f = 0.2f;

	
	
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
	
        Mesh mesh = new Mesh();
		
        Vector3[] newVertices = new Vector3[]
        {
            new Vector3(-0.5f, 0.5f, 0),// point 0
            new Vector3(0.5f, 0.5f, 0),// point 1
			
            new Vector3(0.5f, f, 0),// point 2
            new Vector3(0.5f, -f, 0),// point 3
			
            new Vector3(0.5f, -0.5f, 0),// point 4
            new Vector3(-0.5f, -0.5f, 0),// point 5
			
            new Vector3(-0.5f, -f, 0),// point 6
            new Vector3(-0.5f, f, 0),// point 7

            new Vector3(0.5f * edge, 0.5f, 0),// point 8
            new Vector3(0.5f * edge, f, 0),// point 9

            new Vector3(0.5f * edge, -f, 0),// point 10
            new Vector3(0.5f * edge, -0.5f, 0),// point 11
        };
		
        mesh.vertices = newVertices;
        mesh.triangles = new int[] 
            { 
                0, 8, 7, 
                7, 8, 9, 
                7, 9, 6, 
                6, 9, 10, 
                6, 10, 5, 
                5, 10, 11,

                8, 1, 9,
                9, 1, 2,
                9, 2, 10,
                10, 2, 3,
                10, 3, 11,
                11, 3, 4
            };
	
        Color[] colors = new Color[] { color0, color1, color2, color3, color4, color5, color6, color7, color8, color9, color10, color11};
		
		
        mesh.colors = colors;
		
		
        GetComponent<MeshRenderer>().material = bgMat;
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
