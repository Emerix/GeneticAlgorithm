namespace Cars
{
    public interface ICarsData
    {
        float CurrentTime { get; }
        int CurrentIteration { get; }
        int CurrentSlot { get; }
    }
}