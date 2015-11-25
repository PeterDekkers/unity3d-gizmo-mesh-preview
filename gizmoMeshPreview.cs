using UnityEngine;
using System.Collections;

/*
 * This class is an example of how to draw nested gizmo meshes for a prefab.
 */
public class gizmoMeshPreview : MonoBehaviour
{
	
	// The prefab that we want to draw gizmo meshes for
	public GameObject prefab;
	
	#if UNITY_EDITOR
	// You can turn gizmo mesh rendering off via the Inspector, if you want
	public bool showGizmoMesh = true;
	
	// This gets set to 'true' once there are meshes cached.
	// If you need to redraw the gizmo meshes (e.g. when your prefab changes)
	// you can simply toggle this checkbox in the inspector and they
	// will instantly update.
	public bool gizmoMeshesCached = false;
	
	// We'll cache the meshes that we want to draw gizmos for.
	public MeshFilter[] gizmoMeshes = new MeshFilter[0];
	
	// Cache transforms of all the meshes to draw gizmos for
	private Transform[] gizmoMeshTransforms;
	#endif
	
	void OnDrawGizmos ()
	{
		
		if (this.showGizmoMesh == false || this.prefab == null) {
			return;
		}
		
		// Fetch meshes inside the prefab once and cache them
		// and their transforms.
       		if (!this.gizmoMeshesCached) {
			this.gizmoMeshes = this.prefab.GetComponentsInChildren<MeshFilter>(true);
			this.gizmoMeshTransforms = new Transform[this.gizmoMeshes.Length];
			for (int i = 0; i < this.gizmoMeshes.Length; i++) {
				this.gizmoMeshTransforms [i] = this.gizmoMeshes [i].GetComponent<Transform> ();
			}
			if (this.gizmoMeshes.Length > 0) {
            			this.gizmoMeshesCached = true;
			}
        	}
		
		// If there are meshes in the array, draw a gizmo mesh for each
		if (this.gizmoMeshesCached) {
			
			for (int i = 0; i < this.gizmoMeshes.Length; i++) {
				
				// Attempt to get a vertex color for the gizmo
				if (this.gizmoMeshes [i].sharedMesh.colors.Length >= 1) {
					Gizmos.color = this.gizmoMeshes [i].sharedMesh.colors [0];
				} else {
					// Default to gray
					Gizmos.color = Color.gray;
				}
				
				// Adjust the position and rotation of the gizmo mesh
				Vector3 pos = transform.TransformPoint (this.gizmoMeshTransforms [i].position);
				Quaternion rot = transform.rotation * this.gizmoMeshTransforms [i].rotation;
				
				// Display the gizmo mesh
				Gizmos.DrawMesh (this.gizmoMeshes [i].sharedMesh, pos, rot, Vector3.one);
			}
			
		} else {
			
			// As a fallback just display a yellow gizmo sphere
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere (transform.position, 1);
			
		}
		
	}
	
}
