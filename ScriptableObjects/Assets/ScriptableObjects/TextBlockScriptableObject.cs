using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//rather than extend monobehaviour, we are extending a scriptable object
//this holds basic data for us
//start and update don't do anything anymore automatically, so we remove them
//scriptable objects persist between sessions
//make a game using these scriptable objects!

//this shows the SO in the editor
[CreateAssetMenu (fileName = "New Text Block", 
    menuName = "ScriptableObjects/TextBlock",
    order = 0)]

public class TextBlockScriptableObject : ScriptableObject
{
    //header text
    public string header;
    //main body text
    public string bodyText;
    //important words within the block of text
    public string[] importantWords;
    //an array of possible next objects to navigate to
    public TextBlockScriptableObject[] newBlocks;
}
