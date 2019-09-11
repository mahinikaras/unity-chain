using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Chain delay utility.
/// </summary>
public class Chain : MonoBehaviour
{
    private static Chain _instance;

    public Chain()
    {
        _instance = this;
    }

    private void Awake()
    {
        if (GameObject.FindObjectsOfType<Chain>().Length > 1)
        {
            Destroy(this);
            return;
        }
        
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// Create a Chain sequence.
    /// </summary>
    public ChainSequence CreateSequence()
    {
        return new ChainSequence();
    }

    /// <summary>
    /// Invoke a method after "delay" seconds.
    /// </summary>
    /// <param name="method">Method to run after delay.</param>
    /// <param name="delay">Delay time in seconds.</param>
    public static void Invoke(Action method, float delay)
    {
        _instance.StartCoroutine(_instance.UnityCoroutine(method, delay));
    }

    /// <summary>
    /// Invoke a method after "delay" seconds.
    /// </summary>
    /// <param name="method">Method to run after delay.</param>
    /// <param name="delay">Delay time in seconds.</param>
    /// <param name="repeatRate">Delay time in seconds until repeat.</param>
    public static void InvokeRepeating(Action method, float delay, float repeatRate)
    {
        _instance.StartCoroutine(_instance.UnityCoroutineRepeat(method, delay, repeatRate));
    }

    /// <summary>
    /// Invoke a method after "delay" seconds.
    /// </summary>
    /// <param name="method">Method to run after delay.</param>
    /// <param name="delay">Delay time in seconds.</param>
    /// <param name="repeatRate">Delay time in seconds until repeat.</param>
    /// <param name="repeatTimes">How many times (-1 for forever).</param>
    public static void InvokeRepeating(Action method, float delay, float repeatRate, float repeatTimes)
    {
        _instance.StartCoroutine(_instance.UnityCoroutineRepeat(method, delay, repeatRate, repeatTimes));
    }

    /// <summary>
    /// Advanced invoke method to call with parameters.
    /// </summary>
    /// <param name="delay">Delay time in seconds.</param>
    /// <param name="method">Method to run after delay.</param>
    /// <param name="args">Method arguments.</param>
    public static void AdvancedInvoke(float delay, Action<ChainArgs> method, params object[] args)
    {
        ChainArgs chainArgs = new ChainArgs(args);
        _instance.StartCoroutine(_instance.UnityCoroutine(delay, method, chainArgs));
    }

    /// <summary>
    /// Advanced invoke method to call with parameters.
    /// </summary>
    /// <param name="delay">Delay time in seconds.</param>
    /// <param name="repeatRate">Delay time in seconds until repeat.</param>
    /// <param name="method">Method to run after delay.</param>
    /// <param name="args">Method arguments.</param>
    public static void AdvancedInvokeRepeating
        (float delay, float repeatRate, Action<ChainArgs> method, params object[] args)
    {
        ChainArgs chainArgs = new ChainArgs(args);
        _instance.StartCoroutine(_instance.UnityCoroutine(delay, method, chainArgs));
    }

    /// <summary>
    /// Advanced invoke method to call with parameters.
    /// </summary>
    /// <param name="delay">Delay time in seconds.</param>
    /// <param name="repeatRate">Delay time in seconds until repeat.</param>
    /// <param name="repeatTimes">How many times (-1 for forever).</param>
    /// <param name="method">Method to run after delay.</param>
    /// <param name="args">Method arguments.</param>
    public static void AdvancedInvokeRepeating
        (float delay, float repeatRate, float repeatTimes, Action<ChainArgs> method, params object[] args)
    {
        ChainArgs chainArgs = new ChainArgs(args);
        _instance.StartCoroutine(_instance.UnityCoroutine(delay, method, chainArgs));
    }

    private IEnumerator UnityCoroutine(Action method, float delay)
    {
        yield return new WaitForSeconds(delay);
        method();
    }

    private IEnumerator UnityCoroutine(float delay, Action<ChainArgs> method, ChainArgs args)
    {
        yield return new WaitForSeconds(delay);
        method(args);
    }

    private IEnumerator UnityCoroutineRepeat(Action method, float delay, float repeatRate, float repeatTimes = -1)
    {
        yield return new WaitForSeconds(delay);

        if (repeatTimes < 0)
        {
            while (true)
            {
                method();
                yield return new WaitForSeconds(repeatRate);
            }
        }
        else
        {
            for (int i = 0; i < repeatTimes; i++)
            {
                method();
                yield return new WaitForSeconds(repeatRate);
            }
        }
    }

    private IEnumerator UnityCoroutineRepeat(Action<ChainArgs> method, ChainArgs args, float delay, float repeatRate, float repeatTimes = -1)
    {
        yield return new WaitForSeconds(delay);

        if (repeatTimes < 0)
        {
            while (true)
            {
                method(args);
                yield return new WaitForSeconds(repeatRate);
            }
        }
        else
        {
            for (int i = 0; i < repeatTimes; i++)
            {
                method(args);
                yield return new WaitForSeconds(repeatRate);
            }
        }
    }
}

