using UnityEngine;
using UnityEngine.SceneManagement;

public class lvlManager : MonoBehaviour
{
    public void LevelManagerUi(int index) {     
        SceneManager.LoadScene(index);    
    }
}