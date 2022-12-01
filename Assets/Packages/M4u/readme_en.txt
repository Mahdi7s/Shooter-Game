//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
// Version 1.1.0
//----------------------------------------------
MVVM 4 uGUI is Asset which supported the MVVM (Model-View-ViewModel) pattern for uGUI.
It's possible to be to use MVVM 4 uGUI and improve the conservatism in the game development and development production!

[Model]
Model takes charge of game logic. Operation of the data View reflects and various logic operation of a game.
Model in this Asset is everything besides View and ViewModel (C# scripts and game objects, etc.).

[View]
View takes charge of drawing game data to a screen.
View in this Asset is uGUI (The other View elements aren't made a target.)

[ViewModel]
ViewModel takes charge of bridging to reflect the data received from Model in View. ViewModel and View are to use mechanism of data voiding, and a price reflection to View is performed automatically.
ViewModel in this Asset is MVVM 4 uGUI.

■How to use
(1)View which makes [Component/M4u/ContextRoot] the subject sets it as a game object of the top hierarchy which exists.
(2)The data to which View refers in M4uContextRoot#Context is established. Data has to be the class where I succeeded to M4uContext or M4uContextMonoBehaviour. I have to describe the variable to which View in the data refers as M4uProperty is used, and may access by a property.
(3)It's added to View which binds [component /M4u/***Binding]. The property name one in Context accesses is written in Path.
(4)ViewModel reflects the data which was set as Context changed in Model in View automatically.

■Version History

1.1.0
- add : [Tools/M4u/Show Hierarchy Icon(Hide Hierarchy Icon)]. The binding status can be confirmed in Hierarchy.
- add : [Tools/M4u/Open BindFlow]. This is a binding flow editor.
- add : [M4uActiveBinding][M4uEnableBinding][M4uToggleBinding] add [CheckType=String/Enum][CheckString]. It can be checked String or Enum. (※1)
- add : [M4uColorBinding] (※1)
- add : [M4uSpecialBindings] (※1)
- add : [M4uCollectionBinding] (※2)
- add : [M4uEventBinding][M4uEventBindings] (※3)
- add : SceneDemo3. (※1) demo scene.
- add : SceneDemo4. (※2) demo scene.
- add : SceneDemo5. (※3) demo scene.
- add : The constructor who can set defaults as M4uProperty is added.
- fix : A binding error of SceneDemo2 is corrected.
- mod : A cord of a binding error is corrected.
- del : DemoConst
- del : DemoUtil (Change DemoUtil->M4uUtil).

1.0.0
- First release
