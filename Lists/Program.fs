module Lab2
open System
open Lists

[<EntryPoint>]
let main args =
    let sum a b = a + b
    let countDigit acc n = 
        acc + 1
    
    let maxDigit a b =
     match (a, b) with
     | (a, b) when a >= b -> a
     | _ -> b

    let isEven n = (n % 2 = 0)

    let smallerThan5 n = n < 5
    // списки 

    let list1 = readList 5
    Console.WriteLine("Список list1:")
    printList list1

    let resList = reduce list1 sum isEven 0
    Console.WriteLine("сумма четных элементов списка:" + resList.ToString())
    let minList = minElement list1
    Console.WriteLine("мин элемент:" + minList.ToString())
    0
