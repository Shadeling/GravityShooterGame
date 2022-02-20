using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControler : MonoBehaviour
{
    
    [SerializeField] int KeyTypes = 3;

    private int[] _keys;
    void Start()
    {
        _keys = new int[KeyTypes];
    }

    public void AddKey(int type, int amount = 1)
    {
        if (type < KeyTypes)
        {
            _keys[type] += amount;
        }
    }

    public bool RemoveKey(int type, int amount = 1)
    {
        if (type < KeyTypes && _keys[type]>=amount)
        {
            _keys[type] -= amount;
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
