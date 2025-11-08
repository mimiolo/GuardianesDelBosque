using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Aquí defines los nombres exactos de tus escenas
    public string nivelFacil = "EscenaFacil";
    public string nivelIntermedio = "EscenaIntermedio";
    public string nivelDificil = "EscenaDificil";

    public void CargarNivelFacil()
    {
        SceneManager.LoadScene(nivelFacil);
    }

    public void CargarNivelIntermedio()
    {
        SceneManager.LoadScene(nivelIntermedio);
    }

    public void CargarNivelDificil()
    {
        SceneManager.LoadScene(nivelDificil);
    }
}
