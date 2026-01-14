using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ArFaceDetect : MonoBehaviour
{
    //detecta el manager por default de la cara
    [SerializeField] private ARFaceManager _arFace;
    [SerializeField] private GameObject model;
    //nos indica si se ha detectado y colocado algo en la cara
    private bool facePlace = false;
    private ARFace detectedFace;

    private void OnEnable()
    {
        _arFace.trackablesChanged.AddListener(FacesFound);
    }


    private void OnDisable()
    {
        _arFace.trackablesChanged.RemoveListener(FacesFound);
    }
    private void FacesFound(ARTrackablesChangedEventArgs<ARFace> faceData)
    {
        //si ya existe algo en la cara salimos de los procesos
        if (facePlace)
            return;
        //si se encutra una cara procedemos a ejecutar
        if (faceData.added != null && faceData.added.Count > 0) { 
        
            detectedFace = faceData.added[0];
            GameObject faceObj = Instantiate(model);
            //emparenta como hijo al prefab
            faceObj.transform.SetParent(detectedFace.transform);
            //posiciona el prefab
            faceObj.transform.localPosition = Vector3.zero;
            //ajusta rotacion del prefab
            faceObj.transform.localRotation = Quaternion.identity;
            StopFaceDetect();        
        }
    }
    //si no detecta la cara entonces desactiva los prefabs
    private void StopFaceDetect()
    {
        //_arFace.enabled = false;
        foreach (var face in _arFace.trackables) {

            if (face != detectedFace) { 
            
                face.gameObject.SetActive(false);
            
            }
        }
    }
}
