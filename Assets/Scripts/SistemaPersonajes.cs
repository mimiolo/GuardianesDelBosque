// SistemaPersonajes.cs
using UnityEngine;

public class SistemaPersonajes : MonoBehaviour
{
    public static SistemaPersonajes Instance;

    public int personajeSeleccionado = 0;
    public Material[] skinsDisponibles;

    // Referencias a los materiales del personaje
    public Renderer rendererPersonaje;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CambiarSkin(int indexSkin)
    {
        if (indexSkin >= 0 && indexSkin < skinsDisponibles.Length)
        {
            personajeSeleccionado = indexSkin;
            if (rendererPersonaje != null)
            {
                rendererPersonaje.material = skinsDisponibles[indexSkin];
            }
        }
    }
}