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


## Visualization

如上述我們可發現一個簡單的業務模型已經可以提供參與者很多有用的資訊。不只是識別出在付款過程中所發生的事件，也同樣的展現了一個由左至右的執行過程且可以將執行部份簡化為3個不同的系統。
event storming在視覺化建模技術上也能達到。當人們得到一個big picture時，會開始按照職能、個人特質等，開始有不同的 what if，此時這個簡單的流程就會逐漸被補充完成，且可能沒有結束的那一天。
藉由啟發性思考，(what you see is all there is?)，我們將事物的簡單理解為 everything works as it should.there are no exceptions and edge cases, 人們不會無意或故意犯錯，但在實際環境是複雜且多變的，大部份的時間都在處理例外情形與edge case,並且視為正常流程或事件。
當例外情形與edge cases可以視覺化時，可以使人們更易觀察、去思考。

- what if : 如果帳戶餘額不足時會怎麼樣?
- what if : 如果帳號上的金額是錯誤的？
- what if : 如果收款人的帳號是錯誤的？

有限的空間會對於想要盡最大努力去建模的人們造成損害，想像大家圍著一張桌子溝通，如同我們說過的，這並不是event storming，我們期望人們在房間內四處移動針對有興趣的議題溝通，且這個情況可能同時發生在好幾處，所以我們需要空間，
我們並不是一個happy path且沒有任何例外與edge case，我們身處在複雜多變的真實世界中，傳統的2-3公尺白板的空間有限無法盡可能的展現出所有的問題。

如果白板的空間不足會發生什麼事，人們會開始將剩下的空間視為珍貴的，將會開始節省空間，有一些事件、想法開始變得不重要或是順位往後移動，這是很正常的,人們會根據當下的情況來做出去符合情況的決策，在有一個有限空間的限制下，我們將會得到一個有限的業務模型，請注意為你的議題提供與會者足夠的空間來進行 event storming
there is one issue here , which can do a disservice to those who are trying their best to create a proper events model. you could imagine that such a workshop happens in a meeting room. 
usually, people set around a table and talk.as we have already suggested, this is not how eventstorming works. we expect people to move around the room and be actively engaged in conversations,which might happen simultaneously at different sides of the room. so , we need some space. but this is not all the space we need. have a good look the happy path and no edge cases and exceptions are covered,the real-life process is way more complicated; this diagram already takes some horizontal space. now ,imagine real-world scenarios being modeled like this. indeed , a traditional two-three meter whiteboard would do a disservice for you

## facilitating an eventstorming workshop
- 不需要在event storming會前，在組織倡論有關DDD的概念。
- 這個簡單有效的方法可以幫助你在進行ddd，或即使你並沒有打算為專案進行ddd，也將會幫助你可以在更了解domain(業務邏輯)的情況下建造系統。
- 在雙方公開討論問題時將會共同尋找解決方法，能增加開發者與潛在使用者之間的聯繫。

## who to invite

people with questions and people with answer

- people with questions
    - developers
    - architects
    - business analysts
    - requirements analysts

this group needs to study the information about the domain that is already available(general understanding,perhaps requirements of specifications if they have already been made) and prepare questions.

- people with answer
    - domain experts

他們不是清楚所有細節，也會有對業務知識有錯覺。
需要來自各部門代表來參與會議
domain experts的會議時間非常少，應找出know how things are done, not know how things should be done.

會議中雙方都必須在會前準備好可以明確途述的問題
那些issue需要被解決，或issue需要更具體的描述
了解自己的需求並擁有足夠的訊息以將這些需求傳達給開發者，使他們能夠根據這些資訊撰寫程式碼

## preparing the space

- 前置準備
    1. sticky note (大量不同顏色不同尺寸便利貼) : 可以貼在任意表面，且容易撕下，不會對後來者使用會議室時造成困擾
    2. paper roll (繪圖用紙捲): 寬且長、高品質的記錄白紙
    3. 固定架 : 可將白紙固定在牆壁上的工具，牆壁最好不要有任何東西，窗戶，門等他他障礙物
    4. marker pen (馬克筆) : 粗細適中，且足夠多，每個人都能夠分到一枝筆，且仍有空餘
    5. 會議室 : 不同於傳統會議室，有一張大桌子很多椅子，大家坐在一起討論，event storming需要足夠的空間提供張貼紙捲，人們自由走動討論，請把所有桌椅移置他處或是統一放置於角落，且在角落提供一張桌子擺放紙筆等必要物品
    6. 足夠的點心、飲料 : 不同於其他會議方式，大家可能會激烈的討論或四處移動，很有趣但也很累，提供足夠的食物補充能量，且也能讓與會者的在這場特殊會議的體驗更好。

## the workshop

- 時間與議程
    1. timing and scheduling : 約2小時的會議時間，中間提供10分鐘休息時間
    2. 會前說明 : 解釋規則，並將紙筆分配給每個人，在白板寫下domain event  something happened
    3. 提供1-2個範例，向參與者說明如何進行，甚至可以寫下錯誤的event，提供參與者去糾正，讓大家寫下自己的意見並貼在想貼的地方，第一張便利貼可以是流程中的某一步，而不是起點或終點，參與者會開始將剩餘的事件一邊討論一邊補完
    4. 由於參與者可能未參與過event storming的體驗，主持人需要打破僵硬的氣氛，做為第一個寫下sticky note，並且貼上，引導人們開始討論
    5. alberto brandolini : you can put anything you want anywhere you want,but not at the start. 
        1. 我們總是花太多時間討論從那裡開始，這對event storming 毫無產出。 
        2. 在沒有任何確定的起點之前，直接將第一個sticky note放在中間某地方，然後從這裡開始工作進行延伸思考。

## during the workshop

- 會議中注意事項
    1. 做為主持人的角色不是管理，而是觀察與引導，但有幾項事情需注意
        1. 人們會傾向問主持人問題，他們會將會議主持者視為擁有更多資訊與權限的人，你的職責是將他們引導至也能回答此問題的受邀者面前
        2. 參與者可能對於domain event不熟悉，會將需求(payment processing or shopping cart)、動作(process payment or register customer)寫在便利貼上並且貼上，主持人需避免這種情況並對他們解釋目標是描述一個流程中的domain event，它是必然發生且勢必完成，不可移除或改變的
        3. 當人們進入狀況開始進行討論與張貼便利貼時，不要去移除重覆的便利貼或是去統整它，也不要說參與者張貼的部份是錯誤的，此時是大家溝通消除誤解的時機
    2. 為複雜的討論做好準備
    3. 試著聚焦在edge cases : 對於開發者而言，他們不清楚針對這些情境應該如何處置，但業務邏輯通常有固定的標準作業程序來應對這些情境，它們應該要被建置在系統中。 ex: what if payment has failed? what if the payment amount does not cover the full order total ...etc
    4. 識別與歸納 hotspots可以讓你推遲決策，中斷爭執，讓小組討論繼續前進，而不會卡住。hotspots需要被密切關注，但應在會議完成，打斷無用的爭論迴圈，貼上粉紅色便利貼，要求人們繼續。
        1. 討論edge cases常會產生模糊地帶，不是所有的例外情況都能完全覆蓋業務流程，當人們對於某些domain event持有不同意見時，將該便利貼重新寫下並使用顯眼的顏色(原創者使用粉紅色)，提供人們可以注意到有模糊不清的議題需要定義清楚(hot spots)。
    5. 會議中可能發現有幾乎不與其他群體產生聯繫的 event island or clouds of events =>context map ，順其自然請勿打斷他們。 特徵:在裡面的內部聯結中，通常會有部份事件屬於一個或以上的domain event。
    6. external systems :可能會發現部份domain event與外來系統有關，(作者使用大張柔和的粉紅色歸類並視覺化)
    7. 為了能獲取最大的modeling space，請人們盡量不要留白
    8. 當參與者沒有想法，陷入安靜時，你可以站在不同的觀點引起他們的討論
        1. 要求人們將時間線向後移動，可能有部份被遺漏，被人們認為不重要的東西不會被貼上去。
        2. 識別創造價值的地方，(ex:關注金錢的流向)
    9. 每小時提供休息時間，請勿超過計畫的會議時間。
## after the workshop

收集所有紙卷，使用全景攝影，將圖片寄給所有人。


## 相關連結
[event-storming](EventStorming.com)
[Introducing-EventStorming](https://leanpub.com/introducing_eventstorming)

[event-storming-session](https://selleo.com/blog/how-to-run-your-first-event-storming-session)