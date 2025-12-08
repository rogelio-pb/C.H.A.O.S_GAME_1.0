using UnityEngine;
using UnityEngine.EventSystems; //  IMPORTANTE


[RequireComponent(typeof(Collider2D))]
public class NPCDialogue : MonoBehaviour
{
    [Header("Diálogo de este NPC")]
    public DialogueNode[] nodes;
    public int startNodeIndex = 0;

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        Debug.Log("[NPCDialogue] OnMouseDown en: " + name);

        if (DialogueUI.Instance == null)
        {
            Debug.LogWarning("[NPCDialogue] No hay DialogueUI en la escena.");
            return;
        }

        if (DialogueUI.Instance.IsOpen)
        {
            Debug.Log("[NPCDialogue] Ya hay un diálogo abierto, se ignora el click.");
            return;
        }

        StartDialogue();
    }

    public void StartDialogue()
    {
        if (DialogueUI.Instance == null)
        {
            Debug.LogWarning("[NPCDialogue] No hay DialogueUI en la escena.");
            return;
        }

        Debug.Log("[NPCDialogue] Iniciando diálogo con " + name);
        DialogueUI.Instance.ShowDialogue(this, startNodeIndex);
    }
}
