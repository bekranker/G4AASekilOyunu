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
    private IList<IUpdate> _updates = new List<IUpdate>();

    void Awake() 
    {
        Type[] types = Assembly.GetExecutingAssembly().GetTypes();
        IEnumerable<IMono> IMonoD = (IEnumerable<IMono>)types.Where(t => typeof(IMono).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
        IEnumerable<IUpdate> IUpdatesDispatcher = (IEnumerable<IUpdate>)types.Where(t => typeof(IUpdate).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        DispatchleHandler(ref _dispatchables, in IMonoD);
        DispatchleHandler(ref _updates, in IUpdatesDispatcher);
    }

    void Start()
    {
        foreach (var dispatchable in _dispatchables) {            
            dispatchable?.OnStart();
        }
    }
    void Update()
    {
        foreach (var update in _updates){
            update?.OnUpdate();
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
    void DispatchleHandler<T>(ref IList<T> dispatchables, in IEnumerable<T> dispatchableList)
    {
        foreach (var dispatchableClass in dispatchableList)
        {
            T monoEventDispatchable = (T)Activator.CreateInstance(dispatchableClass as Type);
            dispatchables.Add(monoEventDispatchable);
        }
    }
}