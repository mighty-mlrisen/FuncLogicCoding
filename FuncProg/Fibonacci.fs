module Fibonacci
open System

let rec fibUp n =
    match n with
    | 0 | 1 -> n
    | _ -> fibUp (n - 1) + fibUp (n - 2)


let fibDown (n: int) =
    // x - пред , acc - тек , y - номер
    let rec fib x acc y =
        match y with
        | 0 -> acc
        | _ -> fib acc (acc + x) (y - 1)
        // чтобы на втором месте стоял ответ след итерации 
    fib 1 0 n


