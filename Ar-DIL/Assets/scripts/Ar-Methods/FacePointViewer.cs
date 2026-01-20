using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FacePointViewer : MonoBehaviour
{
    [SerializeField] private ARFaceManager _arFace;
    [SerializeField] private GameObject point;
    private List<GameObject> markers = new List<GameObject>();

    private void OnEnable()
    {
        _arFace.trackablesChanged.AddListener(FacesMarker);
    }


    private void OnDisable()
    {
        _arFace.trackablesChanged.RemoveListener(FacesMarker);
    }
    //va a detectar la cara e inicializar los puntos a colocar
    private void FacesMarker(ARTrackablesChangedEventArgs<ARFace> faceData)
    {
        foreach (var face in faceData.added) {

            var renderer = face.GetComponent<MeshRenderer>();
            if(renderer) renderer.enabled = false;

            var filter = face.GetComponent<MeshFilter>();
            if (filter) filter.mesh = null;

            UpdateMarkers(face);        
        }
        foreach (var face in faceData.updated) { 
            UpdateMarkers(face);
        }
    }
    //genera y coloca los puntos en la cara como si fueran marcadores en lugar de usar un facemask
    private void UpdateMarkers(ARFace face)
    {
        var mesh = face.GetComponent<ARFaceMeshVisualizer>().mesh;
        if(mesh == null) return;

        var vertices = mesh.vertices;

        while (markers.Count < vertices.Length) {

            var meshFace = Instantiate(point, face.transform);
            markers.Add(meshFace);        
        }

        for (int i = 0; i < vertices.Length; i++) {
            markers[i].transform.localPosition = vertices[i];
        }
    }
}
