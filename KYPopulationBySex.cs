namespace KYPopulationGrowth
{
    public class KYPopulationBySex : USPopulation
    {
        //calculate total population for each year
        public override int getTotal()
        {
            return Males + Females;
        }

        //calculate population Percentage for each year for males
        public override double GetMalePercent()
        {
            return (((double)Males / getTotal()));
        }

        //calculate population Percentage for each year for females
        public override double GetFemalePercent()
        {
            return (((double)Females / getTotal()));
        }

        //Display data
        public override string displayRawData()
        {
            return "\t" + Year + "\t" +
                string.Format("{0:#,##0}", Males) + "\t" +
                string.Format("{0:##,##0}", Females) + "\t" +
                string.Format("{0:##,##0}", getTotal()) + "\n";
        }

        //display calculated data
        public override string displayPercentages()
        {
            return "\t" + Year.ToString() + "\t" +
                string.Format("{0:#,##0}", Males) + "\t" +
                string.Format("{0:0.00%}", GetMalePercent()) + "\t" +
                string.Format("{0:#,##0}", Females) + "\t" +
                string.Format("{0:0.00%}", GetFemalePercent()) + "\n";
        }

    }
}

