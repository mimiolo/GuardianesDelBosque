using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Referencias")]
    public AudioSource musicaFondo;
    public Slider sliderVolumen;

    private void Awake()
    {
        // Evitar duplicados de AudioManager
        var otrosManagers = FindObjectsOfType<AudioManager>();
        if (otrosManagers.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        // Mantener este objeto al cambiar de escena
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Cargar el volumen guardado (o poner uno por defecto)
        float volumenGuardado = PlayerPrefs.GetFloat("volumenMusica", 0.5f);
        musicaFondo.volume = volumenGuardado;

        // Si hay un slider, sincronizarlo con el volumen
        if (sliderVolumen != null)
            sliderVolumen.value = volumenGuardado;
    }

    // Llamada desde el slider del menú de opciones
    public void CambiarVolumen(float nuevoVolumen)
    {
        musicaFondo.volume = nuevoVolumen;
        PlayerPrefs.SetFloat("volumenMusica", nuevoVolumen);
    }
}
