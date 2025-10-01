using UnityEngine;
using UnityEngine.UI;
using TMPro; // si usas TextMeshPro

public class InteraccionObjeto : MonoBehaviour
{
    [Header("UI de interacción")]
    public GameObject panelUI; // arrastra aquí tu panel o imagen desde el Canvas
    private bool isPanelActive = false; // para rastrear el estado del panel
    private void OnMouseDown()
    {
        if (!isPanelActive)
        {
            panelUI.SetActive(true); // muestra el panel
            isPanelActive = true;// actualiza el estado
        }
        else
        {
            panelUI.SetActive(false); // oculta el panel
            isPanelActive = false;// actualiza el estado
        }

    }
}
