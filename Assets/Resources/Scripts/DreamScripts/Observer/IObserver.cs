using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver<T>
{
    
    public void UpdateInfo(T info);
}
public interface ISubject <T>
{
    public void AddObserver(IObserver<T> o);
    public void RemoveObserver(IObserver<T> o);
    public void NotifyObservers(T info);

}
