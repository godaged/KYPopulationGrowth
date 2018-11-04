

namespace KYPopulationGrowth
{
    public abstract class USPopulation
    {
        public int Year { get; set; }
        public int Males { get; set; }
        public int Females { get; set; }

        //calculate total population for each year
        public abstract int getTotal();

        //calculate population Percentage for each year for males
        public abstract double GetMalePercent();
        //calculate population Percentage for each year for females
        public abstract double GetFemalePercent();

        //Display data
        public abstract string displayRawData();

        //display calculated data
        public abstract string displayPercentages();

    }
}
