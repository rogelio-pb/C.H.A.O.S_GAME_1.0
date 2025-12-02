using UnityEngine;

public class ParanoiaClock2D : MonoBehaviour
{
    [Header("Referencia del sprite")]
    public SpriteRenderer spriteRenderer;   // arrastra el SpriteRenderer del reloj

    [Header("Sprites del reloj")]
    public Sprite normalSprite;            // reloj en estado normal (<15%)
    public Sprite alteredSprite;           // reloj en estado alterado (>=15%)

    [Header("Paranoia")]
    public float paranoiaThreshold = 15f;

    private bool _isAlteredVisual = false;

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float paranoia = GetParanoiaPercent();

        bool debeEstarAlterado = paranoia >= paranoiaThreshold;

        if (debeEstarAlterado == _isAlteredVisual)
            return; // nada cambió, no hace falta actualizar sprite

        _isAlteredVisual = debeEstarAlterado;

        if (_isAlteredVisual)
        {
            if (alteredSprite != null)
                spriteRenderer.sprite = alteredSprite;
        }
        else
        {
            if (normalSprite != null)
                spriteRenderer.sprite = normalSprite;
        }
    }

    private float GetParanoiaPercent()
    {
        if (ParanoiaManager.Instance == null)
            return 0f;

        // 🔴 Cambia "currentParanoiaPercent" por el nombre real de tu variable
        return ParanoiaManager.Instance.currentParanoiaPercent;
    }
}
