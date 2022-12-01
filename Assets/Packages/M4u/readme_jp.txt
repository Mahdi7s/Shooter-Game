//----------------------------------------------
// MVVM 4 uGUI
// © 2015 yedo-factory
// Version 1.1.0
//----------------------------------------------
MVVM 4 uGUI は、uGUIにMVVM(Model-View-ViewModel)パターンをサポートしたAssetである。
MVVM 4 uGUI を使用することで、ゲーム開発における保守性・開発生産性を向上させることが可能である!

[Model]
ゲームロジックを担当する。Viewに反映するデータの操作や、ゲームに関わる様々なロジック操作など。
このAssetにおけるModelは、ViewとViewModel以外の全ての要素を指す(C#スクリプト、ゲームオブジェクト、等)。

[View]
ゲームデータの画面への描画を担当する。
このAssetにおけるViewは、uGUIを指す(他のView要素はターゲットとしていない)。

[ViewModel]
Modelから受け取ったデータをViewに反映するための橋渡しを担当する。ViewModelとViewはデータバイディングの仕組みを利用することで、自動的にViewへの値反映が行われる。
このAssetにおけるViewModelは、MVVM 4 uGUI が提供する機能を指す。

■使用方法
(1)[Component/M4u/ContextRoot]を対象とするViewが存在するトップ階層のゲームオブジェクトに設定。
(2)M4uContextRoot#ContextにViewが参照するデータを設定。データはM4uContextかM4uContextMonoBehaviourを継承したクラスでなければならない。また、データ内のViewが参照する変数はM4uPropertyを使用し、プロパティでアクセスするように記述しなければならない。
(3)[Component/M4u/***Binding]をバインドするViewに追加。PathにContext内のアクセスするプロパティ名を記述。
(4)以上で、Modelで変更されたContextに設定されたデータを、ViewModelが自動的にViewに反映する。

■バージョン履歴

1.1.0
- add : [Tools/M4u/Show Hierarchy Icon(Hide Hierarchy Icon)]追加。バインド状況をHierarchy上から確認出来るように。
- add : [Tools/M4u/Open BindFlow]追加。バインドフローを確認出来るエディタ。
- add : [M4uActiveBinding][M4uEnableBinding][M4uToggleBinding]に[CheckType=String/Enum][CheckString]を追加。StringとEnumで判定が可能に(※1)
- add : [M4uColorBinding]追加(※1)
- add : [M4uSpecialBindings]追加(※1)
- add : [M4uCollectionBinding]追加(※2)
- add : [M4uEventBinding][M4uEventBindings]追加(※3)
- add : SceneDemo3追加。(※1)のデモシーン。
- add : SceneDemo4追加。(※2)のデモシーン。
- add : SceneDemo5追加。(※3)のデモシーン。
- add : M4uPropertyに初期値を設定できるコンストラクタを追加
- fix : SceneDemo2でのバインドエラーを解消
- mod : バインドエラー時の処理を修正
- del : DemoConst削除
- del : DemoUtil削除(DemoUtil->M4uUtilへの変更)

1.0.0
- 初期バージョン
