using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RorschachPiece : MonoBehaviour, IPointerClickHandler
{
    [Header("Datos de la pieza")]
    public int correctIndex;          // posición correcta (0..n-1)
    [HideInInspector] public int currentIndex; // posición actual en el grid
    [HideInInspector] public int rotationStep; // 0=0°,1=90°,2=180°,3=270°

    [Header("Referencias")]
    public Image image;               // imagen de la pieza
    public GameObject selectionFrame; // borde / highlight cuando está seleccionada

    private RorschachMiniGame manager;

    public void Init(RorschachMiniGame mgr, Sprite sprite, int indexCorrecto)
    {
        manager = mgr;
        correctIndex = indexCorrecto;

        if (image == null)
            image = GetComponent<Image>();

        image.sprite = sprite;

        rotationStep = 0;
        transform.localRotation = Quaternion.identity;
        SetSelected(false);
    }

    public void SetSelected(bool selected)
    {
        if (selectionFrame != null)
            selectionFrame.SetActive(selected);
    }

    public void Rotate90()
    {
        rotationStep = (rotationStep + 1) % 4;
        transform.localRotation = Quaternion.Euler(0f, 0f, rotationStep * 90f);
    }

    public bool IsCorrect()
    {
        return (currentIndex == correctIndex) && (rotationStep == 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (manager != null)
            manager.OnPieceClicked(this);
    }
}
