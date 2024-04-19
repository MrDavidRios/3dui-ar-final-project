using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

using UnityEngine;

public class ExportPrefabToOBJ : MonoBehaviour
{
    [Tooltip("Drag the prefab or GameObject you want to export into this field.")]
    public GameObject objectToExport;

    // This is the path where the exported file will be saved.
    // You might want to expose this in the inspector or set it through a method or variable for flexibility.
    private string exportPath = "Assets/Exports/";

    public void ExportToObj()
    {
        if (objectToExport == null)
        {
            Debug.LogError("ExportPrefabToOBJ: No object assigned to export!");
            return;
        }

        if (!Directory.Exists(exportPath))
        {
            Directory.CreateDirectory(exportPath);
        }

        MeshFilter[] meshFilters = objectToExport.GetComponentsInChildren<MeshFilter>();
        StringBuilder sb = new StringBuilder();

        foreach (MeshFilter meshFilter in meshFilters)
        {
            Mesh mesh = meshFilter.sharedMesh;
            Vector3 transformPosition = meshFilter.transform.position;
            Quaternion transformRotation = meshFilter.transform.rotation;

            // Write vertices
            foreach (Vector3 vertex in mesh.vertices)
            {
                Vector3 transformedVertex = transformRotation * vertex + transformPosition;
                sb.AppendLine($"v {transformedVertex.x} {transformedVertex.y} {transformedVertex.z}");
            }

            // Write UVs
            foreach (Vector3 uv in mesh.uv)
            {
                sb.AppendLine($"vt {uv.x} {uv.y}");
            }

            // Write normals
            foreach (Vector3 normal in mesh.normals)
            {
                Vector3 transformedNormal = transformRotation * normal;
                sb.AppendLine($"vn {transformedNormal.x} {transformedNormal.y} {transformedNormal.z}");
            }

            // Write faces
            int offset = 0;
            for (int i = 0; i < mesh.triangles.Length; i += 3)
            {
                int index1 = mesh.triangles[i] + 1 + offset;
                int index2 = mesh.triangles[i + 1] + 1 + offset;
                int index3 = mesh.triangles[i + 2] + 1 + offset;
                sb.AppendLine($"f {index1}/{index1}/{index1} {index2}/{index2}/{index2} {index3}/{index3}/{index3}");
            }
            offset += mesh.vertices.Length;
        }

        string finalPath = Path.Combine(exportPath, objectToExport.name + ".obj");
        File.WriteAllText(finalPath, sb.ToString());
        Debug.Log($"Exported {objectToExport.name} to {finalPath}");
    }
}
