module Number

open TraversalNumber

let countEvenNotCoprime n =
    let countEven = n / 2
    let evenCoprime = traversalNumberCoprimeСondition n (fun x y -> x + 1) 0 (fun x -> x % 2 = 0)
    let res = countEven - evenCoprime
    res

let maxDigit a b =
     match (a, b) with
     | (a, b) when a >= b -> a
     | _ -> b

let maxDigitNotDiv3 n = 
    let res = traversalNumberСondition n maxDigit 0 (fun x -> x % 3 <> 0)
    res