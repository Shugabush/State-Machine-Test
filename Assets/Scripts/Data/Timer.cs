using UnityEngine;

[System.Serializable]
public struct Timer
{
    [SerializeField] float timeLimit;
    float timeElapsed;

    public Timer(float timeLimit)
    {
        this.timeLimit = timeLimit;
        timeElapsed = 0f;
    }

    public float TimeLimit => timeLimit;
    public float TimeElapsed => timeElapsed;
    public float TimeRemaining => TimeLimit - (TimeElapsed % TimeLimit);

    public float FractionOfTimeElapsed => TimeElapsed % TimeLimit / TimeLimit;
    public float FractionOfTimeRemaining => TimeRemaining / TimeLimit;
    public int Laps => (int)(TimeElapsed / TimeLimit);

    public void Update(float time)
    {
        timeElapsed += time;
    }

    public void Reset()
    {
        this = new Timer(timeLimit);
    }
}
