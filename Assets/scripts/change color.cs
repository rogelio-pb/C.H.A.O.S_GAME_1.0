using UnityEngine;

public class changecolor : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del objeto
    public Color color1 = Color.red; // Primer color
    public Color color2 = Color.blue; // Segundo color
    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
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

}
