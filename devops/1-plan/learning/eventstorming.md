# eventstorming

- what is event storming
- the practical aspects of event stroming
- how to facilitate(促進) a workshop yourself
- deciding what to do after the workshop ends


Ubiquitous language and explained that it is not only a glossary of terms but also the system's behavior described in words.

it remains unclear how to start the knowledge crunching and how to intensify our communication with domain experts to understand the problem space better and get a proper overview of what are we going to build.

- Provide visibility during the discussion. this should remove assumptions(假設) when many people are discussing the same thing with different terms. it also eliminates(消除) some of the ambiguity(岐義、認知不同) and brings it to the surface for further exploration

- Have a modeling language that people understand. UML is not an option ,and the usual boxes and arrows have no real notation,so people can get confused and start spending time trying to clarify the meaning of things.

- involve many people simultaneously. in traditional meetings,only one person can effectively deliver the message, while everyone else needs to shut up and listen. as soon as many people start talking at the same time there is no conversation anymore. but ,assuming people with different interests and backgrounds are present in one session , they might show a lack of interest and get bored

- find a way to express terms , behavior, model processes , and decisions , not feature and data


## modeling language

the basic idea behind event storming is that it gives a straightforward modeling notation that is used to visualize the behavior of the system in a way that everyone can understand

event storming的基本概念就是利用視覺化直接了當的呈現系統運作行為，使每個人容易理解。

domain knowledge的核心概念是行為，而整個event storming就是關於找出業務是怎麼運作的。
通常，我們可以假設系統在特定時間是處於特定狀態，這個狀態會在使用者與系統進行互動時改變狀態。
在使用者會導致系統狀態改變的行為中，我們可以發現某些事發生了，而我們需要進行下一步。

ex: 使用者使用網路銀行進行付款

select payee => enter the amount => confirm the payment => sign the payment

從付款者角度出發，他們的帳戶金額減少，付款動作完成，並且可以將確認付款的帳單視為已處理並可以丟棄

從收款者角度出發，接收到的金額要與開出的發票金額相等，或其他資訊相符

使用者在系統所執行每個動作都造成狀態變化，付款單被建立與簽名，付款者的帳戶減少特定金額，收款人的帳戶中增加等值的金額，帳單會被標記已付款，這而這些作業成為「已發生既定事實」，除非有時光機，否則無法還原這些操作，除了特定情況下，進行退款或其他業務行為，但也是在以上的事實基礎上再繼續進行作業，ex:可能金額計算錯誤，將進行退款，雙方帳戶餘額再次進行計算。
以上的已發生的事件就是 domain event, 這是 event storming中最基礎也是最重要的概念。這些domain event對於大家並不會難以理解，它們不是需求，也不是某人想要做什麼，也不是一個按鈕，而是事件將會發生並完成。每個 domain event都呈現一個既定事實，而我們試圖在系統中建立它。

在創造 domain event的第一個概念，每個事件都是一個特定顏色的便條紙，顏色的區分是必要的因為針對該model會繼續產生更多的想法，而為了避免混淆，我們需要用顏色在不同的model中表示同類型的想法。

創作者建議domain event使用`橘色`的便條紙

如以下2個事件是按照順序發生的，消費者使用信用卡付款，然後付款完成。
** 由一個主詞(subject)與謂詞(verb)所組成，且 verb是過去完成式，代表已經發生了某件事並且成為既定事實。
ex: 
- customer paid by card
- order confirmed

回到payment example將會得到：

                                                                            payment account debited
payment order created => payment order signed => payment order approved =>  payment account credited => incoming payment recorder =>payment matched with a bill => bill marked as paid

1. 依照發生時間順序排列:由左至右

2. 可能不只一個系統，我們是在為業務流程建模，但至少有3個可以被清楚識別的區塊
    1. 面向使用者的的系統，crates and signs payment orders
    2. 完成這個交易的銀行後台系統
    3. 付款者帳戶配對帳單的系統