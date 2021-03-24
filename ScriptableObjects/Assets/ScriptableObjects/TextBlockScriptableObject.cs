using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//rather than extend monobehaviour, we are extending a scriptable object
//this holds basic data for us
//start and update don't do anything anymore automatically, so we remove them
//scriptable objects persist between sessions
//make a game using these scriptable objects!


[CreateAssetMenu (fileName = "New Text Block", 
    menuName = "ScriptableObjects/TextBlock",
    order = 0)]

public class TextBlockScriptableObject : ScriptableObject
{
    public string header;
    public string bodyText;
    public string[] importantWords;
    public TextBlockScriptableObject[] newBlocks;
}
