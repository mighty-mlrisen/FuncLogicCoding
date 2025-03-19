module DigitSum



let rec digitSumUp (n:int) =
    if n < 10 then n
    else n%10 + digitSumUp (n / 10)



let digitSumDown (n:int) =
    let rec digitSum n curSum =
        if n = 0 then curSum
        else
            let n1 = n/10
            let digit = n%10
            let sum = curSum + digit
            digitSum n1 sum
    digitSum n 0