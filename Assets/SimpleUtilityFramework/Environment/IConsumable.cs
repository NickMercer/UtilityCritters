namespace SimpleUtilityFramework.Environment
{
    public interface IConsumable
    {
        bool IsAvailable { get; }
        
        void Consume(IConsumer consumer);
    }
}