using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage; // Original background sprite

    public List<Button> btns = new List<Button>();
    private Dictionary<Button, Color> buttonColorMap = new Dictionary<Button, Color>(); // Map buttons to colors
    private Button firstClickedButton = null;

    public List<Color> pairColors = new List<Color>(); // Colors for pairing

    [SerializeField]
    private Text gameOverText; // Reference to the "GAME OVER" text UI element

    void Start()
    {
        GetButtons();
        AssignPairColors();
        AddListeners();

        // Initially hide the "GAME OVER" text
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {
            Button btn = objects[i].GetComponent<Button>();
            if (btn != null)
            {
                btns.Add(btn);
                btn.image.sprite = bgImage; // Set to default background
            }
        }
    }

    void AssignPairColors()
    {
        // Create color pairs
        if (pairColors.Count == 0)
        {
            for (int i = 0; i < btns.Count / 2; i++)
            {
                Color randomColor = new Color(Random.value, Random.value, Random.value);
                pairColors.Add(randomColor);
                pairColors.Add(randomColor); // Ensure pairs of the same color
            }
            Shuffle(pairColors); // Shuffle the colors
        }

        // Map colors to buttons
        for (int i = 0; i < btns.Count; i++)
        {
            buttonColorMap[btns[i]] = pairColors[i];
            SetButtonColor(btns[i], Color.gray); // Set all buttons to hidden color initially
        }
    }

    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => OnButtonClick(btn));
        }
    }

    void OnButtonClick(Button clickedButton)
    {
        // Ignore if the button has already been matched
        if (!clickedButton.interactable)
            return;

        // Show the button's color
        SetButtonColor(clickedButton, buttonColorMap[clickedButton]);

        // First button click
        if (firstClickedButton == null)
        {
            firstClickedButton = clickedButton;
        }
        else
        {
            // Check if the second button matches the first
            if (buttonColorMap[firstClickedButton] == buttonColorMap[clickedButton])
            {
                Debug.Log("Match Found!");
                // Keep the colors visible
                firstClickedButton.interactable = false;
                clickedButton.interactable = false;

                CheckGameOver(); // Check if the game is over
            }
            else
            {
                Debug.Log("No Match!");
                // Hide the colors after a short delay
                StartCoroutine(HideColors(firstClickedButton, clickedButton));
            }

            firstClickedButton = null; // Reset for the next pair
        }
    }

    IEnumerator HideColors(Button btn1, Button btn2)
    {
        yield return new WaitForSeconds(1); // Delay to allow user to see the colors
        SetButtonColor(btn1, Color.gray);
        SetButtonColor(btn2, Color.gray);
    }

    void SetButtonColor(Button btn, Color color)
    {
        Image img = btn.GetComponent<Image>();
        if (img != null)
        {
            img.color = color;
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void CheckGameOver()
    {
        // Check if all buttons are no longer interactable
        if (btns.TrueForAll(button => !button.interactable))
        {
            Debug.Log("Game Over! All pairs matched.");
            // Display "GAME OVER" text
            if (gameOverText != null)
            {
                gameOverText.text = "GAME OVER";
                gameOverText.gameObject.SetActive(true);
            }

            // Optionally, disable all button interactions (not necessary since they are already interactable=false)
            foreach (Button btn in btns)
            {
                btn.interactable = false;
            }
        }
    }
}
