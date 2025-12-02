using UnityEngine;

public class ParanoiaTV2D : MonoBehaviour
{
    [Header("Referencia del sprite")]
    public SpriteRenderer spriteRenderer;     // arrastra el SpriteRenderer de la TV

    [Header("Sprites de la TV")]
    public Sprite tvOffSprite;               // sprite de la TV apagada
    public Sprite[] tvAlteredSprites;        // sprites de la TV alterada (cambia cada 2s)

    [Header("Paranoia")]
    [Tooltip("Si la paranoia es mayor o igual a este valor, se muestra la TV alterada.")]
    public float paranoiaThreshold = 15f;

    [Header("Animación alterada")]
    public float changeInterval = 2f;        // cambia de sprite cada 2 segundos

    private float _timer = 0f;
    private int _currentIndex = 0;
    private bool _isAlteredVisual = false;

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float paranoia = GetParanoiaPercent();

        bool debeEstarAlterada = paranoia >= paranoiaThreshold;

        if (!debeEstarAlterada)
        {
            // Modo apagado
            _isAlteredVisual = false;
            _timer = 0f;
            _currentIndex = 0;

            if (tvOffSprite != null && spriteRenderer.sprite != tvOffSprite)
                spriteRenderer.sprite = tvOffSprite;

            return;
        }

        // Modo alterado
        if (!_isAlteredVisual)
        {
            // Cambiamos a primer sprite alterado al cruzar el umbral
            _isAlteredVisual = true;
            _timer = 0f;
            _currentIndex = 0;

            if (tvAlteredSprites != null && tvAlteredSprites.Length > 0)
                spriteRenderer.sprite = tvAlteredSprites[_currentIndex];
        }

        // Animar cambio de contenido cada 2 segundos
        if (tvAlteredSprites == null || tvAlteredSprites.Length == 0)
            return;

        _timer += Time.deltaTime;
        if (_timer >= changeInterval)
        {
            _timer = 0f;
            _currentIndex = (_currentIndex + 1) % tvAlteredSprites.Length;
            spriteRenderer.sprite = tvAlteredSprites[_currentIndex];
        }
    }

    private float GetParanoiaPercent()
    {
        if (ParanoiaManager.Instance == null)
            return 0f;

        // 🔴 OJO: cambia "currentParanoiaPercent" por el nombre REAL de tu variable
        return ParanoiaManager.Instance.currentParanoiaPercent;
        // p.ej. .paranoiaPercent, .CurrentParanoia, etc.
    }
}
