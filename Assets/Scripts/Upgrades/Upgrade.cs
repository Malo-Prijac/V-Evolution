using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : ScriptableObject
{
    [SerializeField]protected string name;
    [SerializeField][TextArea(3, 20)] protected string description;
    [SerializeField]protected int price;

    public string Name
    {
        get => name;
        set => name = value;
    }

    public string Description
    {
        get => description;
        set => description = value;
    }

    public int Price
    {
        get => price;
        set => price = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void UseUpgrade(GameObject player)
    {
        throw new System.Exception("Skill not implemented !");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
