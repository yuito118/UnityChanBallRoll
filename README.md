# UnityChanBallRoll

--------------------------------------------------------------------------
## 環境配置（ステップ１）
- 新プロジェクトを作成
- 3Dモードを確認後、スタート
- 2by3にして、Project View を One Column Layout に
 - さっそくシーンの保存
  - フォルダ名に「_Scenes」
    - 説明：「_」を最初につけておくと検索しやすい
  - MiniGame
    - 説明：なんでもいい

 - Ground
  - GameObject->3D Object -> Plane
  - 名前Ground
  - 歯車アイコンからReset
    - 説明：0,0,0になる
  - Fキーでフォーカス
    - 説明：またはダブルクリックで
  - Scale(2,1,2)
 - Player
  - GameObject->3D Object -> Sphere
  - Playerに
    - 説明：名前変更のやり方は２種類
  - Fキー
  - Reset
  - 左上の十字アイコンクリックして、移動モードに
  - ↑をドラッグ　Yを0.5ぐらい
 - Material
  - Material フォルダを作成
  - New Material作成
  - 名前をBackground
  - Albedo の Colorを(0,32,64)
  - シーンビューのGroundオブジェクトにD＆D
 - Directional Light
  - Rotation Y を 60 に

--------------------------------------------------------------------------
## Player設定（ステップ２）
 - Rigidbody
  - Player を選択
  - Component -> Physics -> Rigidbody （Rigidbody がインスペクターで観れますね）
    - 説明：▲のオンオフでキズモが消えたりついたり
 - スクリプト作成
  - Scripts フォルダを作成
  - Player を選択 → Add Component → New Script → PlayerController (C Sharp) → Enterキー
  - 場所をルートからScriptsフォルダに移動
  - ヒエラルキーのPlayerController を ダブルクリック
 - スクリプトを書く
  - 説明
    - Update は描画フレームごとに呼ばれる関数
    - FixedUpdate は物理の更新の際に呼ばれる
    - なので、今回はFixedUpdateに書く

        ```
void FixedUpdate()
{
    Input
}
        ```

   - Googleで 「Unity Input」で検索
  - Inputページの下の方の GetAxis をクリック
  - 引数の意味、書いてある内容、サンプルコード
  - Scriptに戻って

    ```
void FixedUpdate()
{
    float moveH = Input.GetAxis(“Horizontal”);
     float moveV = Input.GetAxis(“Vertical”);
}
    ```

  - 上の説明

    ```
void FixedUpdate()
{
    float moveH = Input.GetAxis(“Horizontal”);
    float moveV = Input.GetAxis(“Vertical”);
    Rigidbody
}
```

  - 上の検索欄でRigidbodyを検索
  - AddForce へ
  - 説明
    - 第１引数force と第２引数mode
    - Vectorというのは空間座標を定める時に使われる重要なクラス
 - 説明
  - Component の説明
    - Unityの画面に戻って、Player のインスペクターにPlayer Controller というスクリプトがくっついている。これをコンポーネントという
    - このPlayer Controller から Rigidbody をアクセスするにはいくつか方法があります。
    - いろいろな取得の方法
    - GameObject.Find
    - public Rigidbody rb; からのインスペクターでアタッチ
    - GetComponent
    - それぞれのメリット、デメリット
    - 今回はドキュメントに書いてある通りにやってみよう
    - ドキュメントに書いてあるサンプルコードを説明

        ```
    private Rigidbody rb;
    
    void Start()
    {
          rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        float moveH = Input.GetAxis(“Horizontal”);
        float moveV = Input.GetAxis(“Vertical”);
    
        Vector3 move = new Vector3 ( moveH, 0.0f, moveV);
    
        rb.AddForce(move);
    }
```

 - では動かしてみましょう。
  - 上部の▲ボタンでプレイモード
  - 上下左右キーで動きますか？→遅い
 - 説明
  - コードに戻って

          rb.AddForce(move * 100);

  - とかだと厄介。毎回コンパイル。面倒臭い

          public float speed;

  - として、

          rb.AddForce(move);

　　　を

          rb.AddForce(move * speed);

　　　と変更

  - Unityにもどる
  - speed を100に　→　早すぎる
  - 10に　→　ちょうどいいね

--------------------------------------------------------------------------
## Camera（ステップ３）
 - 親子構造
  - MainCamera Position Y を10、Rotation X を45
  - まずはMainCamera を子にしてみよう
  - エディタ上でPlayer を動かすとカメラがくっついてくる
  - じゃあプレイしてみましょう→こりゃアカン。回転もくっついてきちゃうから
  - というわけで子ではいけないのです。スクリプトじゃないと
 - Main Camera を選択、Add Component → C# Script “CameraController"
  - 場所をルートから変更

```
    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }
    
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
```

 - 説明
  - Update は本当はいけない。毎回描画フレームに行われるので、Cameraが先か、Playerが先かで挙動が変わる
  - というわけで、LateUpdate にする。これは全てのUpdateが終わった後に行われる関数

 - void Update → void LateUpdate()
 - Camera Controller のPlayer にPlayerオブジェクトをD＆D
 - プレイでカメラ付いてくる？

--------------------------------------------------------------------------
## 障害物（ステップ４）
 - GameObject →  Create Empty Child → 名前を「Walls」
 - フォルダ代わり
 - Position, Rotation(0,0,0)
 - GameObject → 3D Object → Cube →名前を「West Wall」　→ Reset
  - Wallsの子にする
  - Fキー　→　Scale(0.5, 2, 20.5)　→　Position X = -10
 - Duplicate → East Wall →　Position　X＝10
 - Duplicate →　North Wall →　Reset → Scale (20.05, 2, 0.5) → Position Z = 10
 - Duplicate →　South Wall →　Position Z = -10
 - 移動モードを Global → Local にして、プレイして転がる様子がわかる

--------------------------------------------------------------------------
## Prefab（ステップ５）
 - Player のInspector の一番上のチェックを外すと消える。非Active化。
 - Item を置く
  - GameObject → 3D Object → Cube →名前を「Item」　→ Reset
  - Position Y = 0.5 ・Scale (0.5, 0.5, 0.5) ・Rotate(45, 45, 45)
  - Add Component → New Script → Rotator
  - Scripts へ移動
  - ダブルクリック
 - Script 変更
  - transform　で検索

```
    void Update()
    {
         transform.Rotate( new Vector3 (15, 30, 45) * Time.deltaTime);
    }
```

  - プレイモードで動く？
 - Prefab化
  - 説明
    - Prefab化しておくと、あとで全部に適応できるから便利
    - Unityの代名詞的機能
  - Projectビューで何もないところをクリックして、ハイライトをオフ
  - Create → Folder 名前を「Prefabs」
  - Hierarchyビューの「Item 」をProjectニューのPrefabsフォルダにD＆DしてPrefab化
 - GameObject → Create Empty Child → 名前を「Items 」→Reset
  - Item をItems の子に
 - 説明
  - 移動モードがLocalだと移動が
 - 移動モードを Local → Global に　→　地面に平行移動可能に
 - Cmd+D or Ctrl+D でDuplicate →１２個作る→プレイ
 - Background のMaterial をDuplicate して、Item に→色を黄色に
 - Prefabに適応するのは二つやり方がある
  - Item にD&Dしたあと、Inspector の右上のApplyボタンで反映
  - Prefabに直接D&D

--------------------------------------------------------------------------
## 物理運動（ステップ６）
 - Player Active化
 - Collider を検索
  - OnTriggerEnter のコードをコピー
 - PlayerController を開く
  - 最後にペースト

```
    void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
    }
```

 - 説明
  - OnTriggerEnter は接触した時呼ばれる、other は相手側
 - 非Active化で良いので、Destroy の部分を一旦削除
 - GameObjectで検索　→　CompareTag SetActiveを選択

```
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(“Item”) )
        {
             other.gameObject.SetActive(false);
        }
    }
```

  - Unityに戻ってエラーがないことを確認
 - Item の上のTag →Add Tag… 選択　→　Itemを追加
  - 再びItemの上のTag→Itemを選択
 - Unityでプレイ→反応しない（聞いてください時間）
  - 説明
    - Player と Item を選択してRenderer をオフにしてみる→すると緑の線が出てくる
    - Unity の物理挙動は静的か動的かの計算をしている
    - 静的コリジョンを持ってる物体は物理運動に影響を受けない。逆に動的は受ける
    - このItemはTriggerに反応していないから
    - これを使えば部屋に入った瞬間メッセージ出したりできる
  - Triggerをチェック
 - プレイしてみよう
 - できたー。でも実は一つ問題が（聞いてください時間）
 - 説明
  - 実はItem が回転するたびにstaticオブジェクトとして再計算されている
    - これはRigidbodyがついていないから
 - というわけでRigidbodyをつけよう
 - プレイしてみよう→落ちるw→止める
  - Use Gravityをオフにしてもいいけども、ここはIs Kinematic をオンにしよう（聞いてください時間）
    - こうすると物理運動には従わないが、アニメーションはしてくれるようになる
    - エレベーターみたいに落下はしないが、挙動は制御、ということが可能
 - プレイしてみよう、うまくいく？

--------------------------------------------------------------------------
## カウント（ステップ７）
 - PlayerController.cs を立ち上げる
  - 引数に以下の文を
          private int count;
  - Start に以下の文を
          count = 0;
  - void OnTriggerEnter のif 文の中に
          count = count + 1;
 - 表示するUIを作る
  - Create → UI → Text　→ CountText に変更
  - 色を白に。Textを「Count Text」に。
  - Anchorをクリック
    - Shift+Altを押しながら左上のボタンを押す　→左上の場所に
    - 左上の角すぎるので、PosX 10 Pos Y -10 にして、ちょっと間を空ける
 - PlayerController に戻る
  - using UnityEngine.UI を追加

          public Text countText;

  - Startに

          countText.text = “Count: “ + count.ToString();

  - OnTriggerEnter のIf文に

          countText.text = “Count: “ + count.ToString();

  - うーん、同じ文書くのはいただけないね。

```
          void SetCountText()
          {
               countText.text = “Count: “ + count.ToString();
          }
```

  - SetCountText()に変更
 - Unityに戻る
  - Player Controller の Count TextにCountText をD&D

--------------------------------------------------------------------------
・ビルド（ステップ８）
