using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameControler : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;
    
    public List<Button> btns = new List<Button>();

    void Start()
    {
        GetButtons();
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {
          btns.Add(objects[i].GetComponent<Button>());
          btns[i].image.sprite = bgImage;
           
        }
        void AddListeners(){
            foreach(Button btn in btns){
                btn.onclick.AddListener(()=>PickAPuzzle());
            }
        }
    }

    public void PickAPuzzle(){
        Debug.Log("You are clicking a button");
    }
}
