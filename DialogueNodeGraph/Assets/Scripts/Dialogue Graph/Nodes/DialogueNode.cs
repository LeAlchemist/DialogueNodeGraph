using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNode : CompositeNode
{
    public string dialogueText = "Enter Text Here";

    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (state == State.Running)
        {
            state = Update();
        }
        return State.Running;
    }
}
