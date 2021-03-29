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
         printText(); //print text is called once at the start to print the initial scriptable object's text
         lastPos = Input.mousePosition;
      }

      private void Update()
      {
         if (Input.GetMouseButtonDown(0))
         {
            advanceText(); //if we click with the mouse, we want to advance the text, if possible
         }

         if (Input.mousePosition != lastPos)
         {
            hoverText(); //here we change what the hover icon is showing
         }

         //this keeps track of the mouse's last position
         lastPos = Input.mousePosition;
      }

      //the function used to print text to the UI
      void printText()
      {
         //the text we want to print to the main body is the scriptable object's 'body text'
         var textString = currentObject.bodyText;
         
         //before we print, we change all of the following characters to be formatting in the string
         textString = textString.Replace("@", "\n");
         textString = textString.Replace("$", "<b><color=#2e7e99>");
         textString = textString.Replace("%", "</color></b>");
         //then we print the string
         textBox.text = textString;
         
         //lastly we print the headers
         titleBox.text = currentObject.header;
      }

      //the hover text function is very similar to the  advance text function
      //this is only run when the cursor is moved
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
         
         //check what word in the TextMesh box is under the cursor's coordinates
         if(TMPro.TMP_TextUtilities.FindIntersectingWord(textBox, mousePos, cam) >= 0)
         {
            var wordNum = TMPro.TMP_TextUtilities.FindIntersectingWord(textBox, mousePos, cam);
            //from the int I got, I can then make a an array of strings (representing each word)
            //and then check which word is at the INT! 
            
            //I want to take out all the formatting characters, and all punctuation here to get just the actual words
            var strippedString = currentObject.bodyText.Replace('$', ' ');
            strippedString = strippedString.Replace('%', ' ');
            strippedString = strippedString.Replace('.', ' ');
            strippedString = strippedString.Replace('@', ' ');
            strippedString = strippedString.Replace(',', ' ');
            
            //then all those words are broken into an array
            allWords = strippedString.Split(' ');
            
            //since the array/TMP counts whitespaces as words, we first make a list
            List<string> noWhitespaces = new List<string>();
            //then we iterate over the array
            for (int w= 0; w < allWords.Length - 1; w++)
            {
               //words are only added to the list if they are not blank
               if (allWords[w] != "")
               {
                  //Debug.Log("Added: " + allWords[w]);
                  noWhitespaces.Add(allWords[w]);
               }
            }

            //then we get the index of the hovered word here (which is an int)
            var hoveredWord = noWhitespaces[wordNum];

            //then we check to see if the word we've hovered over (referenced by the int) matches any word inside of the 'important words' array of our scriptable object
            int i = 0;
            foreach (var importantWords in currentObject.importantWords)
            {
               //if the word is in the array, we want a special cursor
               if (importantWords == hoveredWord)
               {
                  Cursor.SetCursor(cursorHov, Vector2.zero, CursorMode.ForceSoftware);
                  break; //since we found the correct word, we break the loop here
               }
               else //if not, we want the regular cursor
               {
                  Cursor.SetCursor(cursorReg, Vector2.zero, CursorMode.ForceSoftware);
               }
               i++;
            }
         }
         else //if the cursor is also not hovering any words, then we also want the default cursor
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
               noWhitespaces.Add(allWords[w]);
            }
         }

         Debug.Log(wordNum);
         Debug.Log(noWhitespaces[wordNum]);
         var clickedWord = noWhitespaces[wordNum];

         //this is where it differs from 'hover'
         //if the word we've clicked on is in the list of important words, we want to go to the corresponding block, and re-print out the text to the new block
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