using UnityEngine;

[System.Serializable]
public class QuestionData
{
    [TextArea(2, 4)]
    public string questionText;  // Texto de la pregunta

    [Tooltip("Opciones de respuesta, por ejemplo 3 o 4")]
    public string[] options;     // Respuestas

    [Tooltip("Índice de la respuesta correcta dentro del arreglo 'options'")]
    public int correctIndex;     // 0, 1, 2...
}
