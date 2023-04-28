using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGraphRunner : MonoBehaviour
{
    public DialogueGraph graph;
    // Start is called before the first frame update
    void Start()
    {
        graph = ScriptableObject.CreateInstance<DialogueGraph>();
        var logNode = ScriptableObject.CreateInstance<DebugLogNode>();
        logNode.message = "Test State";

        var dialogueNode = ScriptableObject.CreateInstance<DialogueNode>();
        logNode.message = dialogueNode.dialogueText;
        
        graph.rootNode = logNode;
    }

    // Update is called once per frame
    void Update()
    {
        graph.Update();
    }
}
