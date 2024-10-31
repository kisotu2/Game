using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerialField]
    private Transform puzzleField;


    [SerialField]
    private GameObject btn;
    void Awake(){
        //creating the number of buttons depending on the limit
        for(int i = 0; i < 8, i++){
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField);
        }

    }
}
