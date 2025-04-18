using UnityEngine;

public class DialogueGraphRunner : MonoBehaviour
{
    public DialogueGraph _graph;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _graph.Update();
    }
}
