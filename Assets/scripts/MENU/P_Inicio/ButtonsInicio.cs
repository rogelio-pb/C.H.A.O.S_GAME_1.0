using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Paneles del Menú")]
    public GameObject panelPrincipal;
    public GameObject panelMenuPrincipal;
    public GameObject panelAjustes;
    public GameObject panelCreditos;
    public GameObject panelArchivo;
    void Start()
    {
        // Al iniciar, solo se muestra la pantalla principal
        panelPrincipal.SetActive(true);
        panelMenuPrincipal.SetActive(false);
        panelAjustes.SetActive(false);
        panelCreditos.SetActive(false);
        panelArchivo.SetActive(false);
    }

    //  Botón "Iniciar"
    public void AbrirMenu()
    {
        panelPrincipal.SetActive(false);
        panelMenuPrincipal.SetActive(true);
        panelAjustes.SetActive(false);
        panelCreditos.SetActive(false);
        panelArchivo.SetActive(false);
    }

    //  Botón "Ajustes"
    public void AbrirAjustes()
    {
        panelPrincipal.SetActive(false);
        panelMenuPrincipal.SetActive(false);
        panelAjustes.SetActive(true);
        panelCreditos.SetActive(false);
        panelArchivo.SetActive(false);
    }

    //  Botón "Créditos"
    public void AbrirCreditos()
    {
        panelPrincipal.SetActive(false);
        panelMenuPrincipal.SetActive(false);
        panelAjustes.SetActive(false);
        panelCreditos.SetActive(true);
        panelArchivo.SetActive(false);
    }

    //  Botón "Archivo"
    public void AbrirArchivo()
    {
        panelPrincipal.SetActive(false);
        panelMenuPrincipal.SetActive(false);
        panelAjustes.SetActive(false);
        panelCreditos.SetActive(false);
        panelArchivo.SetActive(true);
    }
    public void IrAlJuego()
    {
        Debug.Log("Cargando escena de juego...");
        SceneManager.LoadScene(2); // Carga la escena con índice 2
    }

    //  Botón "Volver"
    public void VolverAlMenu()
    {
        panelPrincipal.SetActive(false);
        panelMenuPrincipal.SetActive(true);
        panelAjustes.SetActive(false);
        panelCreditos.SetActive(false);
        panelArchivo.SetActive(false);
    }
    
}
