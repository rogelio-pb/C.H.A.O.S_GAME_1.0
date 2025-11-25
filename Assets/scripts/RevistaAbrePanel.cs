using UnityEngine;

public class RevistaAbrePanel : MonoBehaviour
{
    public RevistasUI revistasUI;  // arrastras aquí el panelRevistas

    void OnMouseDown()
    {
        if (revistasUI != null)
        {
            revistasUI.AbrirRevista();
        }
        else
        {
            Debug.LogError("RevistasUI no está asignado en el inspector");
        }
    }
}
