using UnityEngine;

[System.Serializable]
public class QuestionData
{
    [TextArea(2, 4)]
    public string questionText;

    [Tooltip("Opciones de respuesta (texto de los botones)")]
    public string[] options;

    [Tooltip("Índice de la respuesta correcta dentro de 'options'")]
    public int correctIndex = 0;
}
