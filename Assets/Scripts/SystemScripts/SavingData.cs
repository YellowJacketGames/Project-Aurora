using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class SavingData
{
    public int progressionIndex;
    public List<string> objectsIds;
    public List<string> typewritesIds;

    //game settings configuration - res, vol, quality.. 


    public SavingData()
    {
        ResetData();
    }

    public void AddObject(string objectId)
    {
        if (objectsIds.Any(id => id.Equals(objectId)))
            return;
        objectsIds.Add(objectId);
        // asi hace para crear un scriptableObject del id. por si hiciese falta usar un so por alguna razon
        // ObjectClass obj = new ObjectClass();
        // obj = obj.CreateObject(objectId);
        SavingManager.SaveNew(GameManager.instance.Data);
    }

    public bool HasObject(string objectId)
    {
        return (objectsIds.Any(id => id.Equals(objectId)));
    }
    public void RemoveObject(string objectId)
    {
        if (!objectsIds.Any(id => id.Equals(objectId))) return;

        objectsIds.Remove(objectId);
        ObjectClass obj = new ObjectClass();
        obj = obj.CreateObject(objectId);
        GameManager.instance.currentController.playerUIComponent.ShowObjectUsed(obj);
        SavingManager.SaveNew(GameManager.instance.Data);
    }

    public void AddTypewrite(string typewriteId)
    {
        if (typewritesIds.Any(id => id.Equals(typewriteId)))
            return;
        typewritesIds.Add(typewriteId);
        SavingManager.SaveNew(GameManager.instance.Data);
    }

    public bool HasTypewrite(string typewriteId)
    {
        return (typewritesIds.Any(id => id.Equals(typewriteId))) ;
    }

    public void RemoveTypewrite(string typewriteId)
    {
        if (!typewritesIds.Any(id => id.Equals(typewriteId))) return;
        typewritesIds.Remove(typewriteId);
        SavingManager.SaveNew(GameManager.instance.Data);
    }

    public SavingData(int progressionIndex)
    {
        this.progressionIndex = progressionIndex;
    }

    public bool HasSavedData() => (progressionIndex != 0);

    private void ResetData()
    {
        progressionIndex = 0;
        objectsIds = new List<string>();
        typewritesIds = new List<string>();
    }

    public void IncrementProgression()
    {
        progressionIndex++;
        SavingManager.SaveNew(GameManager.instance.Data);
    }
}