using UnityEngine;

public class Introduction : MonoBehaviour
{
    [SerializeField]
    private Notebook _notebook;

    [SerializeField]
    [TextArea(3, 10)]
    private string _introductionText;

    private void Start()
    {
        _notebook.SetText(_introductionText);
        _notebook.Toggle();
    }
}
