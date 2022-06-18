public class ActorRotateRequested
{
    public bool IsRight { get; }

    public ActorRotateRequested(bool isRight)
    {
        IsRight = isRight;
    }
}