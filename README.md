# ObserverPattern
NotificationModule could be used for UI(View/Component chain)

* C#
* Open .sln by MonoDevelop. Then run.

ObserverPattern sample which can be used for the classic window system (WM_NOTIFY).
```
View (Observer/Emitter)
 => View (Observer/Emitter)
  => View (Observer/Emitter)
   => View (Observer/Emitter)
```

In case there are tons of subjects (like UIButtons x 100 on the single view), this classic event procedure style would be nicer than using something like ReactionX.

