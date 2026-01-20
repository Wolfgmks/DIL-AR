using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ArPoints : MonoBehaviour
{
    //modelo 3D para reemplazar los puntos
    public GameObject pointPrefab;
    //gestores de ARpoints
    private ARPointCloud pointCloud;
    private List<GameObject> points = new List<GameObject>();

    private void Awake()
    {
        pointCloud = GetComponent<ARPointCloud>();
    }
    //se hailitan los puntos a generar
    private void OnEnable()
    {
        pointCloud.updated += OnPointCloudUpdate;
    }    
    //se actualizan y remueven los puntos despues de actualizar
    private void OnDisable()
    {
        pointCloud.updated -= OnPointCloudUpdate;   
    }

    private void OnPointCloudUpdate(ARPointCloudUpdatedEventArgs args)
    {
        //Limpia los puntos no visibles
        foreach (var p in points)
            Destroy(p);
        //creacion de puntos
        if (pointCloud.positions != null) {

            foreach (var pos in pointCloud.positions) { 
                var point = Instantiate(pointPrefab, transform);
                point.transform.localPosition = pos;
                points.Add(point);
            }
        
        }
    }

}
