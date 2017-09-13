using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using BeerSting.Api.Function;
using BeerSting.Api.Database;
using System.Data;
using BeerSting.Api.Test;

namespace DeveloperApi
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Test().Tester();
            Console.WriteLine("Exit Program");
            Console.Read();
        }
    }
}
