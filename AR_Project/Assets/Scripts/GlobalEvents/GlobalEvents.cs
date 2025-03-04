using UnityEngine;

public class GlobalEvents
{
    private static GlobalEvents _instance = null;

    public GlobalEvents GetInstance()
    {
        if (_instance == null)
            _instance = new GlobalEvents();
        return _instance;
    }

    public FurnitureInfo CurrentFurnitureChanged;

    public delegate void FurnitureInfo(GameObject model, FurnitureType type);
}
