# assert

[[xunit]]

[links](https://github.com/xunit/assert.xunit)

Assert基於程式碼的返回值、物件的最終狀態、事件是否發生等情況來評估測試的結果。 
Assert的結果可能是pass或fail。
如果所有的assert都pass，那麼整個測試就pass了；如果有任何assert fail了，那麼測試就是fail 

- Boolean：true or false 
- Collection：內容是否相等，是否包含某個元素，是否包含滿足某種條件(predicate)的元素，是否所有的元素都滿足某assert 
- Comparer 
- Equality 
- Event：Cusom events，Framework events(例如：propertyChanged) 
- Exception 
- Guards 
- Identity 
- Null 
- Property 
- Range 
- Record 
- Set 
- String： 相等/不等，是否為空，以…開始/結束，是否包含子字串，匹配正則表達示 
- Type：是否是某種型別或繼承某種型別 


一個Test裡應該有多少個asserts 
1. 每個test方法裡面只有一個assert 
2. 每個test裡面可以有多個asserts，只要這些assert都是針對同一個行為即可。 

## string.assert

String.Assert 
```csharp
[Fact] 
public void CalculateFullName() 
{ 
    var p = new Patient 
    { 
        FirstName = "Nick", 
        LastName = "Carter" 
    }; 
    Assert.Equal("Nick Carter", p.FullName); 
} 
```

## StartsWith, EndsWith 

```csharp
[Fact] 
public void CalculateFullNameStartsWithFirstName() 
{ 
    var p = new Patient 
    { 
        FirstName = "Nick", 
        LastName = "Carter" 
    }; 
    Assert.StartsWith("Nick", p.FullName); 
} 
 

[Fact] 
public void CalculateFullNameEndsWithFirstName() 
{ 
    var p = new Patient 
    { 
        FirstName = "Nick", 
        LastName = "Carter" 
    }; 
    Assert.EndsWith("Carter", p.FullName);e); 
} 
```

## contains
```csharp
Contains 

 [Fact] 
public void CalculateFullNameSubstring() 
{ 
    var p = new Patient 
    { 
        FirstName = "Nick", 
        LastName = "Carter" 
    }; 
    Assert.Contains("ck Ca", p.FullName); 
} 
```

## 正則表達式，matches

```csharp
正則表達式，Matches  

[Fact] 
public void CalculcateFullNameWithTitleCase() 
{ 
    var p = new Patient 
    { 
        FirstName = "Nick", 
        LastName = "Carter" 
    }; 
    Assert.Matches("[A-Z]{1}{a-z}+ [A-Z]{1}[a-z]+", p.FullName); 
} 
```

## 數值

```csharp
    public class Patient 
    { 
        public Patient() 
        { 
            IsNew = true; 
            _bloodSugar = 5.0f; 
        } 
 

        private float _bloodSugar; 
        public float BloodSugar 
        { 
            get { return _bloodSugar; } 
            set { _bloodSugar = value; } 
        } 
    }
```

```csharp
[Fact] 
public void BloodSugarStartWithDefaultValue() 
{ 
    var p = new Patient(); 
    Assert.Equal(5.0, p.BloodSugar); 
} 
```

## null

```csharp
[Fact] 
public void NotHaveNameByDefault() 
{ 
    var plumber = new Plumber(); 
    Assert.Null(plumber.Name); 
} 
 

[Fact] 
public void HaveNameValue() 
{ 
    var plumber = new Plumber 
    { 
        Name = "Brian" 
    }; 
    Assert.NotNull(plumber.Name); 
} 
```

## collection

```csharp
namespace Hospital 
{ 
    public abstract class Worker 
    { 
        public string Name { get; set; } 
        public abstract double TotalReward { get; } 
        public abstract double Hours { get; } 
        public double Salary => TotalReward / Hours; 
        public List<string> Tools { get; set; } 
    } 
 

    public class Plumber : Worker 
    { 
        public Plumber() 
        { 
            Tools = new List<string>() 
            { 
                "螺絲刀", 
                "扳子", 
                "鉗子" 
            }; 
        } 
 
    public override double TotalReward => 200; 
        public override double Hours => 3; 
    } 
} 

[Fact] 
public void HaveScrewdriver() 
{ 
    var plumber = new Plumber(); 
    Assert.Contains("螺絲刀", plumber.Tools); 
} 
```

## object

- 判斷是否是某個型別 Assert.IsType<Type>(xx)

```csharp
public class Programmer : Worker 
{ 
    public override double TotalReward => 1000; 
    public override double Hours => 3.5; 
} 

 
namespace Hospital 
{ 
    public class WorkerFactory 
    { 
        public Worker Create(string name, bool isProgrammer = false) 
        { 
            if (isProgrammer) 
            { 
                return new Programmer { Name = name }; 
            } 
            return new Plumber { Name = name }; 
        } 
    } 
} 

 
namespace Hospital.Tests 
{ 
    public class WorkerShould 
    { 
        [Fact] 
        public void CreatePlumberByDefault() 
        { 
            var factory = new WorkerFactory(); 
            Worker worker = factory.Create("Nick"); 
            Assert.IsType<Plumber>(worker); 
        } 
    } 
} 
```

## exception

```csharp
namespace Hospital 
{ 
    public class WorkerFactory 
    { 
        public Worker Create(string name, bool isProgrammer = false) 
        { 
            if (name == null) 
            { 
                throw new ArgumentNullException(nameof(name)); 
            } 
            if (isProgrammer) 
            { 
                return new Programmer { Name = name }; 
            } 
            return new Plumber { Name = name }; 
        } 
    } 
} 
```
 

- Assert.Throws<ArgumentNullException> 

```csharp
[Fact] 
public void NotAllowNullName() 
{ 
    var factory = new WorkerFactory(); 
    Assert.Throws<ArgumentNullException>(() => factory.Create(null)); 
} 

//可以指定引數的名稱: 
[Fact] 
public void NotAllowNullName() 
{ 
    var factory = new WorkerFactory(); 
    // Assert.Throws<ArgumentNullException>(() => factory.Create(null)); 
    Assert.Throws<ArgumentNullException>("name", () => factory.Create(null)); 
}

//檢查拋出例外訊息
public void test_gives_invalid_string_should_be_throw_exception()
{
    //Given
    Action createMoneyFromString = () =>Money.Create("adb");
    //When
    ArgumentException exception =Assert.Throws<ArgumentException>(createMoneyFromString);
    //Then
    Assert.Contains("invalid string cannot transfer to decimal", exception.Message);
} 
```



## event

- Assert.Raises<T>()第一個引數是註冊handler的Action, 第二個引數是取消handler的Action, 第三個Action是觸發event的程式碼

```csharp
public void Sleep() 
{ 
    OnPatientSlept(); 
} 
 
public event EventHandler<EventArgs> PatientSlept; 
 
protected virtual void OnPatientSlept() 
{ 
    PatientSlept?.Invoke(this, EventArgs.Empty); 
} 

[Fact] 
public void RaiseSleptEvent() 
{ 
    var p = new Patient(); 
    Assert.Raises<EventArgs>( 
        handler => p.PatientSlept += handler,  
        handler => p.PatientSlept -= handler,  
        () => p.Sleep()); 
} 
```

[//begin]: # "Autogenerated link references for markdown compatibility"
[xunit]: xunit.md "xunit"
[//end]: # "Autogenerated link references"