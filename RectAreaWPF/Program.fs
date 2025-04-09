open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media

// Определение главного окна
type MainWindow() as this =
    inherit Window()

    do
        // Настройки окна
        this.Title <- "Вычисление площади прямоугольника"
        this.Width <- 400.0
        this.Height <- 300.0
        this.Background <- Brushes.White

        // Создание контейнера для элементов (StackPanel)
        let stackPanel = new StackPanel()
        stackPanel.Margin <- new Thickness(20.0)

        // Метки для ввода длины и ширины
        let lblLength = new TextBlock(Text = "Введите длину прямоугольника:")
        lblLength.Margin <- new Thickness(0.0, 0.0, 0.0, 5.0)

        let lblWidth = new TextBlock(Text = "Введите ширину прямоугольника:")
        lblWidth.Margin <- new Thickness(0.0, 10.0, 0.0, 5.0)

        // Поля для ввода длины и ширины
        let txtLength = new TextBox()
        txtLength.Margin <- new Thickness(0.0, 0.0, 0.0, 5.0)
        txtLength.Width <- 200.0

        let txtWidth = new TextBox()
        txtWidth.Margin <- new Thickness(0.0, 0.0, 0.0, 20.0)
        txtWidth.Width <- 200.0

        // Кнопка для вычисления площади
        let btnCalculate = new Button(Content = "Вычислить площадь")
        btnCalculate.Width <- 200.0
        btnCalculate.Margin <- new Thickness(0.0, 0.0, 0.0, 10.0)

        // Метка для вывода результата
        let lblResult = new TextBlock(Text = "Площадь:")
        lblResult.Margin <- new Thickness(0.0, 0.0, 0.0, 5.0)

        let txtResult = new TextBlock()
        txtResult.Margin <- new Thickness(0.0, 0.0, 0.0, 10.0)

        // Добавление элементов в контейнер
        stackPanel.Children.Add(lblLength) |> ignore
        stackPanel.Children.Add(txtLength) |> ignore
        stackPanel.Children.Add(lblWidth) |> ignore
        stackPanel.Children.Add(txtWidth) |> ignore
        stackPanel.Children.Add(btnCalculate) |> ignore
        stackPanel.Children.Add(lblResult) |> ignore
        stackPanel.Children.Add(txtResult) |> ignore

        // Установка контейнера как содержимого окна
        this.Content <- stackPanel

        // Обработчик события нажатия на кнопку
        btnCalculate.Click.Add(fun _ ->
            try
                // Чтение значений из текстовых полей
                let length = Double.Parse(txtLength.Text)
                let width = Double.Parse(txtWidth.Text)

                // Вычисление площади
                let area = length * width

                // Вывод результата
                txtResult.Text <- sprintf "Площадь прямоугольника: %.2f" area
            with
                | :? FormatException ->
                    MessageBox.Show("Пожалуйста, введите правильные числовые значения для длины и ширины.", 
                                     "Ошибка ввода", 
                                     MessageBoxButton.OK, 
                                     MessageBoxImage.Error) |> ignore
        )

// Запуск приложения
[<STAThread>]
[<EntryPoint>]
let main argv =
    let app = new Application()
    let window = new MainWindow()
    app.Run(window)
    0
