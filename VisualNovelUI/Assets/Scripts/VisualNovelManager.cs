using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class VisualNovelManager : MonoBehaviour
{
    public GameObject textCanvasPanel;
    public Vector2 textCanvasSize;
    public Color textCanvasColor = new Color(0, 0, 0, 1);
    public TextMeshProUGUI textBox;
    public int currentDialogue;
    public DialogueChunkScriptable[] dialogueChunk;
    [TextArea]
    public string textLog;

    // Start is called before the first frame update
    void Start()
    {
        currentDialogue = 0;
        textLog = dialogueChunk[currentDialogue].dialogue;
    }

    // Update is called once per frame
    void Update()
    {
        textCanvasPanel.GetComponent<Image>().color = textCanvasColor;
        textBox.text = dialogueChunk[currentDialogue].dialogue;
    }

    [ContextMenu("Next Dialogue")]
    public void nextDialogue()
    {
        currentDialogue += 1;
        textLog += dialogueChunk[currentDialogue].dialogue;
    }

    [ContextMenu("Prev Dialogue")]
    public void prevDialogue()
    {
        textLog = textLog.Replace(dialogueChunk[currentDialogue].dialogue, "");
        currentDialogue -= 1;
    }

    [ContextMenu("View Log")]
    public void viewTextLog()
    {
        
    }
}