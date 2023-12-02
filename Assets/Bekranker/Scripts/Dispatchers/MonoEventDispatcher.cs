using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using MonoDispatcher;
public class MonoEventDispatcher : MonoBehaviour
{
    private IList<IMono> _dispatchables = new List<IMono>();

    void Awake() 
    {
        Type[] types = Assembly.GetExecutingAssembly().GetTypes();
        IEnumerable<Type> dispatchableTypes = types.Where(t => typeof(IMono).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        foreach (var item in dispatchableTypes)
        {
            IMono monoEventDispatchable = (Activator.CreateInstance(item) as IMono);
            _dispatchables.Add(monoEventDispatchable);
        }
    }

    void Start()
    {
        foreach (var dispatchable in _dispatchables) {            
            dispatchable.OnStart();
        }
    }

    void OnEnable()
    {
        foreach (var dispatchable in _dispatchables)
        {
            dispatchable.MyGetComponent += ()=> GetComponent<Component>();
        }
    }
    void OnDisable()
    {
        foreach (var dispatchable in _dispatchables)
        {
            dispatchable.MyGetComponent -= ()=> GetComponent<Component>();
        }
    }
}