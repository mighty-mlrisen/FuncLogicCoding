module NumberUI
open Number

let run_method method number =
     method number
 
let choose_function n =
    match n with
    | "1" -> countEvenNotCoprime
    | "2" -> maxDigitNotDiv3
    | "3" -> productNum
    | _ -> failwith "Неверный метод"