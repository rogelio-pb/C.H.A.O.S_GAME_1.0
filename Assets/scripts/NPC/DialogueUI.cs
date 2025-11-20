using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    [Header("Referencias UI")]
    public GameObject panel;                     // Panel principal del diálogo
    public TextMeshProUGUI dialogueText;        // Texto grande de la enfermera
    public Button[] optionButtons;              // Botones de opciones (3 en tu caso)

    [Header("Datos del diálogo actual")]
    public DialogueNode[] nodes;                // Nodos de este NPC (se configura en NPCDialogue)
    private NPCDialogue currentNPC;
    private int currentNodeIndex = -1;

    public bool IsOpen => panel != null && panel.activeSelf;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (panel != null)
            panel.SetActive(false);
    }

    // Llamado por NPCDialogue cuando haces click en un NPC
    public void ShowDialogue(NPCDialogue npc, int nodeIndex)
    {
        currentNPC = npc;
        nodes = npc.nodes;
        currentNodeIndex = nodeIndex;

        if (panel != null)
            panel.SetActive(true);

        UpdateNode();
    }

    private void UpdateNode()
    {
        if (nodes == null || currentNodeIndex < 0 || currentNodeIndex >= nodes.Length)
        {
            EndDialogue();
            return;
        }

        DialogueNode node = nodes[currentNodeIndex];

        // Texto principal
        if (dialogueText != null)
            dialogueText.text = node.text;

        // Limpiar y ocultar todos los botones
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].gameObject.SetActive(false);
        }

        if (node.options == null) return;

        // Configurar cada opción
        for (int i = 0; i < node.options.Length && i < optionButtons.Length; i++)
        {
            int optionIndex = i;
            DialogueOption opt = node.options[optionIndex];

            Button btn = optionButtons[i];
            btn.gameObject.SetActive(true);

            // Buscar el TMP de este botón y ponerle el texto de la opción
            TextMeshProUGUI label = btn.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null)
                label.text = opt.optionText;

            // Asignar qué pasa al hacer click
            btn.onClick.AddListener(() =>
            {
                OnOptionSelected(opt);
            });
        }
    }

    private void OnOptionSelected(DialogueOption option)
    {
        // Aplicar paranoia EN PORCENTAJE
        if (ParanoiaManager.Instance != null && Mathf.Abs(option.paranoiaDelta) > 0.001f)
        {
            ParanoiaManager.Instance.AddParanoiaPercent(option.paranoiaDelta);
        }

        // Ir al siguiente nodo o terminar diálogo
        if (option.nextNodeIndex >= 0 && option.nextNodeIndex < nodes.Length)
        {
            currentNodeIndex = option.nextNodeIndex;
            UpdateNode();
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        if (panel != null)
            panel.SetActive(false);

        currentNPC = null;
        currentNodeIndex = -1;

        foreach (var b in optionButtons)
            b.onClick.RemoveAllListeners();
    }
}
