using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStudy : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;
    
    private Mesh _mesh;
    
    private void Start()
    {
        float u = 0.5f;
        
        _mesh = new Mesh();
        _mesh.vertices = new Vector3[]
        {
            // Front
            new Vector3(-u, -u, -u),
            new Vector3(-u, u, -u),
            new Vector3(u, u, -u),
            new Vector3(u, -u, -u),
            
            // Back
            new Vector3(u, -u, u),
            new Vector3(u, u, u),
            new Vector3(-u, u, u),
            new Vector3(-u, -u, u),
            
            // Left
            new Vector3(-u, -u, u),
            new Vector3(-u, u, u),
            new Vector3(-u, u, -u),
            new Vector3(-u, -u, -u),
            
            // Right
            new Vector3(u, -u, -u),
            new Vector3(u, u, -u),
            new Vector3(u, u, u),
            new Vector3(u, -u, u),
            
            // Top
            new Vector3(-u, u, -u),
            new Vector3(-u, u, u),
            new Vector3(u, u, u),
            new Vector3(u, u, -u),
            
            // Bottom
            new Vector3(-u, -u, -u),
            new Vector3(-u, -u, u),
            new Vector3(u, -u, u),
            new Vector3(u, -u, -u),
        };

        _mesh.SetIndices(new int[]
        {
            // Front
            0, 1, 3,
            3, 1, 2,
            
            // Back
            4, 5, 7,
            7, 5, 6,
            
            // Left
            8, 9, 11,
            11, 9, 10,
            
            // Right
            12, 13, 15,
            15, 13, 14,
            
            // Top
            16, 17, 19,
            19, 17, 18,
            
            // Bottom
            20, 23, 21,
            23, 22, 21,
        }, MeshTopology.Triangles, 0);
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
        _mesh.RecalculateTangents();

        _meshFilter.mesh = _mesh;
    }

    private void Update()
    {
        
    }
}