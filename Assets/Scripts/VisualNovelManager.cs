using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class VisualNovelManager : MonoBehaviour
{
    public bool dialogueEnabled, logEnabled, posesEnabled, portraitEnabled;
    public GameObject background, textCanvasPanel, logCanvasPanel;
    public Color canvasColor = new Color(0, 0, 0, 1);
    public GameObject portrait;
    public TextMeshProUGUI headerTextBox;
    public TextMeshProUGUI bodyTextBox;
    public int currentChapter, currentDialogue;
    public DialogueChunkScriptable[] dialogueChunk;
    public TextMeshProUGUI logTextBox;
    [TextArea]
    public string textLog;


    void Awake()
    {
        RefreshContentFitters();
    }

    // Start is called before the first frame update
    void Start()
    {
        logEnabled = false;

        currentDialogue = 0;
        textLog = dialogueChunk[currentDialogue].dialogueHeader + "\n" + dialogueChunk[currentDialogue].dialogueBody + "\n";
        RefreshContentFitters();
    }

    // Update is called once per frame
    void Update()
    {
        logCanvasPanel.SetActive(logEnabled);

        textCanvasPanel.GetComponent<Image>().color = canvasColor;
        logCanvasPanel.GetComponent<Image>().color = canvasColor;
        headerTextBox.text = dialogueChunk[currentDialogue].dialogueHeader;
        bodyTextBox.text = dialogueChunk[currentDialogue].dialogueBody;
        portraitEnabled = dialogueChunk[currentDialogue].hasPortrait;
        portrait.SetActive(portraitEnabled);
        posesEnabled = dialogueChunk[currentDialogue].hasPoses;
        RefreshContentFitters();

        logTextBox.text = textLog;
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
        if (currentDialogue < (dialogueChunk.Length - 1))
        {
            currentDialogue += 1;
            textLog += dialogueChunk[currentDialogue].dialogueHeader + "\n" + dialogueChunk[currentDialogue].dialogueBody + "\n";
            RefreshContentFitters();
        }
    }

    [ContextMenu("Prev Dialogue")]
    public void prevDialogue()
    {
        if (currentDialogue != 0)
        {
            textLog = textLog.Replace(dialogueChunk[currentDialogue].dialogueBody, "");
            textLog = textLog.Replace(dialogueChunk[currentDialogue].dialogueHeader + "\n", "");
            currentDialogue -= 1;
            RefreshContentFitters();
        }
        
    }

    [ContextMenu("View Log")]
    public void viewTextLog()
    {
        logEnabled = true;
    }

    [ContextMenu("Close Log")]
    public void closeTextLog()
    {
        logEnabled = false;
    }
}