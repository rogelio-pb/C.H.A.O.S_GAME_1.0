using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    [Header("Referencias UI")]
    public GameObject panel;                 // Panel del diálogo (Canvas)
    public TextMeshProUGUI dialogueText;     // Texto principal del NPC
    public Button[] optionButtons;           // Botones de opciones (mínimo 3 recomendados)

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
            panel.SetActive(false); // Oculto al inicio
    }

    public void ShowDialogue(NPCDialogue npc, int nodeIndex)
    {
        currentNPC = npc;
        currentNodeIndex = nodeIndex;

        if (panel != null)
            panel.SetActive(true);

        UpdateNode();
    }

    private void UpdateNode()
    {
        if (currentNPC == null ||
            currentNodeIndex < 0 ||
            currentNodeIndex >= currentNPC.nodes.Length)
        {
            EndDialogue();
            return;
        }

        DialogueNode node = currentNPC.nodes[currentNodeIndex];

        // Mostrar texto del NPC
        dialogueText.text = node.text;

        // Configurar opciones
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < node.options.Length)
            {
                optionButtons[i].gameObject.SetActive(true);

                DialogueOption opt = node.options[i];

                // Cambiar texto del botón
                TextMeshProUGUI btnText = optionButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (btnText != null)
                    btnText.text = opt.optionText;

                // Capturar variables locales
                int nextIndex = opt.nextNodeIndex;
                float paranoiaDelta = opt.paranoiaDelta;

                // Limpiar listeners anteriores
                optionButtons[i].onClick.RemoveAllListeners();

                // Agregar nueva función de click
                optionButtons[i].onClick.AddListener(() => OnOptionSelected(nextIndex, paranoiaDelta));
            }
            else
            {
                // Si no hay más opciones, ocultar botones sobrantes
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnOptionSelected(int nextNodeIndex, float paranoiaDelta)
    {
        // Aplicar paranoia si existe un ParanoiaManager
        if (ParanoiaManager.Instance != null && Mathf.Abs(paranoiaDelta) > 0.01f)
        {
            ParanoiaManager.Instance.AddParanoia(paranoiaDelta);
        }

        // Cambiar al siguiente nodo o cerrar el diálogo
        if (nextNodeIndex < 0)
        {
            EndDialogue();
        }
        else
        {
            currentNodeIndex = nextNodeIndex;
            UpdateNode();
        }
    }

    public void EndDialogue()
    {
        if (panel != null)
            panel.SetActive(false);

        currentNPC = null;
        currentNodeIndex = -1;

        // Limpiar listeners por seguridad
        foreach (Button b in optionButtons)
        {
            b.onClick.RemoveAllListeners();
        }
    }
}
