
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum Dificultad { Facil, Normal, Dificil }
    public Dificultad dificultadActual = Dificultad.Normal;

    public float multiplicadorDificultad = 1f;

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

    public void SetDificultad(int nivelDificultad)
    {
        dificultadActual = (Dificultad)nivelDificultad;

        switch (dificultadActual)
        {
            case Dificultad.Facil:
                multiplicadorDificultad = 0.7f;
                break;
            case Dificultad.Normal:
                multiplicadorDificultad = 1f;
                break;
            case Dificultad.Dificil:
                multiplicadorDificultad = 1.5f;
                break;
        }
    }
}