using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//this is my testing script to figure out how to tell which word is being clicked on

namespace TMP_TextUtilities
{
    public class StringHoverGUITest : MonoBehaviour
    {

        public TextMeshProUGUI textHolder;

        public TMP_Text gameText;

        public Camera cam;
        // Update is called once per frame
        void Start()
        {
            textHolder.text = "Testing <b>TESTIN</b>";
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
            var mousepos = new Vector3(
                (Input.mousePosition).x,
                (Input.mousePosition).y,
                gameText.transform.position.z);
            //Debug.Log(mousepos);
            var wordNum = TMPro.TMP_TextUtilities.FindIntersectingWord(gameText, mousepos, cam);
            Debug.Log(wordNum);
            var words = textHolder.text.Split(' ');
            Debug.Log(words[wordNum]);
        }
    }

}
