using System.Collections;
using System.Collections.Generic;

namespace ObserverPattern
{
    // Interface for Event data.
    public interface INotifyData {
    }

    // Interface for Observer.
    public interface INotifyObserver {
        void onNotify(string eventName, INotifyData data);
    }

    // Interface for Emitter.
    public interface INotifyEmitter {
        NotifyEmitter GetNotifyEmitter();
    }

    // NotifyObserver.
    public class NotifyObserver {
        INotifyObserver _owner;
        List<INotifyEmitter> _targets;

        public NotifyObserver(INotifyObserver owner) {
            _owner = owner;
            _targets = new List<INotifyEmitter>();
        }

        ~NotifyObserver() {
            StopObservingAll();
        }

        public bool StartObserving(INotifyEmitter emitter) {
            if (emitter.GetNotifyEmitter().AddObserver(_owner)) {
                _targets.Add(emitter);
                return true;
            }
            return false;
        }

        public bool StopObserving(INotifyEmitter emitter) {
            if (emitter.GetNotifyEmitter().RemoveObserver(_owner)) {
                _targets.Remove(emitter);
                return true;
            }
            return false;
        }

        public void StopObservingAll() {
            _targets.ForEach((emitter) => {
                emitter.GetNotifyEmitter().RemoveObserver(_owner);
            });
            _targets.Clear();
        }
    }

    // NotifyEmitter.
    public class NotifyEmitter {
        protected List<INotifyObserver> _observers;

        public NotifyEmitter() {
            _observers = new List<INotifyObserver>();
        }

        ~NotifyEmitter() {
            RemoveObserverAll();
        }

        public bool AddObserver(INotifyObserver observer) {
            if (HasObserver(observer)) {
                return false;
            }
            _observers.Add(observer);
            return true;
        }

        public bool RemoveObserver(INotifyObserver observer) {
            if (HasObserver(observer)) {
                _observers.Remove(observer);
                return true;
            }
            return false;
        }

        public void RemoveObserverAll() {
            _observers.Clear();
        }

        public bool HasObserver() {
            return (_observers.Count > 0);
        }

        public bool HasObserver(INotifyObserver observer) {
            return _observers.Contains(observer);
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
}
