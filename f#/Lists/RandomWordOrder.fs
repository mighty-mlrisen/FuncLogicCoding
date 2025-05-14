module RandomWordOrder
open System

let mixWords (text: string) =
    let wordList = text.Split([|' '|], StringSplitOptions.RemoveEmptyEntries) |> Array.toList
    let randomizer = Random()
    wordList
    |> List.sortBy (fun _ -> randomizer.Next())
    |> String.concat " "
