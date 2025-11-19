using UnityEngine;

/// <summary>
/// Opción de diálogo que elige el jugador.
/// </summary>
[System.Serializable]
public class DialogueOption
{
    [TextArea(1, 2)]
    public string optionText;          // Texto que aparece en el botón

    [Tooltip("Índice del siguiente nodo. Usa -1 para terminar el diálogo.")]
    public int nextNodeIndex = -1;

    [Tooltip("Cuánto aumenta/disminuye la paranoia al elegir esta opción.")]
    public float paranoiaDelta = 0f;   // Puede ser positivo, negativo o 0
}

/// <summary>
/// Nodo de diálogo que muestra el texto del NPC y sus opciones.
/// </summary>
[System.Serializable]
public class DialogueNode
{
    [TextArea(2, 4)]
    public string text;                // Texto que dice el NPC

    [Tooltip("Opciones que el jugador puede elegir desde este nodo.")]
    public DialogueOption[] options;
}
