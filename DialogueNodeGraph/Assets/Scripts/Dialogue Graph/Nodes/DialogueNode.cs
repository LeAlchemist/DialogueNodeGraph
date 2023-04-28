using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNode : CompositeNode
{
    int current;
    [TextArea(3 , 6)]
    public string dialogueText = "Enter Text Here";
    public bool debugText = false;

    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (debugText == true)
        {
            Debug.Log(dialogueText);
        }
        
        var child = children[current];

        switch (child.Update())
        {
            case State.Running:
                return State.Running;
            case State.Failure:
                return State.Failure;
            case State.Success:
                current++;
                break;
        }
        
        return current == children.Count ? State.Success : State.Running;
    }
}
