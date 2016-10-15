using System;
using System.Collections;

namespace ObserverPattern
{
    public class EmitterData: INotifyData {
        public string _data { get; private set; }

        public EmitterData(string str) {
            _data = str;
        }
    }

    public class Emitter: INotifyEmitter {
        protected NotifyEmitter _emitter;

        public Emitter() {
            _emitter = new NotifyEmitter();
        }

        public NotifyEmitter GetNotifyEmitter() {
            return _emitter;
        }

        public void NotifyEvent(string eventName, INotifyData data) {
            var info = data as EmitterData;
            Console.WriteLine("NotifyEvent : " + eventName + " / " + info._data);

            _emitter.NotifyEvent(eventName, data);
        }
    }

    // Observer / Emitter.
    public class Transmitter: Emitter, INotifyObserver {
        protected NotifyObserver _observer;
        private String _name;

        public Transmitter(string name) {
            _observer = new NotifyObserver(this);
            _name = name;
        }

        public bool StartObserving(INotifyEmitter emitter) {
            return _observer.StartObserving(emitter);
        }

        public bool StopObserving(INotifyEmitter emitter) {
            return _observer.StopObserving(emitter);
        }

        public void StopObservingAll() {
            _observer.StopObservingAll();
        }

        public void onNotify(string eventName, INotifyData data) {
            EmitterData info = data as EmitterData;

            Console.WriteLine(_name + "::onNotify : " + eventName + " / " + info._data);

            NotifyEvent(eventName, data);   // propagate the event to observers who are looking at you.
        }
    }


    public class Test {
        public void Run() {
            Transmitter compnent1 = new Transmitter("KING");
            Transmitter compnent2 = new Transmitter("KNIGHT");
            Transmitter compnent3 = new Transmitter("SOLDER");

            Console.WriteLine("START");
            compnent1.StartObserving(compnent2);
            compnent2.StartObserving(compnent3);

            compnent3.NotifyEvent("TEST_EVENT: ", new EmitterData("SOLDER EVENT 1"));
            compnent3.NotifyEvent("TEST_EVENT: ", new EmitterData("SOLDER EVENT 2"));
            compnent3.NotifyEvent("TEST_EVENT: ", new EmitterData("SOLDER EVENT 3"));
            compnent3.NotifyEvent("TEST_EVENT: ", new EmitterData("SOLDER EVENT 4"));

            compnent2.NotifyEvent("TEST_EVENT: ", new EmitterData("KNIGHT EVENT 1"));

            compnent1.NotifyEvent("TEST_EVENT: ", new EmitterData("KING EVENT 1"));

            compnent1.StopObservingAll();
            compnent2.StopObservingAll();

            compnent3.NotifyEvent("TEST_EVENT: ", new EmitterData("KING EVENT 1"));

            Console.WriteLine("FINISH");
        }
    }
}

