# Unity Project Plan

## 🎯 プロジェクト概要

このプロジェクトは、キャラクターと対話しながら進行するストーリー要素を持つUnityゲームです。

branchに関して、Sceneの名前に準拠したbranchを使用して欲しい。かなりファイルディレクトリを細分化したけど、理由としてコンフリクトをなるべくおこさないようにするため。

## 📝 計画表

### 📌 **進行タスク**

#### **きらり**
- 吹き出し画像ファイルのアップロード（Google Drive）
- （↓ファイルが完成次第）
  - 吹き出しアニメーションをUnityにインポート
  - 使用する効果音が決まり次第インポート
  - 教室モデルの配置

#### **だお**
- キャラクター本体作成
- 目の作成
- アニメーション作成

#### **えび**
- キャラクター本体作成
- 目の作成
- アニメーション作成

#### **まつ**
- キャラクター本体作成
- 目の作成
- アニメーション作成

#### **とも**
- キャラクター本体作成
- 目の作成
- アニメーション作成（りり、ぽにょ含む）

#### **もしゅ**
- 背景透過画像のシェーダーエディター確認
- プロジェクト共有

#### **ゴニキ**
- 作成したモデルをGoogle Driveにアップロード

### **しゃけ**
- UnityでPlaySceneを担当

---

## 🎬 シーン概要

### **Start Scene**
- 丸が開いていくアニメーション
- ロゴと「Press A」のグラデーション表示
- だおだおが歩いたり止まったりする（アニメーション解決次第）
- Aボタン押下で別シーンへ移動
- branch名：start

### **Play Scene**
- キャラクター操作プログラム
- 話しかける機能
- 吹き出し表示機能
- 教室を歩き回り、メンバーに話しかけられるシステム
- branch名：play

#### **参考にしたい動画**
🔗 [参考動画](https://youtu.be/ta_L_qoMaqc)

### **Celebrate Scene**
- Play シーンで誰かに話しかけたらお祝いストーリー進行
- ダイアローグを使用してお祝いシーンを演出
- branch名：celebrate

### **Ending Scene**
- branch名：end

---

## 🎵 BGM・サウンド

- **スタート画面**
- **その他リアクション系**

---

## 💬 ダイアローグシーン

```
今日はみんな来てくれてありがとう！

卒業おめでとう！
みんながこれからも活躍していく様子、ワンダー一同楽しみにしてるよ。

改めてみんな卒業おめでとう！
```

---

## 📁 ファイル構成

```
Assets
┣ Project
｜ ┣ Characters（キャラクターのモデルとアニメーション、顔素材は含まない）
｜ ｜ ┣ Dao
｜ ｜ ┣ Ebi
｜ ｜ ┣ Tomo
｜ ｜ ┗ Matsu
｜ ┣ Materials ←ここにそれぞれのマテリアル
｜ ｜ ┣ Start_Material
｜ ｜ ┣ Play_Material
｜ ｜ ┣ Celebrate_Material
｜ ｜ ┗ End_Material
｜ ┣ Models（部屋など）
｜ ┣ Prefabs
｜ ｜ ┣ Start_Prefabs
｜ ｜ ┣ Play_Prefabs
｜ ｜ ┣ Celebrate_Prefabs
｜ ｜ ┗ End_Prefabs
｜ ┣ Scenes
｜ ｜ ┣ Start
｜ ｜ ┣ Play
｜ ｜ ┣ Celebrate
｜ ｜ ┗ End
｜ ┣ Scripts ←ここにそれぞれのプログラム
｜ ｜ ┣ Start_Scripts
｜ ｜ ┣ Play_Scripts
｜ ｜ ┣ Celebrate_Scripts
｜ ｜ ┗ End_Scripts
｜ ┣ Textures（キャラクターの顔素材はこちらに配置）
┣（Asset Storeなど外部から取得したもの）
┣ ...
```

---

## 💡 やりたいこと！

- **ライトニングエフェクト実装**
  - 参考動画：[Lightning Effect](https://www.youtube.com/watch?v=JVkv-hU0TmY)
