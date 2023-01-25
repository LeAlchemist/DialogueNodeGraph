using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class VisualNovelManager : MonoBehaviour
{
    public GameObject textCanvasPanel;
    public Color textCanvasColor = new Color(0, 0, 0, 1);
    public TextMeshProUGUI textBox;
    public int currentDialogue;
    public DialogueChunkScriptable[] dialogueChunk;
    [TextArea]
    public string textLog;


    void Awake()
    {
        RefreshContentFitters();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentDialogue = 0;
        textLog = dialogueChunk[currentDialogue].dialogue;
        RefreshContentFitters();
    }

    // Update is called once per frame
    void Update()
    {
        textCanvasPanel.GetComponent<Image>().color = textCanvasColor;
        textBox.text = dialogueChunk[currentDialogue].dialogue;
        RefreshContentFitters();
    }

    public void RefreshContentFitters()
    {
        var rectTransform = (RectTransform)transform;
        RefreshContentFitter(rectTransform);
    }
 
    private void RefreshContentFitter(RectTransform transform)
    {
        if (transform == null || !transform.gameObject.activeSelf)
        {
            return;
        }
     
        foreach (RectTransform child in transform)
        {
            RefreshContentFitter(child);
        }
 
        var layoutGroup = transform.GetComponent<LayoutGroup>();
        var contentSizeFitter = transform.GetComponent<ContentSizeFitter>();
        if (layoutGroup != null)
        {
            layoutGroup.SetLayoutHorizontal();
            layoutGroup.SetLayoutVertical();
        }
 
        if (contentSizeFitter != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform);
        }
    }

    [ContextMenu("Next Dialogue")]
    public void nextDialogue()
    {
        currentDialogue += 1;
        textLog += dialogueChunk[currentDialogue].dialogue;
        RefreshContentFitters();
    }

    [ContextMenu("Prev Dialogue")]
    public void prevDialogue()
    {
        textLog = textLog.Replace(dialogueChunk[currentDialogue].dialogue, "");
        currentDialogue -= 1;
        RefreshContentFitters();
    }

    [ContextMenu("View Log")]
    public void viewTextLog()
    {
        
    }
}