using UnityEngine;

public class CheckpointSillas : MonoBehaviour
{
    private bool checkpointGuardado = false;

    private void OnMouseDown()
    {
        GuardarCheckpoint();
    }



    void GuardarCheckpoint()
    {
        if (checkpointGuardado) return; // Para no guardarlo dos veces

        checkpointGuardado = true;

        PlayerPrefs.SetInt("checkpoint_activado", 1);
      

        PlayerPrefs.Save();

        Debug.Log("CHECKPOINT ACTIVADO EN SILLAS");
    }
}
