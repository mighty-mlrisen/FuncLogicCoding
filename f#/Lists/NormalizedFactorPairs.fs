module NormalizedFactorPairs

open System

let rec gcd a b =
    if b = 0 then a
    else gcd b (a % b)

let getNormalizedPairs number =
    let factors = 
        [1..number] 
        |> List.filter (fun i -> number % i = 0)

    let rawPairs =
        factors
        |> List.map (fun a -> (a, number / a))

    rawPairs
    |> List.map (fun (m, n) ->
        let common = gcd m n
        (m / common, n / common))
    |> List.distinct
    |> List.sort
