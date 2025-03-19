module FuncProg
open QuadraticEquation
open System

[<EntryPoint>]
let main args =
    
    let result = solve 4.0 2.0 -1.0

    match result with
        | None -> Console.WriteLine("Нет решений")
        | Linear(x) -> Console.WriteLine("Линейное уравнение, корень: x ={0}", x)
        | Quadratic(x1, x2) -> Console.WriteLine("Квадратное уравнение, 2 корня: x1 ={0}, x2={1}", x1, x2)
    

    0
