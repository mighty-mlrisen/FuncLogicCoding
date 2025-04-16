module Shape

open System
open IPrint

[<AbstractClass>]
type Shape() =
    abstract member GetArea : unit -> float

type Rectangle(width: float, height: float) =
    inherit Shape()
    member val width: float = width with get
    member val height: float = height with get

    override this.GetArea() = this.width * this.height

    override this.ToString() = 
        $"Rectangle((width = {this.width}, height = {this.height}), area = {this.GetArea()})"
    
    interface IPrint with
        member this.Print() = Console.WriteLine(this.ToString())

type Square(side: float) =
    inherit Rectangle(side, side)

    override this.ToString() = 
        $"Square((side = {this.width}), area = {this.GetArea()})"

    interface IPrint with
        member this.Print() = Console.WriteLine(this.ToString())

type Circle(radius: float) =
    inherit Shape()

    member val radius: float = radius with get

    override this.GetArea() = Math.PI * this.radius * this.radius

    override this.ToString() = 
        $"Circle((radius = {this.radius}), area = {this.GetArea()})"

    interface IPrint with
        member this.Print() = Console.WriteLine(this.ToString())