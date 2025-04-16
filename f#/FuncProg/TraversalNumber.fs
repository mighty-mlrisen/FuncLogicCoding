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


let rec gcd x y= 
    match y with
    | 0 -> x
    | _ -> gcd y (x % y)

let coprime x y = 
    gcd x y = 1

let traversalNumberCoprime n (f:int->int->int) init =
    let rec traversal n acc i =
        match i with
        | 0 -> acc
        | _ -> 
            let newAcc = 
                match coprime n i with
                | true -> f acc i
                | false -> acc
            traversal n newAcc (i-1)
    traversal n init n


let eulerNumber n =
    traversalNumberCoprime n (fun x y -> x + 1) 0


let traversalNumberCoprimeСondition n (f:int->int->int) init (condition:int->bool) =
    let rec traversal n acc i =
        match i with
        | 0 -> acc
        | _ -> 
            let new_i = i - 1
            let flag = condition i
            match flag, coprime n i with
            | true,true -> traversal n (f acc i) new_i
            | _, _ -> traversal n acc new_i
    traversal n init n
            
    