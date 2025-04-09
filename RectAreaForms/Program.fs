open System
open System.Windows.Forms
open System.Drawing

[<EntryPoint>]
[<STAThread>]
let main _ =
    let form = new Form(Text = "Вычисление площади прямоугольника", Width = 400, Height = 300, 
                        Font = new Font("Arial", 12.0f),
                        StartPosition = FormStartPosition.CenterScreen)

    let lblLength = new Label(Text = "Длина:", Top = 30, Left = 30, Width = 100)
    let txtLength = new TextBox(Top = 30, Left = 140, Width = 200)

    let lblWidth = new Label(Text = "Ширина:", Top = 80, Left = 30, Width = 100)
    let txtWidth = new TextBox(Top = 80, Left = 140, Width = 200)

    let lblResult = new Label(Text = "Площадь:", Top = 180, Left = 30, Width = 100)
    let txtResult = new TextBox(Top = 180, Left = 140, Width = 200, ReadOnly = true)

    let btnCalculate = new Button(Text = "Вычислить", Top = 130, Left = 140, Width = 100, Height = 30)

    let tryParseDouble (text: string) =
        match Double.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture) with
        | true, num -> Some num
        | false, _ -> None

    let computeArea length width =
        length * width

    let handleCalculation _ =
        let lengthOpt = tryParseDouble txtLength.Text
        let widthOpt = tryParseDouble txtWidth.Text

        match lengthOpt, widthOpt with
        | Some length, Some width when length > 0.0 && width > 0.0 ->
            let area = computeArea length width
            txtResult.Text <- area.ToString("G6")
        | None, _ -> 
            MessageBox.Show("Некорректное значение длины!", "Ошибка ввода",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning) |> ignore
        | _, None -> 
            MessageBox.Show("Некорректное значение ширины!", "Ошибка ввода",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning) |> ignore
        | _ -> 
            MessageBox.Show("Длина и ширина должны быть положительными числами.", "Ошибка ввода",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning) |> ignore

    btnCalculate.Click.Add(handleCalculation)

    form.Controls.AddRange [|
        lblLength :> Control; txtLength :> Control;
        lblWidth :> Control; txtWidth :> Control;
        lblResult :> Control; txtResult :> Control;
        btnCalculate :> Control
    |]

    Application.Run(form)
    0
