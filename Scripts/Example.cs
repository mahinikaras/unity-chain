using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        SequenceExample();
        //InvokeExample();
        //InvokeRepeatingExample();
        //InvokeWithParametersExample();
    }

    private void SequenceExample()
    {
        var seq = new ChainSequence();
        seq
            .AddDelay(2f)
            .AddMethod(One)
            .AddDelay(2f)
            .AddMethod(Two)
            .OnComplete(Completed)
            .RunRepeating();
    }

    private void InvokeExample()
    {
        Chain.Invoke(One, 2f);
    }

    private void InvokeRepeatingExample()
    {
        Chain.InvokeRepeating(One, 2f, 2f);
    }

    private void InvokeWithParametersExample()
    {
        Chain.Invoke(() => FunctionWithParameters("My Parameter"), 2f);
    }

    private void One()
    {
        Debug.Log("One");
    }

    private void Two()
    {
        Debug.Log("Two");
    }

    private void Completed()
    {
        Debug.Log("Repeat");
    }

    private void FunctionWithParameters(string arg)
    {
        Debug.Log(arg);
    }
}

