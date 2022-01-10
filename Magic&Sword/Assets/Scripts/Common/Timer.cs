using System;

public class Timer
{
    public event Action TimeIsUp;

    private readonly float _time;

    private float _passedTime;
   
    public Timer(float time)
    {
        _time = time;
    }
    public void Update(float deltaTime)
    {
        _passedTime += deltaTime;
        if (_passedTime >= _time)
            TimeIsUp?.Invoke();
    }
    public void Reset()
    {
        _passedTime = 0;
    }
}
