using UnityEngine;

public class executeUrl : MonoBehaviour
{
    [SerializeField]private So_Web url;
    //abre un sitio web almacenado en una cadena de texto
    public void Execute() {
        Application.OpenURL(url.URL);    
    }
}
