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

    public struct Variables
    {
        public int Speed;
        public float Scale;
    }
}