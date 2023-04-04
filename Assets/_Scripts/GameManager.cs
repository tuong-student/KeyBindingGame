using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, ISingleton
{
    void Awake()
    {
        RegisterToContainer();
    }

    public void RegisterToContainer()
    {
        SingletonContainer.Register(this);
    }

    public void UnRegisterFromContainer()
    {
        throw new System.NotImplementedException();
    }
}
