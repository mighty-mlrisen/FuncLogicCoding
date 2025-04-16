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


let smallDiv n =
    let rec check i =
        match i * i > n with
        | true -> n
        | false ->
            match n % i = 0 with
            | true -> i
            | false -> check (i + (if i = 2 then 1 else 2))
    match n % 2 = 0 with
    | true -> 2
    | false -> check 3

let productNum n = 
    let del = smallDiv n

    let maxNonCoprimeNotDivBySd =
         let isNotCoprime x = gcd n x > 1
         let doesNotDivideSd x = x % del <> 0
         let predicate x = isNotCoprime x && doesNotDivideSd x
         let rec findMax candidate =
             if candidate = 0 then 0 
             elif predicate candidate then candidate
             else findMax (candidate - 1)
         findMax n

    let sumDigitsLessThan5 =
         let isLessThan5 x = x < 5
         traversalNumberСondition n (fun a b -> a + b) 0 isLessThan5

    maxNonCoprimeNotDivBySd * sumDigitsLessThan5