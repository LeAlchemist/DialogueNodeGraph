using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DialogueChunkScriptable : ScriptableObject
{
    public bool hasPoses, hasPortrait;
    public string dialogueHeader;
    //Should add header font and size options here
    [TextArea]
    public string dialogueBody;
    //should add body font and size options here
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
