open System
open System.Windows.Forms
open System.Drawing

[<EntryPoint>]
[<STAThread>]
let main _ =
    let form = new Form(Text = "Вычисление логарифма", Width = 450, Height = 250,
                       Font = new Font("Arial", 12.0f),
                       StartPosition = FormStartPosition.CenterScreen)

    let lblNum = new Label(Text = "Число:", Top = 30, Left = 30, Width = 100)
    let txtNum = new TextBox(Top = 30, Left = 140, Width = 200)

    let lblResult = new Label(Text = "Результат:", Top = 80, Left = 30, Width = 100)
    let txtResult = new TextBox(Top = 80, Left = 140, Width = 200, ReadOnly = true)

    let btnEqual = new Button(Text = "=", Top = 130, Left = 140, Width = 100, Height = 30)

    let tryParseDouble (text: string) =
        match Double.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture) with
        | true, num -> Some num
        | false, _ -> None

    let calculateLog num =
        try
            let result = Math.Log(num)
            result.ToString("G6")
        with
        | :? System.ArgumentOutOfRangeException -> "Ошибка: число должно быть больше 0"

    let handleLogCalculation () =
        match tryParseDouble txtNum.Text with
        | Some num when num > 0.0 -> 
            txtResult.Text <- calculateLog num
        | Some num when num <= 0.0 -> 
            MessageBox.Show("Число должно быть больше 0!", "Ошибка ввода", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning) |> ignore
        | None -> 
            MessageBox.Show("Некорректное число!", "Ошибка ввода", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning) |> ignore

    btnEqual.Click.Add(fun _ -> handleLogCalculation())

    form.Controls.AddRange [|
        lblNum :> Control; txtNum :> Control;
        lblResult :> Control; txtResult :> Control;
        btnEqual :> Control
    |]

    Application.Run(form)
    0
