module TraversalNumber

let rec traversalNumber n (f:int->int->int) acc = 
    match n with
    | 0 -> acc
    | n -> traversalNumber (n / 10) f (f acc (n%10))


    