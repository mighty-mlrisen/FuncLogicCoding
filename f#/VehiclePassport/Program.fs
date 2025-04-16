module Document

open System
open VehiclePassport

(*
let passport = 
    VehiclePassport(
        series = "1234",    
        number = "12345",    
        lastName = "Иванов",  
        firstName = "Олег",   
        middleName = "Иванович", 
        vehicleModel = "Toyota Camry", 
        issueDate = DateTime(2025, 5, 15) 
    )
*)
//Console.WriteLine(passport.ToString())

let passport2 = 
    VehiclePassport(
        series = "AA", 
        number = "123456", 
        lastName = "Иванов", 
        firstName = "Олег", 
        middleName = "Иванович", 
        vehicleModel = "Toyota Camry", 
        issueDate = DateTime(2020, 5, 15)
    )


Console.WriteLine(passport2.ToString())