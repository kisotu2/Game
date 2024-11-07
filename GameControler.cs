using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameControler : MonoBehaviour;
{
    public List<Button> btns = new List<Button>();

   void Start(){

   }
   void GetButtons(){
    GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
    for (int i = 0;i <objects.Length; i++){
        btns.Add(objects[i].GetComponent<Button>());
    }
   }
}
