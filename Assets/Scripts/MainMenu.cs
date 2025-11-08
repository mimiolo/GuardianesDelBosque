using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public GameObject mainMenu;     // Menú principal
    public GameObject optionsMenu;  // Menú de opciones
    public GameObject levelMenu;    // Menú de selección de niveles

    
    public void OpenOptionsPanel()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    
    public void OpenMainMenuPanel()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        levelMenu.SetActive(false);
    }

    
    public void OpenLevelMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

        
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}
