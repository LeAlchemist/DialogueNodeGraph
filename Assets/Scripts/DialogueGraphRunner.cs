using UnityEngine;

public class DialogueGraphRunner : MonoBehaviour
{
    public DialogueGraph _graph;

    void Start()
    {
        _graph = _graph.Clone();
    }

    // Update is called once per frame
    void Update()
    {
        _graph.Update();
    }
}
