using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KYPopulationGrowth
{
    class Program
    {
        //Name of the data file
        static string file = "Population.txt";
        static void Main(string[] args)
        {
            /*
             *Uncomment following to resize Console Window
            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(Console.LargestWindowWidth/2, Console.LargestWindowHeight);
            */

            DrawWelcome();
            Console.Write("Press Any Key to go to MENU");
            Console.ReadKey();
            Console.Clear();

            //Start Menu
            //Menu items
            string fileLocation = "Data File Location ";
            string rawDataDisplay = "Population Numbers By Sex in Kentucky";
            string dataDisplayPercentages = "Population By Sex Percentages";
            string dataDisplayChanges = "Population Increase Over each year";
            string dataInSelectedYeaR = "Population in a Specific Year";
            string addLatestData = "Add  Latest Population Data";
            string saveLatestData = "Save Latest Population Data";
            string deleteSelectedData = "Delete Population Data in a Specific Year";

            int option;
            var fileName = getFileName(file);
            var fileContents = ReadData(fileName);
            while (true)
            {
                Console.WriteLine("\n\tKeyntucky Population By Sex Since 1990 \n");
                Console.WriteLine("\t  1. Display {0}", fileLocation);
                Console.WriteLine("\t  2. Display {0}", rawDataDisplay);
                Console.WriteLine("\t  3. Display {0}", dataDisplayPercentages);
                Console.WriteLine("\t  4. Display {0}", dataDisplayChanges);
                Console.WriteLine("\t  5. Display {0}", dataInSelectedYeaR);
                Console.WriteLine("\t  6. {0}", addLatestData);
                Console.WriteLine("\t  7. {0}", saveLatestData);
                Console.WriteLine("\t  8. {0}", deleteSelectedData);

                Console.WriteLine("\n\t  Q. Exit");

                Console.Write("\n\tSelect Option Number(\"1\" through \"8\" or \"Q\" to Exit) : ");

                //I selected if clause over switch statement
                string optionString = Console.ReadLine();
                if (int.TryParse(optionString, out option))
                {
                    if (option == 1)
                    {
                        //option = 1 => file location
                        Console.WriteLine("\n\t  {0}", fileLocation);
                        Console.WriteLine("\n\t File Located at \n\t   {0}", getFileName(file));
                    }
                    else if (option == 2)
                    {
                        //option = 2 => display raw data read from file or saved
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
                        //option = 5 => delete selected data by year
                        Console.WriteLine("\n\t {0}\n", deleteSelectedData);
                        DeleteData(fileContents);
                    }
                    else
                    {
                        Console.WriteLine("\n \tYou select \"{0}\" is not an option, please select 1 through 8 or \"Q\" to quit", optionString);
                    }
                }
                else
                {
                    if (optionString.ToString().ToUpper() == "Q")
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
                        Console.WriteLine("\n \tYou select \"{0}\" is not an option, please select 1 through 8 or \"Q\" to quit", optionString);
                    }
                }
                Console.Write("\n \tPress Enter to Continue...");
                Console.ReadKey();
                Console.Clear();
            }
            //End Menu
        }

        //Start methods
        //get the filename folders
        public static string getFileName(string fileName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            return Path.Combine(directory.FullName, file);
        }

        //Read data from file
        public static List<PopulationBySex> ReadData(string fileName)
        {
            var population = new List<PopulationBySex>();
            using (var sr = new StreamReader(fileName))
            {
                string headerLine = sr.ReadLine();
                string line;
                int year;
                int males;
                int females;
                while ((line = sr.ReadLine()) != null)
                {
                    var populationBySex = new PopulationBySex();
                    string[] values = line.Split(',');
                    if (int.TryParse(values[0], out year))
                    {
                        populationBySex.Year = year;
                    }
                    if (int.TryParse(values[1], out males))
                    {
                        populationBySex.Males = males;
                    }
                    if (int.TryParse(values[2], out females))
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
                malePreviousValue = item.Males;
                femalePreviousValue = item.Females;
                totalPreviousValue = item.getTotal();

                Console.Write("\t" + item.Year + "\t" +
                    item.Males + "\t " + string.Format("{0:0.00%}", changeMales) + "\t\t" +
                    item.Females + "\t " + string.Format("{0:0.00%}", changeFemales) + "\t\t" +
                    item.getTotal() + "\t " + string.Format("{0:0.00%}", changeTotal) + "\n");

            }
        }

        //Add latest population data to missing years
        public static void AddLatestData(List<PopulationBySex> fileContents)
        {
            var population = new PopulationBySex();           
            int maxYear = GetMaxYear(fileContents);

            int outParameter;
            int year = maxYear + 1; 
            //Console.WriteLine("\n\tEnter Latest Data to the File");
            Console.WriteLine("\t Year : {0}", year);
            //year = Console.ReadLine();
            //if (int.TryParse(year, out outParameter))
            //{
                population.Year = year;
            //}
            while (true)
            {
                //string males;
                Console.Write("\t Males : ");
                string males = Console.ReadLine();
                if (int.TryParse(males, out outParameter))
                {
                    population.Males = outParameter;
                }
                else
                {
                    Console.WriteLine("\tInvalid Entry, please enter valid population value");
                }

                //string females;
                Console.Write("\t Females : ");
                string females = Console.ReadLine();
                if (int.TryParse(females, out outParameter))
                {
                    population.Females = outParameter;
                }
                else
                {
                    Console.WriteLine("\tInvalid Entry, please enter valid population value");
                }
                if (population.Males > 0 && population.Females > 0)
                {
                    break;
                }
            }

                fileContents.Add(population);

                Console.WriteLine("\n\tFollowing Data added to the Population data \n\t Year : {0} \n\t Males : {1} \n\t Females : {2}", year, population.Males, population.Females);
            
        }

        //Search for population data for a specific year
        public static void FindData(List<PopulationBySex> fileContents)
        {
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
                            Year = x.Year,
                            Males = x.Males,
                            Females = x.Females,
                            Total = x.getTotal(),
                            MalePercetages = x.GetMalePercent(),
                            FemalePercentage = x.GetFemalePercent()
                        })
                        .FirstOrDefault();

            Console.WriteLine("\n\t Population By Sex in {0} \n", data.Year);
            Console.WriteLine("\t\t Population   %");
            Console.WriteLine("\t Male  \t: {0} {1}", string.Format("{0:#,##0}", data.Males), string.Format("{0:0.00%}", data.MalePercetages));
            Console.WriteLine("\t Female\t: {0} {1}", string.Format("{0:#,##0}", data.Females), string.Format("{0:0.00%}", data.FemalePercentage));
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

        public static int GetMinYear(List<PopulationBySex> fileContents)
        {
            var year = (from x in fileContents
                           orderby x.Year
                           select new { MinYear = x.Year })
                          .Take(1)
                          .FirstOrDefault();
            return year.MinYear;
        }

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