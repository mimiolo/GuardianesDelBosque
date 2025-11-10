
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControladorDificultad : MonoBehaviour
{
    public TMP_Dropdown dropdownDificultad;

    private void Start()
    {
        // Cargar dificultad guardada
        dropdownDificultad.value = (int)GameManager.Instance.dificultadActual;
    }

    public void CambiarDificultad(int opcion)
    {
        GameManager.Instance.SetDificultad(opcion);
    }

    public void VolverMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}