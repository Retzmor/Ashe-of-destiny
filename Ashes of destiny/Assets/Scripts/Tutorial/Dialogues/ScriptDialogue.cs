using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ScriptDialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float velocityText = 0.1f;
    private bool wasSkipped = false;
    private string[] dialogues;
    [SerializeField] GameObject dialoguePanel;
    public System.Action OnDialogueEnd;
    private bool isWrite = false;
    private int numberText;

    PlayerControls input;
    InputAction nextAction;
    float inputCooldown = 0.2f;
    float lastInputTime;

    private void OnEnable()
    {
        input = new PlayerControls();
        input.Enable();

        input.Dialogue.Enable();
        nextAction = input.Dialogue.Next;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        if (!dialoguePanel.activeSelf) return;

        if (nextAction.triggered && Time.time - lastInputTime > inputCooldown)
        {
            lastInputTime = Time.time;
            ChangeText();
        }
    }


    // Start is called before the first frame update

    public void ChangeText()
    {
        if (isWrite)
        {
            StopAllCoroutines();
            text.text = dialogues[numberText];
            isWrite = false;
        }

        else
        {
            NextLine();
        }
    }


    IEnumerator LineOfText()
    {
        isWrite = true;
        text.text = "";
        foreach (char letter in dialogues[numberText].ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(velocityText);
        }
        isWrite = false;
    }


    private void NextLine()
    {
        if (numberText < dialogues.Length - 1)
        {
            numberText++;
            text.text = string.Empty;
            StartCoroutine(LineOfText());
        }

        else
        {
            DesactiveUI();
        }

    }

    public void SkipDialogue()
    {
        if (wasSkipped) return;

        wasSkipped = true;
        StopAllCoroutines();
        text.text = "";
        DesactiveUI();
    }

    private void DesactiveUI()
    {
        dialoguePanel.SetActive(false);
        OnDialogueEnd?.Invoke();
    }

    public void SetDialogue(string[] newDialogues)
    {
        dialogues = newDialogues;
        numberText = 0;
        text.text = string.Empty;
        dialoguePanel.SetActive(true);
        wasSkipped = false;
        StartCoroutine(LineOfText());
    }
}

