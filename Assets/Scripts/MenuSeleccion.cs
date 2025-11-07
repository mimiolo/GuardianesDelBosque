// MenuSeleccionPersonaje.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSeleccion : MonoBehaviour
{
    public Image previewPersonaje;
    public Sprite[] previewsSkins;
    public TMP_Text textoSkinActual;

    private int skinActual = 0;

    private void Start()
    {
        ActualizarPreview();
    }

    public void SiguienteSkin()
    {
        skinActual = (skinActual + 1) % previewsSkins.Length;
        ActualizarPreview();
    }

    public void AnteriorSkin()
    {
        skinActual--;
        if (skinActual < 0) skinActual = previewsSkins.Length - 1;
        ActualizarPreview();
    }

    private void ActualizarPreview()
    {
        previewPersonaje.sprite = previewsSkins[skinActual];
        textoSkinActual.text = "Skin " + (skinActual + 1);
    }

    public void ConfirmarSeleccion()
    {
        SistemaPersonajes.Instance.CambiarSkin(skinActual);
        SceneManager.LoadScene("NivelJuego");
    }

    public void VolverMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}