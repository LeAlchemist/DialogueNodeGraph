using UnityEngine;

public class DialogueTreeRunner : MonoBehaviour
{
    public DialogueTree _dialogueTree;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _dialogueTree.Update();
    }
}
