module Lists
open System

let readList n = 
    let rec read n acc =
        match n with 
        | 0 -> acc
        | k -> 
            let el = Console.ReadLine() |> int
            let newAcc = acc@[el]
            read (k-1) newAcc
    read n []

let rec printList list = 
    match list with 
    | [] -> Console.ReadKey()
    | head::tail -> 
        Console.WriteLine(head.ToString())
        printList tail

let rec reduce list (f:int->int->int) (predicate:int->bool) acc = 
    match list with
    | [] -> acc
    | head::tail ->
        let newAcc = 
            match predicate head with
            | true -> f acc head
            | false -> acc
        reduce tail f predicate newAcc


let minElement list = 
    match list with 
    | [] -> 0
    | h::t -> 
        reduce t min (fun a -> true ) h




