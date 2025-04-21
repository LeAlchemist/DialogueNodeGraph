using UnityEngine;

public class DialogueNode : ActionNode
{
    [TextArea]
    public string dialogue;
    public float wordsPerMinute = 100;
    float secondsPerWord;
    int wordCount, index;
    [HideInInspector]
    public float duration = 1;
    [HideInInspector]
    float startTime;

    public DialogueNode()
    {
        description = "This holds dialogue";
        dialogue = "";
    }

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        secondsPerWord = 60 / wordsPerMinute;

        GetWordCount();

        duration = wordCount * secondsPerWord + 5;

        if (Time.time - startTime > duration)
        {
            return State.Success;
        }

        return State.Running;
    }

    public void GetWordCount()
    {
        while (index < dialogue.Length && char.IsWhiteSpace(dialogue[index]))
            index++;

        while (index < dialogue.Length)
        {
            // check if current char is part of a word
            while (index < dialogue.Length && !char.IsWhiteSpace(dialogue[index]))
                index++;

            wordCount++;

            // skip whitespace until next word
            while (index < dialogue.Length && char.IsWhiteSpace(dialogue[index]))
                index++;
        }
    }
}
