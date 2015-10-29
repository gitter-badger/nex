#Commit Rule#

コミットをわかりやすく管理するためにいくつかのルールを設けたいと思います。  

---

##書式##
###基本的に英語###
基本的に英語でお願いします。単語を並べるだけでも大丈夫です。


###大文字で始める###
文の最初を大文字にして下さい。それ以外はなるべく小文字でお願いします。  

ex.  
>Added user view


###ピリオドをつけない###
文の最後のピリオドは付けないで下さい。これはバージョン表記と間違えないようにするためです。  

ex.
>Changed A to B


###言い回し###
####コミットの際####
動詞の過去形で始めて下さい。  
「…を～しました」という文にしてください。  

ex.
>Added  
>Fixed  
>Removed  
>Changed  
>Updated

####仕様変更等の際####
now + 動詞の過去分詞で書いて下さい。  
「…は～するようになります」 という文にして下さい。

ex.
>Minimized windows are now hidden  
>(最小化されたウィンドウは今後は隠されます)

---

##記号##
＋ 新規追加 add, added  

↗ 改善 improve, improved  

⇄ 変更 change, changed

➤ 修正 modify, modified  

＊ バグフィックス fix, fixed  

－ 削除 remove, removed  

↶ 取り消し revert, reverted  

↺ 更新 update, updated  

⇈ 拡張 upgrade, upgraded  

✓ 有効 enable, enabled  

☓ 無効 disable, disabled

↔ 拡張 extend, extended

⤴ 強化、向上 enhance, enhanced

！ 実装 implement, implemented

＃ リファクタ refactor, refactored

／ 最適化 optimize, optimized

✂ 削減、切り詰め reduce, reduced

---

##参考##
なんかこっちを読んだほうが早い気もします。  
[ChangeLog を支える英語](https://gist.github.com/hayajo/3938098)