open System
open IPrint
open Shape
open ShapeDiscriminated


let rectangle = Rectangle(2.0, 4.0)
let square =  Square(2.0)
let circle = Circle(5.0)


let rectangle2 = new Rectangle(2.0, 4.0)
let square2 = new Square(2.0)
let circle2 = new Circle(5.0)

(rectangle :> IPrint).Print()  
(square :> IPrint).Print()
(circle :> IPrint).Print()

Console.WriteLine("\nАлгебраический тип: \n")
(rectangle2 :> IPrint).Print()  
(square2 :> IPrint).Print()
(circle2 :> IPrint).Print()