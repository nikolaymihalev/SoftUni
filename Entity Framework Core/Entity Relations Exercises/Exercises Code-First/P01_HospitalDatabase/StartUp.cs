﻿using P01_HospitalDatabase.Data;

namespace P01_HospitalDatabase
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            HospitalContext context = new HospitalContext();
            Console.WriteLine("Hello, World!");
        }
    }
}