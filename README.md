# Chain
Plugin based on [Super Invoke](https://assetstore.unity.com/packages/tools/super-invoke-53369). All code was written by me and I don't know how the original implementation is like.

## How to use?

Throw the Chain prefab into the scene and you're ready to go.

**1. To Invoke**:
> **Chain.Invoke(Method, Delay);**
> 
> Ex: 
>
> Chain.Invoke(Method, 2f);

**2. To Invoke Repeating**:
> **Chain.InvokeRepeating(Method, Delay, Rate);**
>
> Ex:
>
> Chain.InvokeRepeating(Method, 2f, 2f);
> 
***OR***
> 
> **Chain.InvokeRepeating(Method, Delay, Rate, RepeatTimes)**;
> 
> Ex:
> 
> Chain.InvokeRepeating(Method, 2f, 2f, 10);

**3. To Invoke With Parameters:**
> **Chain.Invoke(() => Method(Parameter), Delay);**
> 
> Ex:
> 
> Chain.Invoke(() => Method("My Parameter"), 2f);

**4. To Invoke Repeating With Parameters:**
> **Chain.Invoke(() => Method(Parameter), Delay, Rate, RepeatTimes);**
> 
> Ex:
> 
> Chain.Invoke(() => Method("My Parameter"), 2f, 2f, 10);

**5. To use Chains:**
> 
> **var seq = new ChainSequence();**
> 
> **seq.AddDelay(2f);**          //delay 2
> 
> **sel.AddMethod(One);**        //call method
> 
> **seq.AddDelay(2f);**          //delay 2
>
> **sel.AddMethod(Two);**        //call method
>
>   **seq.OnComplete(Completed)** //on complete chain, call method
>
> **seq.Run();**                 //to start the sequence

***OR JUST***

>**seq.AddDelay(2f)**
>    
>    **.AddMethod(One)**
>    
>    **.AddDelay(2f)**
>    
>    **.AddMethod(Two)**
>    
>    **.OnComplete(Completed)**
>    
>    **.Run();**

Similarly, you can run repeatedly:

> **seq.RunRepeating();**

...and of course, stop:

> **seq.Stop();**
