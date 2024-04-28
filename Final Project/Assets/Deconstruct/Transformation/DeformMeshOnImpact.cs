using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class DeformMeshOnImpact : MonoBehaviour
{
    Mesh deformingMesh;
    Vector3[] originalVertices, displacedVertices;
    float toughness = 5.0f;
    float deformationScale = 0.1f; // Reduce deformation effect

    void Start()
    {
        deformingMesh = GetComponent<MeshFilter>().mesh;
        originalVertices = deformingMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++)
        {
            displacedVertices[i] = originalVertices[i];
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 pointOfImpact = transform.InverseTransformPoint(contact.point);
        float force = collision.relativeVelocity.magnitude * collision.rigidbody.mass;
        ApplyDeformation(pointOfImpact, force);
    }

    void ApplyDeformation(Vector3 pointOfImpact, float force)
    {
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            Vector3 pointToVertex = displacedVertices[i] - pointOfImpact;
            float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
            float deformation = attenuatedForce / toughness;
            // Ensuring deformation is inward by reversing the direction
            displacedVertices[i] -= pointToVertex.normalized * deformation * deformationScale;
        }
        deformingMesh.vertices = displacedVertices;
        deformingMesh.RecalculateNormals();
    }
}
