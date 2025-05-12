using UnityEngine;

public class AnimatedBackground : MonoBehaviour
{
    [SerializeField] private Vector2 moveDirection;
    private MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        mesh.material.mainTextureOffset += moveDirection * Time.deltaTime;
    }
}
