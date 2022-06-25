using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiview : MonoBehaviour
{
    public float distance = 10;
    public float angle = 30;
    public float height = 1.0f;
    public Color colormesh = Color.red;
    Mesh mesh;

    public float effectivesppoting;
    public bool armored;
    public bool hasat;


    Collider count;
    
    public int scanfreq = 2;
    float scaninterval;
    float scantimer;

    public LayerMask layers;
    public LayerMask occlusionlayer;
    public LayerMask teamcolor;

    Collider[] colliders = new Collider[50];
    GameObject pl;
    public bool insight = false;

    

    void Start()
    {
        scaninterval = 1.0f / scanfreq;
        pl = GameObject.FindGameObjectWithTag("Player");
    }
    

    // Update is called once per frame
    void Update()
    {
        scantimer += 1 * Time.deltaTime;
        if(scantimer >= scaninterval)
        {
          Scan();
            scantimer = 0;
        }
       
        
        
    }

    private void Scan()
    {
      
            
            if (Insight(pl))
            {
                insight = true;
                Debug.Log("VU");
            }
            else
            {
                insight = false;
            }
        
    }
    public bool Insight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dist = obj.transform.position;
        Vector3 direction = dist - origin;




        if (direction.y < -height || direction.y > height)
        {
            return false;
        }
        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);

        if (deltaAngle > angle)
        {
            return false;
        }

        if (Physics.Linecast(origin, dist, occlusionlayer ))
        {
            return false;
        }
     

        return true;
      
    }

    Mesh createwedgemesh()
    {
        Mesh mesh = new Mesh();
        int numtriangle = 8;
        int numvertices = numtriangle * 3;

        Vector3[] vertices = new Vector3[numvertices];
        int[] triangles = new int[numvertices];

        Vector3 bottomcenter = Vector3.zero;
        Vector3 bottomleft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomright = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topcenter = bottomcenter + Vector3.up * height;
        Vector3 topright = bottomright + Vector3.up * height;
        Vector3 topleft = bottomleft + Vector3.up * height;

        int verts = 0;
        //left side
        vertices[verts++] = bottomcenter;
        vertices[verts++] = bottomleft;
        vertices[verts++] = topleft;

        vertices[verts++] = topleft;
        vertices[verts++] = topcenter;   
        vertices[verts++] = bottomcenter;


        //rightside
        vertices[verts++] = bottomcenter;
        vertices[verts++] = topcenter;
        vertices[verts++] = topright;


        vertices[verts++] = topright;
        vertices[verts++] = bottomright;
        vertices[verts++] = bottomcenter;

        //farside
        vertices[verts++] = bottomleft;
        vertices[verts++] = bottomright;
        vertices[verts++] = topright;


        vertices[verts++] = topright;
        vertices[verts++] = topleft;
        vertices[verts++] = bottomleft;

        //top side
        vertices[verts++] = topcenter;
        vertices[verts++] = topleft;
        vertices[verts++] = topright;

         //bottom side
        vertices[verts++] = bottomcenter;
        vertices[verts++] = bottomright;
        vertices[verts++] = bottomleft;

        for(int i = 0;i < numvertices; ++i)
        {
            triangles[i] = i;
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }




        return mesh;
    }

    private void OnValidate()
    {
        mesh = createwedgemesh();
        scaninterval = 1.0f / scanfreq;

    }

    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = colormesh;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
        
      
    }
}
