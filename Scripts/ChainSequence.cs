using System;
using System.Collections.Generic;

/// <summary>
/// Chain sequence.
/// </summary>
public class ChainSequence
{
    enum ChainStepType
    {
        METHOD, DELAY,
    }

    class ChainStep
    {
        public ChainStepType type;
        public Action action;

        public ChainStep(ChainStepType type, Action action)
        {
            this.type = type;
            this.action = action;
        }
    }

    int stepIndex = 0;

    bool isRunning;
    bool isRepeating;
    List<ChainStep> steps;
    CircularQueue<float> times;
    Action onComplete;
    Action onStop;

    public ChainSequence()
    {
        steps = new List<ChainStep>();
        times = new CircularQueue<float>();
    }

    /// <summary>
    /// Adds a method call to the sequence.
    /// </summary>
    /// <param name="method">The method to be added.</param>
    public ChainSequence AddMethod(Action method)
    {
        var step = new ChainStep(ChainStepType.METHOD, method);
        steps.Add(step);
        return this;
    }

    /// <summary>
    /// Adds a delay to the sequence.
    /// </summary>
    /// <param name="delay">The delay in seconds.</param>
    public ChainSequence AddDelay(float delay)
    {
        times.Add(delay);
        Action action = StartDelay;
        var step = new ChainStep(ChainStepType.DELAY, action);
        steps.Add(step);
        return this;
    }

    /// <summary>
    /// Adds a callback that will be called at the end of the sequence.
    /// </summary>
    /// <param name="method">The callback method.</param>
    public ChainSequence OnComplete(Action method)
    {
        onComplete = method;
        return this;
    }

    /// <summary>
    /// Adds a callback that will be called if the sequence stops.
    /// </summary>
    /// <param name="method">The callback method.</param>
    public ChainSequence OnStop(Action method)
    {
        onStop = method;
        return this;
    }

    /// <summary>
    /// Run the sequence.
    /// </summary>
    public void Run()
    {
        if (!isRunning)
        {
            isRunning = true;
            stepIndex = 0;
            ExecuteSteps();
        }
    }

    /// <summary>
    /// Run the sequence repeatedly.
    /// </summary>
    public void RunRepeating()
    {
        if (!isRunning)
        {
            isRunning = true;
            isRepeating = true;
            stepIndex = 0;
            ExecuteSteps();
        }
    }

    /// <summary>
    /// Stop the sequence execution.
    /// </summary>
    public void Stop()
    {
        isRunning = false;
        isRepeating = false;
        times.Reset();

        onStop?.Invoke();
    }

    private void ExecuteSteps()
    {
        if (!isRunning)
            return;

        for (; stepIndex < steps.Count; stepIndex++)
        {
            switch (steps[stepIndex].type)
            {
                case ChainStepType.METHOD:
                    steps[stepIndex].action();
                    break;
                case ChainStepType.DELAY:
                    steps[stepIndex].action();
                    stepIndex++;
                    return;
                default: break;
            }

            if ((stepIndex + 1) == steps.Count)
            {
                stepIndex = -1;
                onComplete?.Invoke();

                if (!isRepeating)
                    return;
            }
        }
    }

    private void StartDelay()
    {
        Chain.Invoke(FinishDelay, times.Next());
    }

    private void FinishDelay()
    {
        ExecuteSteps();
    }
}
