module TraversalNumber

let rec traversalNumber n (f:int->int->int) acc = 
    match n with
    | 0 -> acc
    | n -> traversalNumber (n / 10) f (f acc (n%10))


let rec traversalNumberСondition n (f:int->int->int) acc (condition:int->bool) =
    match n with
    | 0 -> acc 
    | n ->
        let digit = n % 10
        let newAcc = 
            match condition digit with
            | true -> (f acc digit)
            | false -> acc
        traversalNumberСondition (n / 10) f newAcc condition


    