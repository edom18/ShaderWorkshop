using System;
using UnityEngine;

public class TriangleStudy : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private float _unit = 0.5f;
    [SerializeField] private Transform _target;

    private Mesh _mesh;
    
    private Vector3[] _vertices = new Vector3[3];
    private Vector2[] _uv = new Vector2[3];
    private int _index = 0;

    private void OnValidate()
    {
        if (!Application.isPlaying) return;

        RemakeMesh();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;
        
        Debug.Log(hit.point);

        AddPosition(hit.point, hit.textureCoord);
    }

    private void AddPosition(Vector3 position, Vector2 uv)
    {
        if (_index >= _vertices.Length)
        {
            _index = 0;
            _meshFilter.mesh = null;
        }

        _vertices[_index] = position - _target.forward * 0.0001f;
        _uv[_index] = uv;
        _index++;

        if (_index == _vertices.Length)
        {
            RemakeMesh();
        }
    }

    private void RemakeMesh()
    {
        _mesh = new Mesh();
        _mesh.vertices = _vertices;
        _mesh.uv = _uv;
        _mesh.SetIndices(new int[] { 0, 1, 2, }, MeshTopology.Triangles, 0);
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
        _mesh.RecalculateTangents();

        _meshFilter.mesh = _mesh;
    }
}