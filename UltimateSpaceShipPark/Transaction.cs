namespace UltimateSpaceShipPark
{
    public class Transaction
    {
        public double PriceRate(DateTime Entre, DateTime Exist)
        {
            double totalCost;
            TimeSpan time = Exist - Entre;
            // we only need hours for the certain day, should the hour exceed 4 hours, we get a day instead, as 4 days equal 60kr
            double hours = (char)time.Hours;
            // if the user stays for months or days we get total of months/days in int days. 
            double Days = (char)time.TotalDays;
            // Our minutes only takes 59 at largest and 0 as a minimum. 
            double Minutes = (char)time.Minutes;
            Minutes = (hours * 60) + Minutes;
            // checkes if the user has spent more then 3 hours and 20 minutes = 50kr. Which means a day. 
            if (Minutes >= 200)
            {
                Days = Days + 1;
                hours = 0;
                Minutes = 0;
            }
            totalCost = (Days * 50) + (Minutes * 0.25);

            return totalCost;

        }
    }

}
