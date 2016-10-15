using System;
using System.Collections;

namespace ObserverPattern
{
    // data to pass around.
    public class EmitterData: INotifyData {
        public string _data { get; private set; }

        public EmitterData(string str) {
            _data = str;
        }
    }

    // Objecy which has an Emitter.
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
            if (_emitter.HasObserver()) {
                Console.WriteLine("NotifyEvent : " + eventName + " / " + info._data);

                _emitter.NotifyEvent(eventName, data);
            }
        }
    }

    // Object which has functions as both Observer and Emitter.
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

            // The default behavior of this test transmitter would be
            // to propagate the event to observers who are looking at you.
            NotifyEvent(eventName, data);
        }
    }


    public class Test {
        public void Run() {
            RunCase1();
            RunCase2();
        }

        private void RunCase1() {
            // KING => KNIGHT => SOLDIER.
            // SOLDIER's event must be propagated to KNIGHT => be reached out KING (in serial order).
            // KNIGHT's event must be propagated to KING.

            Transmitter king = new Transmitter("KING");
            Transmitter knight = new Transmitter("KNIGHT");
            Transmitter soldier = new Transmitter("SOLDIER");

            Console.WriteLine("RUN: Case1");
            king.StartObserving(knight);
            knight.StartObserving(soldier);

            // This event should be received by KNIGHT ans then KING.
            soldier.NotifyEvent("TEST_EVENT: ", new EmitterData("SOLDIER EVENT 1"));

            // This event should be received by KING.
            knight.NotifyEvent("TEST_EVENT: ", new EmitterData("KNIGHT EVENT 1"));

            // This event should not be notified to anyone.
            king.NotifyEvent("TEST_EVENT: ", new EmitterData("KING EVENT 1"));

            king.StopObservingAll();
            knight.StopObservingAll();

            Console.WriteLine("FINISH\n");
        }

        private void RunCase2() {
            // KING A / KING B => KNIGHT.

            Transmitter kingA = new Transmitter("KING A");
            Transmitter kingB = new Transmitter("KING B");
            Transmitter knight = new Transmitter("NIGHT");

            Console.WriteLine("RUN: Case2");
            kingA.StartObserving(knight);
            kingB.StartObserving(knight);

            // This event should be received by kingA and kingB.
            knight.NotifyEvent("TEST_EVENT: ", new EmitterData("NIGHT EVENT 1"));

            kingA.StopObservingAll();
            kingB.StopObservingAll();

            Console.WriteLine("FINISH/n");
        }
    }
}

