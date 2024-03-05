using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

//This script handles the data of each of the objects in game, both the 
//Key objects for progression and collectables like the typewriter objects

//The objects id is used to check if the object used is the correct one but will also be
//useful on other areas such as saving the game. We have to make sure that the corresponding Image
//to represent said object also has the same id to get this data in an easier way.

[CreateAssetMenu(fileName = "Object", menuName = "ScriptableObjects/Create new object", order = 1)]

public class ObjectClass : ScriptableObject //This is a scriptable object to make as many collectable objects as we want
{
    [Header("Object Variables")]
    [SerializeField] private string objectName;
    [SerializeField] private ObjectType type;
    [SerializeField] private string objectId;
    [SerializeField] int idIndex = 3;
    [SerializeField] Sprite objectIcon;
    [Space]
    [Header("Typewriter Variables")]
    [SerializeField] private string assignedLetter;
    [SerializeField][Range(0, 24)] public int assignedLetterIndex;
    [SerializeField] private TextAsset assignedDialogue;

    private void OnEnable()
    {
        //Generate the ID whenever the game begins
        GenerateId();

    }


    //This region holds all the methods to get the different variables from the object
    #region GetVariables 

    public string GetObjectName()
    {
        return objectName;
    }

    public ObjectType GetObjectType()
    {
        return type;
    }

    public string GetId()
    {
        return objectId;
    }

    public Sprite GetIcon()
    {
        return objectIcon;
    }

    public TextAsset GetDialogue()
    {
        return assignedDialogue;
    }
    #endregion


    public void GenerateId() //this method generates and ID which we will use to check if the object needed is 
    {
        //We first clear the id for new changes
        objectId = "";

        //We add the initials of "object and a '_' to make sure the string can be split if necessary
        objectId += "obj_";

        //We add the actual name of the object
        objectId += objectName+"_";

        //Depending on the type we will add different strings
        switch (type)
        {
            case ObjectType.KeyObject: //If it's a key item, it will have "key_" and end the id
                objectId += "key";
                break;
            case ObjectType.TypeWriterObject: //If it's a typewritter item, it will have "type_" plus the assigned letter for the typewriter
                objectId += "type_";
                objectId += assignedLetter;
                break;
            default:
                break;
        }
    }

    public bool CompareId(ObjectClass newObject) // Compares if the object required to do something is the same one the player has in their inventory
    {
        if(newObject.GetId() == objectId)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CompareId(string newObjectId) // Compares if the object id required to do something is the same one the player has in their inventory
    {
        if (newObjectId == objectId)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public ObjectClass CreateObject(string newId)
    {
        ObjectClass obj = new ObjectClass();
        string[] chars = newId.Split("_");

        obj.objectId = newId;
        obj.objectName = chars[1];

        obj.objectIcon = GetIconFromResources(obj);

        switch (chars[2])
        {
            case "key":
                obj.type = ObjectType.KeyObject;
                break;
            case "type":
                obj.type = ObjectType.TypeWriterObject;
                obj.assignedLetter = chars[3];
                break;
            default:
                break;
        }

        return obj;

    }

    public Sprite GetIconFromResources(ObjectClass obj)
    {
        Sprite s = Resources.Load<Sprite>("Sprites/ObjectIcons/" + obj.objectId);

        if (s != null)
        {
            return s;
        }
        else
        {
            Debug.Log("Sprite for " + objectName + " was not found");
            return null;
        }
    }
}
