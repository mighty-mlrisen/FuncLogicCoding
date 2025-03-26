﻿module FuncProg
open QuadraticEquation
open CircleCylinder
open DigitSum
open System
open Fibonacci
open LogicFunc
open TraversalNumber
open FavoriteLanguage

[<EntryPoint>]
let main args =
    
    let resultSolve = solve 4.0 2.0 -1.0

    match resultSolve with
        | None -> Console.WriteLine("Нет решений")
        | Linear(x) -> Console.WriteLine("Линейное уравнение, корень: x ={0}", x)
        | Quadratic(x1, x2) -> Console.WriteLine("Квадратное уравнение, 2 корня: x1 ={0}, x2={1}", x1, x2)
    
    Console.Write("Введите радиус: ")
    let r = Console.ReadLine() |> float
    Console.Write("Введите высоту: ")
    let h = Console.ReadLine() |> float

    let volume1 = cylinderVolumeCurry r h

    Console.WriteLine("Объем (каррирование): " + volume1.ToString())

    let volume2 = cylinderVolumeSuperPos r h

    Console.WriteLine("Объем: (суперпозиция): " + volume2.ToString() + "\n")

    Console.Write("Введите число: ")
    let n = Console.ReadLine() |> int

    let sum1 = digitSumUp n
    Console.WriteLine("Сумма цифр (рекурсия вверх): " + sum1.ToString())

    let sum2 = digitSumDown n
    Console.WriteLine("Сумма цифр (рекурсия вниз): " + sum2.ToString())

    let fact = logicFunc false 5
    Console.WriteLine("Факториал числа 5: " + fact.ToString())

    let max = traversalNumber 184 (fun a b -> if a > b then a else b) 0
    Console.WriteLine("Max цифра числа 184: " + max.ToString())
    let sum a b = a + b
    Console.Write("Сумма цифр числа 123: ")
    Console.WriteLine(traversalNumber 123 sum 0)

    let countDigit acc n = 
        acc + 1
    
    let maxDigit a b =
     match (a, b) with
     | (a, b) when a >= b -> a
     | _ -> b

    let isEven n = (n % 2 = 0)

    let smallerThan5 n = n < 5

    let travCond = traversalNumberСondition 1456 countDigit 0 isEven

    Console.WriteLine("Кол-ство четных цифр числа 1456: " + travCond.ToString())

    let languagePos = (fun() -> Console.ReadLine()) >> chooseLanguage >> Console.WriteLine
    Console.WriteLine("Твой любимый язык программирования")
    languagePos ()

    let languageCurry input output = 
        output (chooseLanguage (input()))

    Console.WriteLine("Твой любимый язык программирования")
    languageCurry Console.ReadLine Console.WriteLine

    let travCoprime = traversalNumberCoprime 10 sum 0
    Console.WriteLine("Сумма взаимно простых с 10: " + travCoprime.ToString())

    let euler = eulerNumber 10
    Console.WriteLine("Число Эйлера для 10: " + euler.ToString())

    let travCoprimeCond = traversalNumberCoprimeСondition 10 sum 0 smallerThan5

    Console.WriteLine("Сумма взаимно простых с 10 (< 5): " + travCoprimeCond.ToString())
    0
