using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;
    [SerializeField]
    private GameObject btn;

    void Awake()
    {
        // Creating the buttons and setting their names and tags
        for (int i = 0; i < 7; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false);
            button.tag = "PuzzleButton"; // Set the tag for each button
        }
    }
}
