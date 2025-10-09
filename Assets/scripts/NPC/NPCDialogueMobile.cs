using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCDialogueMobile : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject dialoguePanel;         // panel con el texto
    [SerializeField] TextMeshProUGUI dialogueText;     // texto principal
    [SerializeField] TextMeshProUGUI nameText;         // opcional
    [SerializeField] Button continueButton;            // toca para continuar
    [SerializeField] Button interactButton;            // botón flotante "Hablar"

    [Header("Contenido")]
    [SerializeField] string npcName = "NPC";
    [TextArea(2, 5)] public string[] lines;
    [SerializeField, Range(0.001f, 0.1f)] float typeSpeed = 0.02f;

    [Header("Restricciones")]
    [SerializeField] float tapStartDistance = 3.0f;    // distancia máx. para iniciar con tap al NPC
    [SerializeField] Transform player;                 // referencia al jugador

    bool playerInside = false;
    bool isTyping = false;
    int index = 0;
    Coroutine typingCR;

    void Awake()
    {
        if (dialoguePanel) dialoguePanel.SetActive(false);
        if (interactButton) interactButton.gameObject.SetActive(false);
        if (nameText) nameText.text = npcName;

        // Listeners táctiles
        if (continueButton)
        {
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(OnContinueTapped);
        }
        if (interactButton)
        {
            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(OnInteractTapped);
        }
    }

    // ====== Interacción principal (botones táctiles) ======
    void OnInteractTapped()
    {
        if (!dialoguePanel.activeSelf) OpenDialogue();
        else OnContinueTapped();
    }

    void OnContinueTapped()
    {
        if (isTyping) { FinishTypingCurrent(); return; }
        NextLine();
    }

    // ====== Tap directo sobre el NPC (requiere collider) ======
    void OnMouseDown() // Funciona en móvil como tap
    {
        if (player == null) return;
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist > tapStartDistance) return; // muy lejos

        if (!dialoguePanel.activeSelf) OpenDialogue();
        else OnContinueTapped();
    }

    // ====== Diálogo ======
    void OpenDialogue()
    {
        if (lines == null || lines.Length == 0) return;
        index = 0;
        dialoguePanel.SetActive(true);
        StartTyping(lines[index]);
    }

    void NextLine()
    {
        index++;
        if (index >= lines.Length) { CloseDialogue(); return; }
        StartTyping(lines[index]);
    }

    void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        // Si pausas al abrir, reanuda aquí: Time.timeScale = 1f;
    }

    void StartTyping(string fullText)
    {
        if (typingCR != null) StopCoroutine(typingCR);
        typingCR = StartCoroutine(TypeRoutine(fullText));
    }

    IEnumerator TypeRoutine(string fullText)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }

    void FinishTypingCurrent()
    {
        if (typingCR != null) StopCoroutine(typingCR);
        dialogueText.text = lines[index];
        isTyping = false;
    }

    // ====== Zona de activación (mostrar/ocultar botón "Hablar") ======
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            if (interactButton) interactButton.gameObject.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            if (interactButton) interactButton.gameObject.SetActive(false);
            CloseDialogue();
        }
    }

    // Versión 2D:
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            if (interactButton) interactButton.gameObject.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            if (interactButton) interactButton.gameObject.SetActive(false);
            CloseDialogue();
        }
    }
}
