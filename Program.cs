using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KYPopulationGrowth
{
    class Program
    {
        //Name of the data file
        const string file = "Population.txt";

        static void Main(string[] args)
        {
            //resize the console window
            ConsoleWindow.SetSize();

            //Welcome message
            DrawWelcome();
            Console.Write("Press Any Key to go to MENU");
            Console.ReadKey();
            Console.Clear();

            
            //Menu items
            string fileLocation = "Data File Location ";
            string rawDataDisplay = "Population Numbers By Sex in Kentucky";
            string dataDisplayPercentages = "Population By Sex Percentages";
            string dataDisplayChanges = "Population Data Increase Over each year";
            string dataInSelectedYeaR = "Population Data in a Specific Year";
            string addLatestData = "Add  Latest Population Data";
            string saveLatestData = "Save Latest Population Data";
            string deleteSelectedData = "Delete Population Data in a Specific Year";

            //Get the data file path and file name
            var fileName = getFileName(file);
            //Read data from file
            var fileContents = ReadData(fileName);

            //Start Menu, IF option Q selected, it breaks the loop
            while (true)
            {
                Console.WriteLine("\n\tKentucky Population By Sex Since 1990 \n");
                Console.WriteLine("\t  1. Display {0}", fileLocation);
                Console.WriteLine("\t  2. Display {0}", rawDataDisplay);
                Console.WriteLine("\t  3. Display {0}", dataDisplayPercentages);
                Console.WriteLine("\t  4. Display {0}", dataDisplayChanges);
                Console.WriteLine("\t  5. Display {0}", dataInSelectedYeaR);
                Console.WriteLine("\t  6. {0}", addLatestData);
                Console.WriteLine("\t  7. {0}", saveLatestData);
                Console.WriteLine("\t  8. {0}", deleteSelectedData);
                Console.WriteLine("\t  9. Update {0}", dataInSelectedYeaR);

                Console.WriteLine("\n\t  Q. Exit");

                Console.Write("\n\tSelect Option Number(\"1\" through \"9\" or \"Q\" to Exit) : ");

                //I selected if clause over switch statement
                string getOption = Console.ReadLine();
                if (int.TryParse(getOption, out int option))
                {
                    if (option == 1)
                    {
                        //option = 1 => file location
                        Console.WriteLine("\n\t  {0}", fileLocation);
                        Console.WriteLine("\n\t File Located at \n\t   {0}", getFileName(file));
                    }
                    else if (option == 2)
                    {
                        //option = 2 => display data read from file
                        Console.WriteLine("\n\t {0}\n", rawDataDisplay);
                        DisplayData(fileContents);
                    }

                    else if (option == 3)
                    {
                        //option = 3 => display data with percentage calculated
                        Console.WriteLine("\n\t {0}\n", dataDisplayPercentages);
                        DisplayPercentages(fileContents);
                    }
                    else if (option == 4)
                    {
                        //option = 4 => display raw data percentage changed over year
                        Console.WriteLine("\n\t {0}\n", dataDisplayChanges);
                        DisplayIncrease(fileContents);
                    }
                    else if (option == 5)
                    {
                        //option = 5 => display data in a user selected year
                        Console.WriteLine("\n\t {0}\n", dataInSelectedYeaR);
                        FindData(fileContents);
                    }
                    else if (option == 6)
                    {
                        //option = 6 => Add latest population data 
                        Console.WriteLine("\n\t {0}\n", addLatestData);
                        AddLatestData(fileContents);
                    }
                    else if (option == 7)
                    {
                        //option = 7 => Save data to a file
                        Console.WriteLine("\n\t {0}\n", saveLatestData);
                        WriteToFile(fileContents);
                    }
                    else if (option == 8)
                    {
                        //option = 8 => delete selected data by year
                        Console.WriteLine("\n\t {0}\n", deleteSelectedData);
                        DeleteData(fileContents);
                    }
                    else if(option == 9)
                    {
                        Console.WriteLine("\n\tUpdate {0}\n", dataInSelectedYeaR);
                        UpdateData(fileContents);
                    }
                    else
                    {
                        Console.WriteLine("\n \tYou select \"{0}\" is not an option, please select \"1\" through \"9\" or \"Q\" to quit", getOption);
                    }
                }
                else
                {
                    if (getOption.ToString().ToUpper() == "Q")
                    {
                        //Quit from Console App
                        //Console.WriteLine("\n \tThank You and Have a nice day!");
                        DrawExit();
                        Console.ReadKey();
                        Console.ResetColor();
                        break;
                    }
                    else
                    {
                        //user entered bad option or try to hack the app :)
                        Console.WriteLine("\n \tYou select \"{0}\" is not an option, please select \"1\" through \"9\" or \"Q\" to quit", getOption);
                    }
                }
                Console.Write("\n \tPress Enter to Continue...");
                Console.ReadKey();
                Console.Clear();
            }
            //End Menu
        }

        //methods
        //get the filename folders
        public static string getFileName(string fileName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            //set path and file name and return it
            return Path.Combine(directory.FullName, file);
        }

        //Read data from file
        public static List<PopulationBySex> ReadData(string fileName)
        {
            //create a new object using PopulationBySex class
            var population = new List<PopulationBySex>();
            //reading data from file
            using (var sr = new StreamReader(fileName))
            {
                //read header line to skip the header line
                sr.ReadLine();
                string line;
                //read data
                while ((line = sr.ReadLine()) != null)
                {
                    //Instantiate class populationBySex
                    var populationBySex = new PopulationBySex();
                    string[] values = line.Split(',');
                    if (int.TryParse(values[0], out int year))
                    {
                        populationBySex.Year = year;
                    }
                    if (int.TryParse(values[1], out int males))
                    {
                        populationBySex.Males = males;
                    }
                    if (int.TryParse(values[2], out int females))
                    {
                        populationBySex.Females = females;
                    }
                    population.Add(populationBySex);
                }
                return population;
            }
        }

        //get population 
        public static List<PopulationBySex> GetPopulation(List<PopulationBySex> populations)
        {
            var population = new List<PopulationBySex>();
            foreach (var item in populations)
            {
                population.Add(item);
            }
            return population;
        }

        //Display Raw data
        public static void DisplayData(List<PopulationBySex> populations)
        {
            var data = GetPopulation(populations);
            Console.WriteLine("\tYear \t Male \t\t Female \t Total");
            foreach (var item in data)
            {
                Console.Write(item.displayRawData());
            }
        }

        //Display Percentages
        public static void DisplayPercentages(List<PopulationBySex> fileContents)
        {
            var data = GetPopulation(fileContents);
            Console.WriteLine("\tYear\tMale Population  %\tFemale Population   % ");
            foreach (var item in data)
            {
                Console.Write(item.displayPercentages());
            }
        }

        //Display population changes over the previous year
        public static void DisplayIncrease(List<PopulationBySex> fileContents)
        {
            var data = GetPopulation(fileContents);
            int malePreviousValue = 0;
            int femalePreviousValue = 0;
            int totalPreviousValue = 0;
            double changeMales = 0.00;
            double changeFemales = 0;
            double changeTotal = 0;

            Console.WriteLine("\tYear \t Male \t Increase \t Female\t Increase\t Total\t Increase");
            foreach (var item in data)
            {
                if (malePreviousValue + femalePreviousValue + totalPreviousValue > 0)
                {
                    changeMales = ((double)item.Males - malePreviousValue) / item.Males;
                    changeFemales = ((double)item.Females - femalePreviousValue) / item.Females;
                    changeTotal = ((double)item.getTotal() - totalPreviousValue) / item.getTotal();
                }

                if (malePreviousValue + femalePreviousValue + totalPreviousValue == 0)
                {
                    Console.Write("\t" + item.Year + "\t" +
                    item.Males + "\t " + "-" + "\t\t" +
                    item.Females + "\t " + "-" + "\t\t" +
                    item.getTotal() + "\t " + "-" + "\n");
                }
                else
                {
                    Console.Write("\t" + item.Year + "\t" +
                    item.Males + "\t " + string.Format("{0:0.00%}", changeMales) + "\t\t" +
                    item.Females + "\t " + string.Format("{0:0.00%}", changeFemales) + "\t\t" +
                    item.getTotal() + "\t " + string.Format("{0:0.00%}", changeTotal) + "\n");
                }
                malePreviousValue = item.Males;
                femalePreviousValue = item.Females;
                totalPreviousValue = item.getTotal();
            }
        }

        //Add latest population data to missing years
        public static void AddLatestData(List<PopulationBySex> fileContents)
        {
            var population = new PopulationBySex();           
            int maxYear = GetMaxYear(fileContents);

            int year = maxYear + 1; 
            Console.WriteLine("\n\tEnter Latest Data for");
            Console.WriteLine("\t     Year : {0}", year);
            population.Year = year;

            while (true)
            {
                Console.Write("\t Males : ");
                string males = Console.ReadLine();

                if (int.TryParse(males, out int outMales))
                {
                    population.Males = outMales;
                }

                Console.Write("\t Females : ");
                string females = Console.ReadLine();
                if (int.TryParse(females, out int outfemales))
                {
                    population.Females = outfemales;
                }

                if (population.Males > 0 && population.Females > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\tInvalid Entry, please enter valid population value");
                }
            }

                fileContents.Add(population);

                Console.WriteLine("\n\tFollowing Data added to the Population data \n\t Year : {0} \n\t Males : {1} \n\t Females : {2}", year, population.Males, population.Females);
            
        }

        /*-----------------------------------------------------------------------*/
        public static void UpdateData(List<PopulationBySex> fileContents)
        {
            var population = new PopulationBySex();
            int minYear = GetMinYear(fileContents);
            int maxYear = GetMaxYear(fileContents);
            int outYear = 0;
            int outMales = 0;
            int outFemales = 0;
            string yearString;


            //population.Year = year;
            while (true)
            {
                Console.Write("\n\tEnter the Year Between \"{0}\" \"{1}\"to update : ", minYear, maxYear);
                yearString = Console.ReadLine();

                if (int.TryParse(yearString, out outYear))
                {
                    if (outYear >= minYear && outYear <= maxYear)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\tselect a year between \"{0}\" & \"{1}\"", minYear, maxYear);
                    }

                }
            }
            while (true)
            {
                var data = (from y in fileContents 
                    where y.Year == outYear 
                    select new { maleValue = y.Males, femaleValue = y.Females })
                    .FirstOrDefault() ;

                Console.WriteLine("\t Existing Male Value is {0}", data.maleValue);
                Console.Write("\t Edit Males : ");
                string males = Console.ReadLine();
                int.TryParse(males, out outMales);

                Console.WriteLine("\t Existing Male Value is {0}", data.femaleValue);
                Console.Write("\t Edit Females : ");
                string females = Console.ReadLine();
                int.TryParse(females, out outFemales);

                if (outYear > 0 && outMales > 0 && outFemales > 0)
                {
                    foreach(var fc in fileContents)
                    {
                        if(fc.Year == outYear)
                        {
                            fc.Males = outMales;
                            fc.Females = outFemales;
                        }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("\tInvalid Entry, please enter valid population value");
                }
            }

            //fileContents.Add(population);

            Console.WriteLine("\n\tPopulation data updated with following values \n\t Year : {0} \n\t Males : {1} \n\t Females : {2}", outYear, outMales, outFemales);

        }
        /*-----------------------------------------------------------------------*/

        //Search for population data for a specific year
        public static void FindData(List<PopulationBySex> fileContents)
        {
            //Get Min and max year to limit search to existing data range only
            int minYear = GetMinYear(fileContents);
            int maxYear = GetMaxYear(fileContents);
            int year;
            while (true)
            {
                Console.Write("\n\tEnter Year Between {0}-{1} : ", minYear, maxYear);
                string stringYear = Console.ReadLine();
                if (int.TryParse(stringYear, out year))
                {
                    if (year >= minYear && year <= maxYear)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\tOut of Range, please select year between {0}-{1}", minYear, maxYear);
                    }
                }
                else
                {
                    Console.WriteLine("\tInvalid Option, please select year between {0}-{1}", minYear, maxYear);
                }
            }

            var data = (from x in fileContents
                        where x.Year == year
                        select new
                        {
                            year = x.Year,
                            males = x.Males,
                            females = x.Females,
                            Total = x.getTotal(),
                            MalePercetages = x.GetMalePercent(),
                            FemalePercentage = x.GetFemalePercent()
                        })
                        .FirstOrDefault();

            Console.WriteLine("\n\t Population By Sex in {0} \n", data.year);
            Console.WriteLine("\t\t Population   %");
            Console.WriteLine("\t Male  \t: {0} {1}", string.Format("{0:#,##0}", data.males), string.Format("{0:0.00%}", data.MalePercetages));
            Console.WriteLine("\t Female\t: {0} {1}", string.Format("{0:#,##0}", data.females), string.Format("{0:0.00%}", data.FemalePercentage));
            Console.WriteLine("\t Total \t: {0} {1}", string.Format("{0:#,##0}", data.Total), string.Format("{0:0.00%}", data.MalePercetages + data.FemalePercentage));

            /*
            foreach ( var item in fileContents)
            {
                if (item.Year == year)
                {
                    Console.WriteLine("Population By Sex in {0} \n", item.Year);
                    Console.WriteLine(" Male Population : " +  item.Males + "\n Female Population : " + item.Females);
                    return;
                }
            }
            */
        }

        //Write population data to the file
        public static void WriteToFile(List<PopulationBySex> fileContents)
        {
            using (var writer = new StreamWriter("Population.txt"))
            {
                writer.WriteLine("Year,Males,Females,Total");
                foreach (var item in fileContents)
                {
                    writer.WriteLine(item.Year + "," + item.Males + "," + item.Females + "," + item.getTotal());
                }
            }
        }

        //public static void UpdateData(List<PopulationBySex> fileContents)
        //{
        //    using (var writer = new StreamWriter("Population.txt"))
        //    {
        //        writer.WriteLine("Year,Males,Females,Total");
        //        foreach (var item in fileContents)
        //        {
        //            writer.WriteLine(item.Year + "," + item.Males + "," + item.Females + "," + item.getTotal());
        //        }
        //    }
        //}

        //Delete selected data by year
        public static void DeleteData(List<PopulationBySex> fileContents)
        {
            int minYear = GetMinYear(fileContents);
            int maxYear = GetMaxYear(fileContents);
            while (true)
            {
                Console.Write("\n\tSelect a year to delete from data between {0}-{1} : ", minYear, maxYear);
                string stringYear = Console.ReadLine();
                int year;
                if (Int32.TryParse(stringYear, out year))
                {
                    if (year >= minYear && year <= maxYear)
                    {
                        var data = (from x in fileContents where x.Year == year select new { MalePopulation = x.Males, FemalePopulation = x.Females }).FirstOrDefault();
                        
                        Console.WriteLine("\n\tYear: {0} \t Males : {1} \t Females : {2}", year, data.MalePopulation, data.FemalePopulation);
                        Console.Write("\tDo you want to delete these data Y/N ?");
                        string YesNo = Console.ReadLine();
                        if (YesNo.ToUpper() == "Y")
                        {
                            fileContents.RemoveAll(y => y.Year == year);
                            Console.WriteLine("\t{0} Data deleted from file", year);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\tData not deleted");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\tOut of Range, please enter year between {0}-{1}", minYear, maxYear);
                    }
                }
                else
                {
                    Console.WriteLine("\tInvalid Option, please enter year between {0}-{1}", minYear, maxYear);
                }

            }

        }

        //Get the minimum year from data
        public static int GetMinYear(List<PopulationBySex> fileContents)
        {
            var year = (from x in fileContents
                           orderby x.Year
                           select new { MinYear = x.Year })
                          .Take(1)
                          .FirstOrDefault();
            return year.MinYear;
        }

        //Get the maximum year from data
        public static int GetMaxYear(List<PopulationBySex> fileContents)
        {
            var year = (from x in fileContents
                           orderby x.Year descending
                           select new { MaxYear = x.Year })
                              .Take(1)
                              .FirstOrDefault();
            return year.MaxYear;
        }

        public static void DrawWelcome()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            //Console.Write("Press any key to continue");
            //Console.ReadKey();

            Console.WriteLine("");
            Console.WriteLine("    &&       && &&&&&& &&       &&&&&   &&&&&   &&&&   &&&& &&&&&&   ");
            Console.WriteLine("    &&       && &&     &&      &&      &&   &&  && && && && &&       ");
            Console.WriteLine("    &&  &&&  && &&&&&& &&     &&      &&     && &&  &&&  && &&&&&&   ");
            Console.WriteLine("    && && && && &&     &&      &&      &&   &&  &&       && &&       ");
            Console.WriteLine("    &&&&   &&&& &&&&&& &&&&&&   &&&&&   &&&&&   &&       && &&&&&&   ");
            Console.WriteLine("  ");
            Console.WriteLine("                                          &&                          ");
            Console.WriteLine("                                          &&                          ");
            Console.WriteLine("             $$   &&&&&   &&&&&     &&&&& &&  &&&&&&  $$              ");
            Console.WriteLine("           $$    &&      &&   &&   &&   &&&& &&    &&   $$            ");
            Console.WriteLine("         <$     &&      &&     && &&     &&& &&&&&&&&     $>          ");
            Console.WriteLine("           $$    &&      &&   &&   &&   &&&& &&         $$            ");
            Console.WriteLine("             $$   &&&&&   &&&&&     &&&&& &&  &&&&&&  $$              ");
            Console.WriteLine("  ");
            Console.WriteLine("    &&       &&&&&   &&      && &&  &&&&&  &       &  && &&     &&&&&& ");
            Console.WriteLine("    &&      &&   &&  &&      && && &&      &&     &&  && &&     &&     ");
            Console.WriteLine("    &&     &&     && &&      && &&  &&&&    &&   &&   && &&     &&&&&& ");
            Console.WriteLine("    &&      &&   &&   &&    &&  &&     &&    && &&    && &&     &&     ");
            Console.WriteLine("    &&&&&&   &&&&&     &&&&&&   && &&&&&      &&&     && &&&&&& &&&&&& ");

            Console.WriteLine("          ");
        }
        public static void DrawExit()
        {
            Console.WriteLine("");
            Console.WriteLine("   $$$$$$$$ $$    $$   $$$   $$$   $$ $$  $$   $$    $$   &&&&&   &&      &&     ");
            Console.WriteLine("      $$    $$    $$  $$ $$  $$$$  $$ $$ $$     $$  $$   &&   &&  &&      &&     ");
            Console.WriteLine("      $$    $$$$$$$$ $$$$$$$ $$ $$ $$ $$$$       $$$$   &&     && &&      &&     ");
            Console.WriteLine("      $$    $$    $$ $$   $$ $$  $$$$ $$ $$       $$     &&   &&   &&    &&     ");
            Console.WriteLine("      $$    $$    $$ $$   $$ $$   $$$ $$  $$      $$      &&&&&     &&&&&&      ");
            Console.WriteLine("    ");
            Console.WriteLine("                                            &&                          ");
            Console.WriteLine("                                            &&                          ");
            Console.WriteLine("               $$   &&&&&   &&&&&     &&&&& &&  &&&&&&  $$              ");
            Console.WriteLine("             $$    &&      &&   &&   &&   &&&& &&    &&   $$            ");
            Console.WriteLine("           <$     &&      &&     && &&     &&& &&&&&&&&     $>          ");
            Console.WriteLine("             $$    &&      &&   &&   &&   &&&& &&         $$            ");
            Console.WriteLine("               $$   &&&&&   &&&&&     &&&&& &&  &&&&&&  $$              ");
            Console.WriteLine("    ");
            Console.WriteLine("      &&       &&&&&   &&      && &&  &&&&& &&       && && &&     &&&&&& ");
            Console.WriteLine("      &&      &&   &&  &&      && && &&      &&     &&  && &&     &&     ");
            Console.WriteLine("      &&     &&     && &&      && &&  &&&&    &&   &&   && &&     &&&&&& ");
            Console.WriteLine("      &&      &&   &&   &&    &&  &&     &&    && &&    && &&     &&     ");
            Console.WriteLine("      &&&&&&   &&&&&     &&&&&&   && &&&&&      &&&     && &&&&&& &&&&&& ");

            Console.WriteLine("          ");
            Console.ResetColor();
        }
    }
}