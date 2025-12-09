using UnityEngine;
using UnityEngine.EventSystems;

public static class TapBlocker
{
    public static bool IsTouchOverUI()
    {
        // Para mouse y editor
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        // Para móviles (toques)
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return true;
        }

        return false;
    }
}
