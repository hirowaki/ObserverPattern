using System.Collections;
using System.Collections.Generic;

// Interface for Event data.
public interface INotifyData {
}

// Interface for Event Observer.
public interface INotifyObserver {
    void onNotify(string eventName, INotifyData data);
}

// Interface for Event Emitter.
public interface INotifyEmitter {
    NotifyEmitter GetNotifyEmitter();
}

// NotifyObserver.
public class NotifyObserver {
    INotifyObserver _owner;

    public NotifyObserver(INotifyObserver owner) {
        _owner = owner;
    }

    public bool StartObserving(INotifyEmitter emitter) {
        return emitter.GetNotifyEmitter().AddObserver(_owner);
    }

    public bool StopObserving(INotifyEmitter emitter) {
        return emitter.GetNotifyEmitter().RemoveObserver(_owner);
    }
}
    
// NotifyEmitter.
public class NotifyEmitter {
    protected List<INotifyObserver> _observers = new List<INotifyObserver>();

    public bool AddObserver(INotifyObserver observer) {
        if (_observers.Contains(observer)) {
            return false;
        }
        _observers.Add(observer);
        return true;
    }

    public bool RemoveObserver(INotifyObserver observer) {
        if (_observers.Contains(observer)) {
            _observers.Remove(observer);
            return true;
        }
        return false;
    }

    public void NotifyEvent(string eventName, INotifyData data) {
        Queue<INotifyObserver> queue = new Queue<INotifyObserver>();

        _observers.ForEach((observer) => {
            queue.Enqueue(observer);
        });

        while (queue.Count > 0) {
            _NotifyEvent(queue.Dequeue(), eventName, data);
        }
    }

    void _NotifyEvent(INotifyObserver observer, string eventName, INotifyData data) {
        if (_observers.Contains(observer)) {
            observer.onNotify(eventName, data);
        }
    }
}

