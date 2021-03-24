using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//this is my testing script to figure out how to tell which word is being clicked on

namespace TMP_TextUtilities //using the TMP utilities since they include a function I need
{
    public class StringHoverGUITest : MonoBehaviour //putting my monobehaviour inside the namespace gives me access to TMP
    {

        public TextMeshProUGUI textHolder;

        public TMP_Text gameText;

        public Camera cam;

        
        void Start()
        {
            textHolder.text = "Never gonna give you up, never gonna let you down";
            //for this test, I am just using a set string of text
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetWord();
            }   
        }

        void GetWord()
        {
            //the position I want to check at is the mouse position x and y, but the GUI z pos, since it's vector 3
            var mousepos = new Vector3(
                (Input.mousePosition).x,
                (Input.mousePosition).y,
                gameText.transform.position.z);
            //Debug.Log(mousepos);
            //the word number I get using the find intersecting word
            //this gets the # at the position i've specified
            var wordNum = TMPro.TMP_TextUtilities.FindIntersectingWord(gameText, mousepos, cam);
            Debug.Log(wordNum);
            //from the int I got, I can then make a an array of strings (representing each word)
            //and then check which word is at the INT! 
            var words = textHolder.text.Split(' ');
            Debug.Log(words[wordNum]);
        }
    }

}
