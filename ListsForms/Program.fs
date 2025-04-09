open System
open System.Windows.Forms
open System.Drawing

[<EntryPoint>]
[<STAThread>]
let main _ =
    let form = new Form(Text = "Логическая проверка элементов списка", 
                       Width = 600, 
                       Height = 450,
                       Font = new Font("Segoe UI", 10.0f),
                       StartPosition = FormStartPosition.CenterScreen,
                       FormBorderStyle = FormBorderStyle.FixedDialog,
                       MaximizeBox = false)

    let mainPanel = new Panel(Dock = DockStyle.Fill, Padding = Padding(20))

    // Ввод списка чисел
    let groupBox1 = new GroupBox(Text = "Ввод чисел списка",
                                Height = 100,
                                Dock = DockStyle.Top,
                                Padding = Padding(10))
    let lblInput = new Label(Text = "Вводите числа через запятую", 
                            Dock = DockStyle.Top,
                            Margin = Padding(0, 0, 0, 5))
    let txtInput = new TextBox(Dock = DockStyle.Top)
    groupBox1.Controls.AddRange([| lblInput; txtInput |])

    // Кнопка для проверки
    let btnCheck = new Button(Text = "Проверить", 
                              Height = 40,
                              Dock = DockStyle.Top,
                              Top = groupBox1.Bottom + 20,
                              BackColor = Color.LightSteelBlue,
                              FlatStyle = FlatStyle.Flat)
    btnCheck.FlatAppearance.BorderSize <- 0

    // Результат проверки
    let groupBoxResult = new GroupBox(Text = "Результат проверки",
                                     Dock = DockStyle.Fill,
                                     Top = btnCheck.Bottom + 10,
                                     Padding = Padding(10))
    let txtResult = new TextBox(Multiline = true,
                               Dock = DockStyle.Fill,
                               ScrollBars = ScrollBars.Vertical,
                               Font = new Font("Consolas", 10.0f),
                               ReadOnly = true)
    groupBoxResult.Controls.Add(txtResult)

    // Функция для логической проверки (находит хотя бы одно число больше 10)
    let checkCondition (numbers: int list) =
        numbers |> List.exists (fun x -> x > 10)

    // Парсинг ввода
    let parseInput (input: string) =
        try
            input.Split([|','; ' '|], StringSplitOptions.RemoveEmptyEntries)
            |> Array.map (fun s -> s.Trim())
            |> Array.map Int32.Parse
            |> List.ofArray
            |> Some
        with _ -> None

    // Обработчик нажатия кнопки
    btnCheck.Click.Add(fun _ ->
        match parseInput txtInput.Text with
        | Some numbers ->
            if checkCondition numbers then
                txtResult.Text <- "Есть число больше 10!"
            else
                txtResult.Text <- "Нет чисел больше 10."
        | None ->
            MessageBox.Show("Некорректный формат ввода!\nПример правильного ввода: 1, 2, 15", 
                          "Ошибка ввода", 
                          MessageBoxButtons.OK, 
                          MessageBoxIcon.Error) |> ignore
    )

    // Добавление элементов на форму
    mainPanel.Controls.AddRange([|
        groupBoxResult :> Control
        btnCheck :> Control
        groupBox1 :> Control
    |])

    form.Controls.Add(mainPanel)
    Application.Run(form)
    0
