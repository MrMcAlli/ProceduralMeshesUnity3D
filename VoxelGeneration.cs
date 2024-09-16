using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelGeneration : MonoBehaviour
{
    public GameObject worldCubePrefab;
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    public int sizeX = 50;
    public int sizeZ = 50;
    public int noiseHeight = 4;
    public float scale = 8f;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        vertices = new Vector3[sizeX * sizeZ];
    }

    void Update()
    {
        UpdateMesh();
        CreateVerticies();
    }

    void CreateVerticies()
    {
        vertices = new Vector3[(sizeX + 1) * (sizeZ + 1)];

        for (int i = 0, z =0; z<= sizeZ; z++)
        {
            for (int x = 0; x<=sizeX; x++)
            {
                float y = GenerateNoise(x,z,scale) * noiseHeight;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[sizeX * sizeZ * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < sizeZ; z++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + sizeX + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + sizeX + 1;
                triangles[tris + 5] = vert + sizeX + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }       
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private float GenerateNoise(int x, int z, float noiseScale)
    {
        float xNoise = (x + this.transform.position.x) / noiseScale;
        float zNoise = (z + this.transform.position.y) / noiseScale;

        return Mathf.PerlinNoise(xNoise, zNoise);
    }
}