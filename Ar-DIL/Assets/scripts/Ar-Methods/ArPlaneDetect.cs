using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ArPlaneDetect : MonoBehaviour
{
    //detecta el manager de plane por default
    [SerializeField] private ARPlaneManager _arPlane;
    [SerializeField] private GameObject model;
    //genera una lista de posibles objectos a colocar
    private List<ARPlane> planos = new List<ARPlane>();
    private GameObject modelPlace;
    //suscribir o activar listener
    private void OnEnable()
    {
        _arPlane.trackablesChanged.AddListener(PlanesFound);
    }    
    //desuscribe o desactiva el listener
    private void OnDisable() { 
        _arPlane.trackablesChanged.RemoveListener(PlanesFound);
    }
    //logica de cuando detecta los planos caso horizontal y/o vertical
    private void PlanesFound(ARTrackablesChangedEventArgs<ARPlane> planeData)
    {
        if (planeData.added != null && planeData.added.Count > 0) {         
            planos.AddRange(planeData.added);        
        }
        //se genera la instancia a la deteccion de planos y para evitar redundancias se llama a stpoPlane
        foreach (var plane in planos) { 
            modelPlace = Instantiate(model);
            float YOffSet = modelPlace.transform.localScale.y / 2;
            modelPlace.transform.position = new Vector3(plane.center.x, plane.center.y + YOffSet, plane.center.z);
            modelPlace.transform.forward = plane.normal;
            StopPlaneDetect();
        }
    }
    //Si no detecta planos entonces desactiva los prefabs
    private void StopPlaneDetect()
    {
        _arPlane.requestedDetectionMode = PlaneDetectionMode.None;
        foreach (var plane in planos) {         
            plane.gameObject.SetActive(false);
        }
    }
}
