using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu]
public class ItemsHolder : ScriptableObject
{
    private string _itemName;
    private Sprite _icon;
    [TextArea] private string _description;

    public string ItemName
    {
        get
        {
            return _itemName;
        }
    }

    public Sprite icon
    {
        get
        {
            return _icon;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }
    }
}

