using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PointDensity : MonoBehaviour
{
    [SerializeField] private ARPointCloudManager pointCloudManager;
    [SerializeField] private float radio = 0.2f;
    [SerializeField] private int density = 50;

    private List<Vector3> cachePoints = new List<Vector3>();


    void Update()
    {
        UpdatePointerCheck();
        CheckDensity();
        
    }

    private void CheckDensity()
    {
        Vector3 center = transform.position;
        int count = 0;

        foreach (var p in cachePoints) {

            if (Vector3.Distance(center, p) <= radio)
                count++;        
        }

        Debug.Log($"Densidad actual es: {count}");

        if (count >= density)
        {
            Debug.Log("Suficiente densidad todo Ok");
        }
        else {

            Debug.Log("Falta densidad");
        }
        
    }

    private void UpdatePointerCheck()
    {
        //limpia puntos no visibles
        cachePoints.Clear();
        //crea puntos nuevos
        foreach (var pointCloud in pointCloudManager.trackables) {

            if (pointCloud.positions == null) continue;
            foreach (var position in pointCloud.positions)
                cachePoints.Add(position);        
        }
    }
}
