#!/usr/bin/dotnet run

#:package Humanizer@2.*

using Humanizer;

string singular = "hobby";
string plural = singular.Pluralize();
Console.WriteLine("Plural of '{0}' is '{1}'", singular, plural); 
