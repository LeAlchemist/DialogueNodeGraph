using UnityEngine;

public class DialogueGraphRunner : MonoBehaviour
{
    public DialogueGraph _graph;
    public string currentDialogue = "";

    void Start()
    {
        _graph = _graph.Clone();
        _graph.node = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _graph.Update();

        if (_graph.node < _graph.nodes.Count)
            switch (_graph.nodes[_graph.node])
            {
                #region RootNode
                case RootNode:
                    switch (_graph.nodes[_graph.node].state)
                    {
                        case Node.State.Running:
                            _graph.node++;
                            break;
                    }
                    break;
                #endregion
                #region DialogueNode
                case DialogueNode:
                    switch (_graph.nodes[_graph.node].state)
                    {
                        case Node.State.Running:
                            _graph.dialogueNode = _graph.nodes[_graph.node] as DialogueNode;
                            currentDialogue = _graph.dialogueNode.dialogue;
                            Debug.Log($"{_graph.nodes[_graph.node].name} {_graph.nodes[_graph.node].guid} {_graph.nodes[_graph.node].state} {_graph.dialogueNode.dialogue}");
                            break;
                        case Node.State.Success:
                            currentDialogue = "";
                            _graph.node++;
                            break;
                    }
                    break;
                #endregion
                #region ActionNode
                case ActionNode:
                    switch (_graph.nodes[_graph.node].state)
                    {
                        case Node.State.Success:
                            _graph.node++;
                            break;
                    }
                    break;
                #endregion
                #region CompositeNode
                case CompositeNode:
                    switch (_graph.nodes[_graph.node].state)
                    {
                        case Node.State.Running:
                            _graph.node++;
                            break;
                    }
                    break;
                #endregion
                #region DecoratorNode
                case DecoratorNode:
                    switch (_graph.nodes[_graph.node].state)
                    {
                        case Node.State.Running:
                            _graph.node++;
                            break;
                    }
                    break;
                    #endregion
            }
    }
}
