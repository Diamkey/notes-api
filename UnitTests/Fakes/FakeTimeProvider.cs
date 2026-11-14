namespace UnitTests.Fakes;

internal sealed class FakeTimeProvider : TimeProvider
{
    private readonly DateTimeOffset _fixedTime;
    
    public FakeTimeProvider(DateTimeOffset fixedTime)
    {
        _fixedTime = fixedTime;
    }

    public override DateTimeOffset GetUtcNow()
    {
        return _fixedTime;
    }
}