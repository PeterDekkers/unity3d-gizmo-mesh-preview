using UnityEngine;
using System.Collections;

public class gizmoMeshPreview : MonoBehaviour {

    public GameObject prefab;
    #if UNITY_EDITOR
    public MeshFilter[] gizmoMeshes = new MeshFilter[0];
    public bool showGizmoMesh = true;
    private Transform[] gizmoMeshTransforms;
    #endif

    void OnDrawGizmos() {

        if (this.showGizmoMesh == false || this.prefab == null) {
            return;
        }

        // Fetch them once...
        if (this.gizmoMeshes.Length == 0) {
            this.gizmoMeshes = this.prefab.GetComponentsInChildren<MeshFilter>(true);
            this.gizmoMeshTransforms = new Transform[this.gizmoMeshes.Length];
            for (int i = 0; i < this.gizmoMeshes.Length; i++) {
                this.gizmoMeshTransforms[i] = this.gizmoMeshes[i].GetComponent<Transform>();
            }
        }

        if (this.gizmoMeshes.Length > 0) {

            // Draw ALL OF THE MESH
            for (int i = 0; i < this.gizmoMeshes.Length; i++) {
                if (this.gizmoMeshes[i].sharedMesh.colors.Length >= 1) {
                    Gizmos.color = this.gizmoMeshes[i].sharedMesh.colors[0];
                } else {
                    Gizmos.color = Color.gray;
                }
                Vector3 pos = transform.TransformPoint(this.gizmoMeshTransforms[i].position);
                Quaternion rot = transform.rotation * this.gizmoMeshTransforms[i].rotation;
                Gizmos.DrawMesh(this.gizmoMeshes[i].sharedMesh, pos, rot, Vector3.one);
            }

        } else {

            // Meh, fallback display.
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 1);

        }

    }

}
