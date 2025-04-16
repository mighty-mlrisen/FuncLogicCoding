open System
open FParsec

// Алгебраический тип для арифметических выражений
type ArithmeticExpr =
    | Value of int
    | Addition of ArithmeticExpr * ArithmeticExpr
    | Subtraction of ArithmeticExpr * ArithmeticExpr

// Парсер для чисел
let valueParser : Parser<ArithmeticExpr, unit> =
    pint32 |>> Value

// Определение парсера для выражений (с отложенным парсером)
let expressionParser, expressionParserRef = createParserForwardedToRef()

// Парсер для сложения и вычитания
let addSubParser =
    let operatorParser = OperatorPrecedenceParser<ArithmeticExpr, unit, unit>()
    let expr = operatorParser.ExpressionParser
    operatorParser.TermParser <- valueParser <|> between (pchar '(') (pchar ')') expressionParser
    operatorParser.AddOperator(InfixOperator("+", spaces, 1, Associativity.Left, fun x y -> Addition(x, y)))
    operatorParser.AddOperator(InfixOperator("-", spaces, 1, Associativity.Left, fun x y -> Subtraction(x, y)))
    expr

expressionParserRef.Value <- addSubParser

// Функция для разбора строки
let parseExpression input =
    match run expressionParser input with
    | Success(result, _, _) -> result
    | Failure(errorMsg, _, _) -> failwith errorMsg

// Функция для печати результата разбора
let printParsedResult expr =
    let rec toString = function
        | Value n -> n.ToString()
        | Addition (e1, e2) -> $"({toString e1} + {toString e2})"
        | Subtraction (e1, e2) -> $"({toString e1} - {toString e2})"
    
    let resultStr = toString expr
    Console.WriteLine($"Разобранное выражение: {resultStr}")

// Входная строка для разбора
let inputStr = "(6+2)-(3+5)-(4+1)"
Console.WriteLine($"Входная строка: {inputStr}")
let parsedExpression = parseExpression inputStr
printParsedResult parsedExpression
