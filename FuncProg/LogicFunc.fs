module LogicFunc
open DigitSum



let factNumber n:int =
    let rec fact n acc = 
        match n with
        | 1 -> acc
        | _ -> fact (n-1) (acc * n)
    fact n 1

let logicFunc flag= 
    match flag with 
    | true -> digitSumDown
    | false -> factNumber
