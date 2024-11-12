using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage; // Original background sprite
    [SerializeField]
    private Sprite flippedImage; // Sprite for the flipped state

    public List<Button> btns = new List<Button>();

    void Start()
    {
        GetButtons();
        AddListeners();
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage; // Set to default background
        }
    }

    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    public void PickAPuzzle()
    {
        // Get the button that was clicked
        GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        string buttonName = clickedButton.name;

        // Change the sprite to show the button as "flipped"
        clickedButton.GetComponent<Image>().sprite = flippedImage;

        // Log the name of the clicked button
        Debug.Log("You are clicking a button named " + buttonName);
    }
}
