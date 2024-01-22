using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MonoDispatcher{

    public interface IMono
    {
        event Func<Component> MyGetComponent;
        void OnStart();
    }
    public interface IEnableAndDisable{
        void OnEnable();
        void OnDisable();
    }
}