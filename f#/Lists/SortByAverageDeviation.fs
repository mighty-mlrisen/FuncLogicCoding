module SortByAverageDeviation

open System

let sortByAverageDeviation (stringList: string list) =
    match stringList with
    | [] -> []
    | firstStr :: remainingStrs ->
        let firstAvg = 
            firstStr 
            |> Seq.averageBy float
        let squaredDeviation str =
            let currentAvg = str |> Seq.averageBy float
            (currentAvg - firstAvg) ** 2.0
        firstStr :: (remainingStrs |> List.sortBy squaredDeviation)
