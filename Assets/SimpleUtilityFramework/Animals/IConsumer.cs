namespace SimpleUtilityFramework.Environment
{
    public interface IConsumer
    {
        void Feed(int foodAmount);
        void Drink(int thirstToQuench);
    }
}