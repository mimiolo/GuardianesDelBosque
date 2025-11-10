// MenuPrincipal.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void IniciarJuego()
    {
        SceneManager.LoadScene("SeleccionPersonaje");
    }

    public void AbrirOpciones()
    {
        SceneManager.LoadScene("OpcionesDificultad");
    }

    public void SalirJuego()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}