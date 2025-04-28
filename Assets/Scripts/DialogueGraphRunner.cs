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

        for (int i = 0; i < _graph.nodes.Count; i++)
            if (_graph.nodes[i].started == true)
            {
                switch (_graph.nodes[i])
                {
                    #region RootNode
                    case RootNode:
                        switch (_graph.nodes[i].state)
                        {
                            case Node.State.Running:
                                break;
                        }
                        break;
                    #endregion
                    #region DialogueNode
                    case DialogueNode:
                        switch (_graph.nodes[i].state)
                        {
                            case Node.State.Running:
                                _graph.dialogueNode = _graph.nodes[i] as DialogueNode;
                                currentDialogue = _graph.dialogueNode.dialogue;
                                Debug.Log($"{_graph.nodes[i].name} {_graph.nodes[i].guid} {_graph.nodes[i].state} \n \"{_graph.dialogueNode.dialogue}\"");

                                break;
                            case Node.State.Success:
                                currentDialogue = _graph.dialogueNode.dialogue;
                                currentDialogue = "";
                                break;
                        }

                        break;
                    #endregion
                    #region ActionNode
                    case ActionNode:
                        switch (_graph.nodes[i].state)
                        {
                            case Node.State.Success:
                                break;
                        }
                        break;
                    #endregion
                    #region CompositeNode
                    case CompositeNode:
                        switch (_graph.nodes[i].state)
                        {
                            case Node.State.Running:
                                break;
                        }
                        break;
                    #endregion
                    #region DecoratorNode
                    case DecoratorNode:
                        switch (_graph.nodes[i].state)
                        {
                            case Node.State.Running:
                                break;
                        }
                        break;
                        #endregion
                }
            }
    }
}
