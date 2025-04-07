using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ChapterChunkScriptable : ScriptableObject
{
    public string chapterName;
    public Sprite[] chapterBackground;
    public DialogueChunkScriptable[] dialogueChunk;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
