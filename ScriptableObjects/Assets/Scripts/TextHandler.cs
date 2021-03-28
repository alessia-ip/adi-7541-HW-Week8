using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using UnityEditor;

namespace TMP_TextUtilities //using the TMP utilities since they include a function I need
{

   public class TextHandler : MonoBehaviour
   {
      public TextMeshProUGUI textBox;
      public TextMeshProUGUI titleBox;
      public TextBlockScriptableObject currentObject;
      public Camera cam;

      //public string[] allWords;

      public Texture2D cursorReg;
      public Texture2D cursorHov;

      private Vector3 lastPos;
      
      private void Start()
      {
         printText();
         lastPos = Input.mousePosition;
      }

      private void Update()
      {
         if (Input.GetMouseButtonDown(0))
         {
            advanceText();
         }

         if (Input.mousePosition != lastPos)
         {
            hoverText();
         }

         lastPos = Input.mousePosition;
      }

      void printText()
      {
         var textString = currentObject.bodyText;
         textString = textString.Replace("@", "\n");
         textString = textString.Replace("$", "<b><color=#2e7e99>");
         textString = textString.Replace("%", "</color></b>");
         textBox.text = textString;
         titleBox.text = currentObject.header;
      }

      void hoverText()
      {
         string[] allWords;
         var mousePos = new Vector3(
            (Input.mousePosition).x,
            (Input.mousePosition).y,
            textBox.transform.position.z);
         //the word number I get using the find intersecting word
         //this gets the # at the position i've specified
         
         //reference string interpolation: https://diegogiacomelli.com.br/unitytips-string-interpolation/
         //thanks Moochi uwu
         
         if(TMPro.TMP_TextUtilities.FindIntersectingWord(textBox, mousePos, cam) >= 0)
         {
            var wordNum = TMPro.TMP_TextUtilities.FindIntersectingWord(textBox, mousePos, cam);
            //from the int I got, I can then make a an array of strings (representing each word)
            //and then check which word is at the INT! 
            var strippedString = currentObject.bodyText.Replace('$', ' ');
            strippedString = strippedString.Replace('%', ' ');
            strippedString = strippedString.Replace('.', ' ');
            strippedString = strippedString.Replace('@', ' ');
            strippedString = strippedString.Replace(',', ' ');
            allWords = strippedString.Split(' ');
            List<string> noWhitespaces = new List<string>();
            for (int w= 0; w < allWords.Length - 1; w++)
            {
               if (allWords[w] != "")
               {
                  //Debug.Log("Added: " + allWords[w]);
                  noWhitespaces.Add(allWords[w]);
               }
            }
            //Debug.Log(allWords);
            //Debug.Log(allWords[wordNum]);
            Debug.Log(wordNum);
            Debug.Log(noWhitespaces[wordNum]);
            var hoveredWord = noWhitespaces[wordNum];

            int i = 0;
            foreach (var importantWords in currentObject.importantWords)
            {
               if (importantWords == hoveredWord)
               {
                  Cursor.SetCursor(cursorHov, Vector2.zero, CursorMode.ForceSoftware);
                  break;
               }
               else
               {
                  Cursor.SetCursor(cursorReg, Vector2.zero, CursorMode.ForceSoftware);
               }
               i++;
            }
         }
         else
         {
            Cursor.SetCursor(cursorReg, Vector2.zero, CursorMode.ForceSoftware);
         }
        
      }
      
      private void advanceText()
      {
         string[] allWords;
         //the position I want to check at is the mouse position x and y, but the GUI z pos, since it's vector 3
         var mousePos = new Vector3(
            (Input.mousePosition).x,
            (Input.mousePosition).y,
            textBox.transform.position.z);
         //the word number I get using the find intersecting word
         //this gets the # at the position i've specified
         var wordNum = TMPro.TMP_TextUtilities.FindIntersectingWord(textBox, mousePos, cam);
         //from the int I got, I can then make a an array of strings (representing each word)
         //and then check which word is at the INT! 
         var strippedString = currentObject.bodyText.Replace('$', ' ');
         strippedString = strippedString.Replace('%', ' ');
         strippedString = strippedString.Replace('.', ' ');
         strippedString = strippedString.Replace('@', ' ');
         strippedString = strippedString.Replace(',', ' ');
         allWords = strippedString.Split(' ');
         List<string> noWhitespaces = new List<string>();
         for (int w= 0; w < allWords.Length - 1; w++)
         {
            if (allWords[w] != "")
            {
               //Debug.Log("Added: " + allWords[w]);
               noWhitespaces.Add(allWords[w]);
            }
         }
         //Debug.Log(allWords);
         //Debug.Log(allWords[wordNum]);
         Debug.Log(wordNum);
         Debug.Log(noWhitespaces[wordNum]);
         var clickedWord = noWhitespaces[wordNum];

         int i = 0;
         foreach (var importantWords in currentObject.importantWords)
         {
            if (importantWords == clickedWord)
            {
               currentObject = currentObject.newBlocks[i];
               printText();
               break;
            }

            i++;
         }
      }
      
      
      
   }
}