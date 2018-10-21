namespace KYPopulationGrowth
{
    public class PopulationBySex
    {
        public int Year { get; set; }
        public int Males { get; set; }
        public int Females { get; set; }

        //calculate total population for each year
        public int getTotal()
        {
            return Males + Females;
        }

        //calculate population Percentage for each year for males
        public double GetMalePercent()
        {
            return (((double)Males / getTotal()));
        }

        //calculate population Percentage for each year for females
        public double GetFemalePercent()
        {
            return (((double)Females / getTotal()));
        }

        //Display data
        public string displayRawData()
        {
            return "\t" + Year + "\t" +
                string.Format("{0:#,##0}", Males) + "\t" +
                string.Format("{0:##,##0}", Females) + "\t" +
                string.Format("{0:##,##0}", getTotal()) + "\n";
        }

        //display calculated data
        public string displayPercentages()
        {
            return "\t" + Year.ToString() + "\t" +
                string.Format("{0:#,##0}", Males) + "\t" +
                string.Format("{0:0.00%}", GetMalePercent()) + "\t" +
                string.Format("{0:#,##0}", Females) + "\t" +
                string.Format("{0:0.00%}", GetFemalePercent()) + "\n";
        }

    }
}

