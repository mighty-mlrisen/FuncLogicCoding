open System

type TaskMessage =
    | SayHello of string
    | Multiply of int * int
    | Terminate

let workerAgent = 
    MailboxProcessor.Start(fun inbox ->
        let rec workerLoop () = 
            async {
                let! msg = inbox.Receive()
                
                match msg with
                | SayHello name -> 
                    Console.WriteLine("Привет, {0}!", name)
                    return! workerLoop()
                
                | Multiply (x, y) ->
                    Console.WriteLine("Произведение {0} и {1}: {2}", x, y, (x * y))
                    return! workerLoop()
                
                | Terminate ->
                    Console.WriteLine("Агент завершает свою работу.")
                    return ()
            }
        workerLoop ()
    )

workerAgent.Post(SayHello "Артём")
workerAgent.Post(Multiply(3, 9))
workerAgent.Post(Terminate)

Threading.Thread.Sleep(1000)
