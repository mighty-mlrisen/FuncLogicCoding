module ListUtils
open System

let mostCommonElement items =
    items
    |> List.countBy id
    |> List.sortByDescending snd
    |> List.head
    |> fst

let countSquaresFromList (nums: int list) =
    let baseSet = List.distinct nums
    nums
    |> List.filter (fun n ->
        baseSet
        |> List.exists (fun m -> m * m = n)
    )
    |> List.length

let sumOfDigits x:int =
    let rec helper n acc =
        if n = 0 then acc
        else
            let rest = n / 10
            let digit = n % 10
            helper rest (acc + digit)
    helper x 0

let totalDivisors value =
    if value = 0 then 0
    else
        let absVal = abs value
        [1..absVal] |> List.filter (fun d -> absVal % d = 0) |> List.length

let generateTriples (aList: int list) (bList: int list) (cList: int list) =
    let aSorted = aList |> List.sortByDescending id
    
    let bSorted = 
        bList 
        |> List.sortBy (fun n -> (sumOfDigits n, abs n))
    
    let cSorted = 
        cList 
        |> List.sortByDescending (fun n -> (totalDivisors n, abs n))
    
    List.zip3 aSorted bSorted cSorted

let inputAndSortByLength () =
    let rec collect acc =
        let entry = Console.ReadLine()
        if String.IsNullOrEmpty(entry) then acc
        else collect (entry :: acc)
    
    collect [] |> List.rev |> List.sortBy (fun s -> s.Length)
