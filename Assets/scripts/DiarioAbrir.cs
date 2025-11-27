using UnityEngine;

public class AbreDiarioUI : MonoBehaviour
{
    public DiarioSecretoUI DiarioUI;  // arrastras aquí el panelRevistas

    void OnMouseDown()
    {
        if (DiarioUI != null)
        {
            DiarioUI.AbrirDiario();
        }
        else
        {
            Debug.LogError("RevistasUI no está asignado en el inspector");
        }
    }
}
