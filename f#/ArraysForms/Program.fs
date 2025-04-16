open System
open System.Windows.Forms
open System.Drawing
open System.Linq

[<EntryPoint>]
[<STAThread>]
let main _ =
    let form = new Form(Text = "Нахождение рациональных корней многочлена", 
                       Width = 600, 
                       Height = 450,
                       Font = new Font("Segoe UI", 10.0f),
                       StartPosition = FormStartPosition.CenterScreen,
                       FormBorderStyle = FormBorderStyle.FixedDialog,
                       MaximizeBox = false)

    let mainPanel = new Panel(Dock = DockStyle.Fill, Padding = Padding(20))

    let groupBox1 = new GroupBox(Text = "Коэффициенты многочлена",
                                Height = 150,
                                Dock = DockStyle.Top,
                                Padding = Padding(10))
    let lblCoefficients = new Label(Text = "Вводите коэффициенты через запятую", 
                                    Dock = DockStyle.Top,
                                    Margin = Padding(0, 0, 0, 5))
    let txtCoefficients = new TextBox(Dock = DockStyle.Top)
    groupBox1.Controls.AddRange([| lblCoefficients; txtCoefficients |])

    let btnFindRoots = new Button(Text = "Найти рациональные корни",
                                 Height = 40,
                                 Dock = DockStyle.Top,
                                 Top = groupBox1.Bottom + 20,
                                 BackColor = Color.LightSteelBlue,
                                 FlatStyle = FlatStyle.Flat)
    btnFindRoots.FlatAppearance.BorderSize <- 0

    let groupBoxResult = new GroupBox(Text = "Результат",
                                     Dock = DockStyle.Fill,
                                     Top = btnFindRoots.Bottom + 10,
                                     Padding = Padding(10))
    let txtResult = new TextBox(Multiline = true,
                               Dock = DockStyle.Fill,
                               ScrollBars = ScrollBars.Vertical,
                               Font = new Font("Consolas", 10.0f),
                               ReadOnly = true)
    groupBoxResult.Controls.Add(txtResult)

    let findRationalRoots (coefficients: int list) =
        let divisor n = 
            [1..Math.Abs(n)] |> List.filter (fun i -> n % i = 0)
        
        let possibleRoots = 
            let divisorsOfConstant = divisor (List.last coefficients)
            let divisorsOfLeading = divisor (List.head coefficients)
            divisorsOfConstant |> List.collect (fun d -> divisorsOfLeading |> List.map (fun dl -> float d / float dl))
        
        let evaluate (x: float) =
            coefficients |> List.rev |> List.indexed |> List.sumBy (fun (i, coef) -> coef * Math.Pow(x, float i))

        possibleRoots
        |> List.filter (fun root -> Math.Abs(evaluate root) < 0.0001) // погрешность
        |> List.distinct

    let parseCoefficients (input: string) =
        try
            input.Split([|','; ' '|], StringSplitOptions.RemoveEmptyEntries)
            |> Array.map (fun s -> s.Trim())
            |> Array.map Int32.Parse
            |> List.ofArray
            |> Some
        with _ -> None

    btnFindRoots.Click.Add(fun _ ->
        match parseCoefficients txtCoefficients.Text with
        | Some coefficients ->
            let roots = findRationalRoots coefficients
            if roots.IsEmpty then
                txtResult.Text <- "Рациональные корни не найдены."
            else
                txtResult.Text <- sprintf "Найденные рациональные корни:\r\n[%s]" 
                                    (String.Join("; ", roots.Select(fun r -> r.ToString("G6"))))
        | None ->
            MessageBox.Show("Некорректный формат коэффициентов!\nПример правильного ввода: 1, -2, 3", 
                          "Ошибка ввода", 
                          MessageBoxButtons.OK, 
                          MessageBoxIcon.Error) |> ignore
    )

    mainPanel.Controls.AddRange([|
        groupBoxResult :> Control
        btnFindRoots :> Control
        groupBox1 :> Control
    |])

    form.Controls.Add(mainPanel)
    Application.Run(form)
    0
