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
    private List<IEnableAndDisable> _enableAndDisable = new List<IEnableAndDisable>();

    void Awake() 
    {
        Type[] types = Assembly.GetExecutingAssembly().GetTypes();
        if(types.Length == 0) return;
        //IEnumerable<IMono> IMonoD = (IEnumerable<IMono>)types.Where(t => typeof(IMono).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
        //IEnumerable<IUpdate> IUpdatesDispatcher = (IEnumerable<IUpdate>)types.Where(t => typeof(IUpdate).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
        List<IEnableAndDisable> IEenableAndDisableDispatcher = (List<IEnableAndDisable>)types.Where(t => typeof(IEnableAndDisable).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        //DispatchleHandler(ref _dispatchables, in IMonoD);
        //DispatchleHandler(ref _updates, in IUpdatesDispatcher);
        DispatchleHandler(ref _enableAndDisable, in IEenableAndDisableDispatcher);
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
        foreach (var enableAndDisable in _enableAndDisable){
            enableAndDisable?.OnEnable();
        }
    }
    void OnDisable()
    {
        foreach (var enableAndDisable in _enableAndDisable){
            enableAndDisable?.OnDisable();
        }
    }
    void DispatchleHandler<T>(ref List<T> dispatchables, in List<T> dispatchableList)
    {
        foreach (var dispatchableClass in dispatchableList)
        {
            T monoEventDispatchable = (T)Activator.CreateInstance(dispatchableClass as Type);
            dispatchables.Add(monoEventDispatchable);
        }
    }
}