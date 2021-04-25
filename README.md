# tools-tray-win

Windows 用 Web ページ・EXE をホットキーで即表示・起動できるようにするツールです。

ブラウザ開いてお気に入り探す、EXE のあるフォルダを探す、などの手間を省けます。

# ダウンロード

[Release](https://github.com/okamototomoyuki/tools-tray-win/releases) から最新バージョンをダウンロードしてください。

# 必要なもの

* .net 5 ランタイム ～

# 使い方
## 設定

### キーの登録

toolstray.exe を起動すると、タスクトレイに本ツールが常駐します。  
![image](https://user-images.githubusercontent.com/68187390/115990086-6634a580-a5fc-11eb-911f-99ac62af7d3e.png)

右クリック -> 設定 でキー設定画面を開きます。  

![image](https://user-images.githubusercontent.com/68187390/115989503-a9d9e000-a5f9-11eb-9629-a262e6ff92ec.png)

デフォルトでは下記の設定になっています。  
```
esc SettingWindow
h GoogleWindow
i https://translate.google.co.jp/?hl=ja&sl=en&tl=ja&op=translate
u https://translate.google.co.jp/?hl=ja&sl=ja&tl=en&op=translate
```

行頭にキーボードのキー、空白を開けて URL または EXE のパスを記載します。  
in + alt + 登録キーがホットキーになります。  
デフォルト設定は下記の意味になります。  

* win + alt + esc : キー設定画面
* win + alt + h : Google 検索ウインドウ（※下記参考)
* win + alt + i : Google 翻訳（日⇒英）
* win + alt + u : Google 翻訳（英⇒日）

### EXE の登録

URL 同様に EXE もパスで入力します。  
下記は win + alt + e で Sakura Editorを開く例です。  

```
e D:\software\sakura\sakura.exe
```

## 登録の反映

登録が終わったら、タスクトレイ上の本ツールを 右クリック -> 終了 し、ツールを再度起動してください。  
起動後、ホットキーは有効になります。

# 特殊な書き方

### SettingWindow

キー設定画面です。

### GoogleWindow

![image](https://user-images.githubusercontent.com/68187390/115989525-beb67380-a5f9-11eb-941d-c09301000373.png)


Google 検査を瞬時に行うための内部ツールです。  
キーワードを入力して Enter を押すとデフォルトブラウザで Google 検索結果が表示されます。

### mono

EXE は一つ開いたら、二個目は起動ではなく、単にアクティブにして欲しい場合もあります。  
そのときは EXE のパスの後ろに `mono` と書きます。  
すると、起動してなければ起動、起動済みならアクティブになるだけで、二重起動はしません。  
下記は Source Tree を起動済みならアクティブにするだけの例です。  
```
s D:\software\SourceTree\SourceTree.exe mono
```

# 設定ファイルの場所
`C:\tools-tray\` が設定ファイル置き場です。

* shortcut.txt : キー設定画面の内容
* userData : Web ページを開くときの内部ブラウザの設定情報

# 登録できないキー

Windows で既に win + alt 同時押しが設定されているキーは登録しても何も起こりません。  
全てのキーを把握できていませんが下記は登録できませんでした。他のキーに割り当ててください。  

* Y
* N

# オススメ設定

## キー設定
SourceTree と Sakura Editor を瞬時に開けるとなにかと便利です

```
esc SettingWindow
h GoogleWindow
i https://translate.google.co.jp/?hl=ja&sl=en&tl=ja&op=translate
u https://translate.google.co.jp/?hl=ja&sl=ja&tl=en&op=translate
e (サクラエディタのパス)\sakura.exe
s (SourceTreeのパス)\SourceTree.exe mono
```

## 起動設定
スタートアップにショートカットを置いておくと、起動時にいちいち実行する手間が省けます。
