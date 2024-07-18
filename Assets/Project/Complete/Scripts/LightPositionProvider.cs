using UnityEngine;

public class LightPositionProvider : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Transform _lightTransform;

    private Material _material;
    
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
    }

    private void Update()
    {
        _material.SetVector("_LightPosition", _lightTransform.position - _renderer.transform.position);
    }
}
