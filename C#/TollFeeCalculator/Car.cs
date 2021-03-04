namespace TollFeeCalculator
{
    public class Car : Vehicle
    {
        public override bool IsTollFree()
        {
            return false;
        }
    }
}