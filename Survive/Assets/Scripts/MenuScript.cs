using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{   
   public void LoadGame()
    {
        SceneManager.LoadScene(1); 
    }
    public void ExiGame()
    {
        Application.Quit();
    }
}