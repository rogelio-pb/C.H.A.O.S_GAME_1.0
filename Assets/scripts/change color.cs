using UnityEngine;

public class changecolor : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del objeto
    public Color color1 = Color.blue; // Primer color
    public Color color2 = Color.red; // Segundo color
    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    
    }
    private void OnMouseDown()
    {
            // Cambia el color
            if (spriteRenderer.color == color1)
            {
                spriteRenderer.color = color2;
            }
            else
            {
                spriteRenderer.color = color1;
            }
        }
    }


