# xunit

## sample

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

## inlineData example

```csharp
public class StringTests1 
{ 

    [Theory, 
    InlineData("goodnight moon", "moon", true), 
    InlineData("hello world", "hi", false)] 
    public void Contains(string input, string sub, bool expected) 
    { 
        var actual = input.Contains(sub); 
        Assert.Equal(expected, actual); 
    } 
} 
```

## propertydata example

```csharp
public class StringTests2 
{ 

    [Theory, PropertyData("SplitCountData")] 
    public void SplitCount(string input, int expectedCount) 
    { 
        var actualCount = input.Split(' ').Count(); 
        Assert.Equal(expectedCount, actualCount); 
    } 

    public static IEnumerable<object[]> SplitCountData 
    { 
        get 
        { 
            // Or this could read from a file. :) 
            return new[] 
            { 
                new object[] { "xUnit", 1 }, 
                new object[] { "is fun", 2 }, 
                new object[] { "to test with", 3 } 
            }; 
        } 
    } 
} 
```

## classdata example

```csharp
public class StringTests3 
{ 
    [Theory, ClassData(typeof(IndexOfData))] 
    public void IndexOf(string input, char letter, int expected) 
    { 
        var actual = input.IndexOf(letter); 
        Assert.Equal(expected, actual); 
    } 
} 

  

public class IndexOfData : IEnumerable<object[]> 
{ 

    private readonly List<object[]> _data = new List<object[]> 
    { 
        new object[] { "hello world", 'w', 6 }, 
        new object[] { "goodnight moon", 'w', -1 } 
    }; 

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => return GetEnumerator();
} 
```

## complex class data example

```csharp
public interface ITheoryDatum 
{ 
    object[] ToParameterArray(); 
} 

public abstract class TheoryDatum : ITheoryDatum 
{ 
    public abstract object[] ToParameterArray(); 
 

    public static ITheoryDatum Factory<TSystemUnderTest, TExpectedOutput>(TSystemUnderTest sut, TExpectedOutput expectedOutput, string description) 
    { 
        var datum= new TheoryDatum<TSystemUnderTest, TExpectedOutput>(); 
        datum.SystemUnderTest = sut; 
        datum.Description = description; 
        datum.ExpectedOutput = expectedOutput; 
        return datum; 
    } 
} 
```

```csharp
public class TheoryDatum<TSystemUnderTest, TExecptedOutput> : TheoryDatum 
{ 
    public TSystemUnderTest SystemUnderTest { get; set; } 
 
    public string Description { get; set; } 

    public TExpectedOutput ExpectedOutput { get; set; } 

    public override object[] ToParameterArray() 
    { 
        var output = new object[3]; 
        output[0] = SystemUnderTest; 
        output[1] = ExpectedOutput; 
        output[2] = Description; 
        return output; 
    } 
} 
```

```csharp
public class IngredientTests : TestBase 
{ 
    [Theory] 
    [MemberData(nameof(IsValidData))] 
    public void IsValid(Ingredient ingredient, bool expectedResult, string testDescription) 
    { 
        Assert.True(ingredient.IsValid == expectedResult, testDescription); 
    } 
 

    public static IEnumerable<object[]> IsValidData 
    { 
        get 
        { 
            var food = new Food(); 
            var quantity = new Quantity(); 
            var data= new List<ITheoryDatum>(); 
            data.Add(TheoryDatum.Factory(new Ingredient { Food = food }                       , false, "Quantity missing")); 
            data.Add(TheoryDatum.Factory(new Ingredient { Quantity = quantity }               , false, "Food missing")); 
            data.Add(TheoryDatum.Factory(new Ingredient { Quantity = quantity, Food = food }  , true,  "Valid")); 

            return data.ConvertAll(d => d.ToParameterArray()); 
        } 

      } 
} 
```

## 相關連結

[link1](https://docs.microsoft.com/zh-tw/dotnet/core/testing/unit-testing-with-dotnet-test)

[link2](https://xunit.net/#documentation)