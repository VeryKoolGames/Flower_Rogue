using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private int _dialogueIndex = 0;
    private bool _isWriting = false;
    private bool _skipRequested = false;

    public async Task StartDialogue(List<string> dialogue)
    {
        _dialogueIndex = 0;
        dialogueText.text = "";
        dialoguePanel.SetActive(true);

        while (_dialogueIndex < dialogue.Count)
        {
            await WriteDialogue(dialogue[_dialogueIndex]);
            await AwaitPlayerInput();
        }

        dialoguePanel.SetActive(false);
    }
    
    void Update()
    {
        if (_isWriting && Input.GetKeyDown(KeyCode.Space))
        {
            _skipRequested = true;
        }
    }

    private async Task WriteDialogue(string dialogue)
    {
        _isWriting = true;
        _skipRequested = false;
        dialogueText.text = "";
        foreach (char letter in dialogue)
        {
            if (_skipRequested)
            {
                dialogueText.text = dialogue;
                break;
            }
            dialogueText.text += letter;
            await Awaitable.WaitForSecondsAsync(.05f);
        }
        _isWriting = false;
    }

    private async Task AwaitPlayerInput()
    {
        while (!Input.GetKeyDown(KeyCode.E) || _isWriting)
        {
            await Task.Yield();
        }

        _dialogueIndex++;
    }
}