//Lirerias a utilizar llamando desde las librerias base de unity hasta las de XR que son para experiencias extendidas
using System.Collections.Generic;//libreria nativa
using UnityEngine;//lireria unity
using UnityEngine.XR.ARFoundation;//uso de reconocimientos
using UnityEngine.XR.ARSubsystems;//uso de subsistema de reconocimiento

public class multitrackmodels : MonoBehaviour
{
    //Variables 
    /*declaramos las variables empezando si son variables publicas o privadas 
     * despues declaramos el tipo 
     * asignamos nombre de la variable evitando utilizar espacios, se recomienda utilizar nombleclatura camel-case o buen guiones 
     * asignamos en el caso de que sea necesario el valor de dicha variable 
     * ejemplo:
     * public float decimalEjemplo = 10f;
     */
    //vamos a asingar el objeto que contiene imageReferenceLibrary
    [SerializeField] private ARTrackedImageManager ArTrackedImageManager;
    //asigna el modelo 3D pero crea un arreglo(conjunto) de modelos 3D permitiendo usar mas de uno
    [SerializeField] private GameObject[] models;
    //me permite crear una libreria de los objetos a utilizar o modelos 3D
    private Dictionary<string, GameObject> arModels = new Dictionary<string, GameObject>();
    //me define el estado del reconocimiento o tracking de la imagen ya sea activo o inactivo
    private Dictionary<string, bool> arState = new Dictionary<string, bool>();
    
    void Start()
    {
        //inicializa y compara los nombres de las imagenes de referencia con los modelos 3D para asignarlos
        foreach (var model in models) {
            GameObject newModel = Instantiate(model, Vector3.zero, Quaternion.identity);
            newModel.name = model.name;
            arModels.Add(model.name, newModel);
            newModel.SetActive(false);
            arState.Add(model.name, false);
        }
    }
    //en caso de que se detecte la imagen procedemos a activar 
    private void OnEnable()
    {
        ArTrackedImageManager.trackablesChanged.AddListener(ImageFound);
    }
    //en caso de que se detecte la imagen procedemos a desactivar 
    private void OnDisable()
    {
        ArTrackedImageManager.trackablesChanged.RemoveListener(ImageFound);
    }
    //si se encuentra la imagen procedemos a mostrar el modelo que sea igual a su posicion y nombre o caso contrario procedemos a ocultarlo
    private void ImageFound(ARTrackablesChangedEventArgs<ARTrackedImage> eventData)
    {
        foreach (var trackedImage in eventData.updated)
        {
            //caso de vista de vista de imagen llamamos modelo
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                ShowARModel(trackedImage);
            }
            //caso de vista de vista de imagen ocultamos modelo
            else if (trackedImage.trackingState == TrackingState.Limited)
            {
                HideARModel(trackedImage);
            }
        }
    }


    private void ShowARModel(ARTrackedImage trackedImage)
    {
        //esta funcion permite instanciar el modelo 3D al detectar la imagen
        bool isModelActivated = arState[trackedImage.referenceImage.name];
        if (!isModelActivated)
        {
            GameObject aRModel = arModels[trackedImage.referenceImage.name];
            aRModel.transform.position = trackedImage.transform.position;
            aRModel.SetActive(true);
            arState[trackedImage.referenceImage.name] = true;
        }
        else
        {
            GameObject aRModel = arModels[trackedImage.referenceImage.name];
            aRModel.transform.position = trackedImage.transform.position;
        }
    }

    private void HideARModel(ARTrackedImage trackedImage)
    {
        //esta funcion permite ocultar o desactivar el modelo 3D al perder la imagen de vista
        bool isModelActivated = arState[trackedImage.referenceImage.name];
        if (isModelActivated)
        {
            GameObject aRModel = arModels[trackedImage.referenceImage.name];
            aRModel.SetActive(false);
            arState[trackedImage.referenceImage.name] = false;
        }
    }
}
