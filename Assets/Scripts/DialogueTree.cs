using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue Graph")]
public class DialogueTree : ScriptableObject
{
    public Node rootNode;
    public Node.State treeState = Node.State.Running;

    public Node.State Update()
    {
        if (rootNode.state == Node.State.Running)
        {
            return rootNode.Update();
        }

        return treeState;
    }
}
