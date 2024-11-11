using DinamicStructData;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DinamicStructWPF
{
    public partial class MainWindow : Window
    {
        private NodeAndStack.Stack<string> stack = new NodeAndStack.Stack<string>(); // Инициализация стека
        private QueueHandler queueHandler; // Обработчик очереди
        private CustomLinkedList<int> customList;

        public MainWindow()
        {
            InitializeComponent(); // Обязательно вызываем инициализацию компонентов
            queueHandler = new QueueHandler(); // Инициализация обработчика очереди
            customList = new CustomLinkedList<int>();
        }
        private void CustomLinkedListInputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CustomLinkedListInputTextBox.Text == "Введите значение" && CustomLinkedListInputTextBox.Foreground == Brushes.Gray)
            {
                CustomLinkedListInputTextBox.Text = string.Empty;
                CustomLinkedListInputTextBox.Foreground = Brushes.Black;
            }
        }

        private void CustomLinkedListInputTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CustomLinkedListInputTextBox.Text))
            {
                CustomLinkedListInputTextBox.Text = "Введите значение";
                CustomLinkedListInputTextBox.Foreground = Brushes.Gray;
            }
        }

        // Event handler for ComboBox selection change
        private void DataStructureSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataStructureSelector == null) return;

            string selectedStructure = (DataStructureSelector.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Проверяем наличие StackPanel и других панелей перед доступом к ним
            if (StackPanel != null) StackPanel.Visibility = Visibility.Collapsed;
            if (QueuePanel != null) QueuePanel.Visibility = Visibility.Collapsed;
            if (PostfixPanel != null) PostfixPanel.Visibility = Visibility.Collapsed;
            if (CustomLinkedListPanel != null) CustomLinkedListPanel.Visibility = Visibility.Collapsed;

            // Show the selected panel
            switch (selectedStructure)
            {
                case "Стек":
                    if (StackPanel != null) StackPanel.Visibility = Visibility.Visible;
                    break;
                case "Очередь":
                    if (QueuePanel != null) QueuePanel.Visibility = Visibility.Visible;
                    break;
                case "Постфиксная запись":
                    if (PostfixPanel != null) PostfixPanel.Visibility = Visibility.Visible;
                    break;
                case "Пользовательский связный список":
                    if (CustomLinkedListPanel != null) CustomLinkedListPanel.Visibility = Visibility.Visible;
                    break;
            }
        }

        // Event handler for adding an element to the custom linked list
        private void AddToCustomListButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CustomLinkedListInputTextBox.Text, out int value))
            {
                customList.AddToFront(value);
                CustomLinkedListOutputTextBlock.Text += $"Добавлено: {value}\n";
                customList.PrintList(CustomLinkedListOutputTextBlock);
            }
            else
            {
                CustomLinkedListOutputTextBlock.Text += "Ошибка: Введите корректное число.\n";
            }
        }

        // Event handler for reversing the custom linked list
        private void ReverseCustomListButton_Click(object sender, RoutedEventArgs e)
        {
            customList.Reverse();
            CustomLinkedListOutputTextBlock.Text += "Список перевернут.\n";
            customList.PrintList(CustomLinkedListOutputTextBlock);
        }

        // Event handler for moving the last element to the front
        private void MoveLastToFirstButton_Click(object sender, RoutedEventArgs e)
        {
            customList.MoveLastToFirst();
            CustomLinkedListOutputTextBlock.Text += "Последний элемент перемещен в начало.\n";
            customList.PrintList(CustomLinkedListOutputTextBlock);
        }

        // Event handler for moving the first element to the end
        private void MoveFirstToLastButton_Click(object sender, RoutedEventArgs e)
        {
            customList.MoveFirstToLast();
            CustomLinkedListOutputTextBlock.Text += "Первый элемент перемещен в конец.\n";
            customList.PrintList(CustomLinkedListOutputTextBlock);
        }


        // Обработчик нажатия на кнопку для работы со стеком
        private void ProcessStackButton_Click(object sender, RoutedEventArgs e)
        {
            if (StackInputTextBox == null || StackOutputTextBlock == null)
                return; // Проверка на null для TextBox и TextBlock

            string input = StackInputTextBox.Text;
            if (string.IsNullOrWhiteSpace(input))
            {
                StackOutputTextBlock.Text = "Введите команды для обработки стека.\n";
                return;
            }

            string[] commands = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            StackOutputTextBlock.Text = $"Обработка команд: {input}\n";

            foreach (string command in commands)
            {
                if (command.StartsWith("1,"))
                {
                    string value = command.Substring(2);
                    stack.Push(value);
                    StackOutputTextBlock.Text += $"Добавлено в стек: {value}\n";
                }
                else if (command == "2")
                {
                    var result = stack.Pop();
                    StackOutputTextBlock.Text += result != null ? $"Удалено из стека: {result}\n" : "Стек пуст. Операция удаления невозможна.\n";
                }
                else if (command == "3")
                {
                    var top = stack.Top();
                    StackOutputTextBlock.Text += top != null ? $"Верхний элемент стека: {top}\n" : "Стек пуст. Операция просмотра невозможна.\n";
                }
                else if (command == "4")
                {
                    bool isEmpty = stack.IsEmpty();
                    StackOutputTextBlock.Text += $"Стек пуст? {isEmpty}\n";
                }
                else if (command == "5")
                {
                    StackOutputTextBlock.Text += "Элементы стека:\n";
                    stack.Print(StackOutputTextBlock);
                }
                else
                {
                    StackOutputTextBlock.Text += $"Неизвестная команда: {command}\n";
                }
            }
        }

        // Обработчик нажатия на кнопку для работы с очередью
        private void ProcessQueueButton_Click(object sender, RoutedEventArgs e)
        {
            if (QueueInputTextBox == null || QueueOutputTextBlock == null)
                return; // Проверка на null для TextBox и TextBlock

            string input = QueueInputTextBox.Text;
            if (string.IsNullOrWhiteSpace(input))
            {
                QueueOutputTextBlock.Text = "Введите команды для обработки очереди.\n";
                return;
            }

            string[] commands = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            QueueOutputTextBlock.Text = "Обработка команд для очереди:\n";
            queueHandler.ProcessCommands(commands, QueueOutputTextBlock);
        }

        // Обработчик нажатия на кнопку для вычисления постфиксного выражения
        private void EvaluatePostfixButton_Click(object sender, RoutedEventArgs e)
        {
            if (PostfixInputTextBox == null || PostfixOutputTextBlock == null)
                return; // Проверка на null для TextBox и TextBlock

            string input = PostfixInputTextBox.Text;
            if (string.IsNullOrWhiteSpace(input))
            {
                PostfixOutputTextBlock.Text += "Введите постфиксное выражение.\n";
                return;
            }

            string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            PostfixOutputTextBlock.Text += $"Вычисление постфиксного выражения: {input}\n";

            try
            {
                double result = EvaluatePostfix(tokens);
                PostfixOutputTextBlock.Text += $"Результат: {result}\n";
            }
            catch (Exception ex)
            {
                PostfixOutputTextBlock.Text += $"Ошибка: {ex.Message}\n";
            }
        }

        // Метод для вычисления постфиксного выражения
        private double EvaluatePostfix(string[] tokens)
        {
            Stack<double> stack = new Stack<double>();

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double number))
                {
                    // Если токен - число, кладем его в стек
                    stack.Push(number);
                    PostfixOutputTextBlock.Text += $"Число {number} добавлено в стек.\n";
                }
                else
                {
                    // Если токен - оператор или функция
                    try
                    {
                        switch (token)
                        {
                            case "+":
                                stack.Push(stack.Pop() + stack.Pop());
                                PostfixOutputTextBlock.Text += "Выполнена операция сложения.\n";
                                break;
                            case "-":
                                {
                                    double b = stack.Pop();
                                    double a = stack.Pop();
                                    stack.Push(a - b);
                                    PostfixOutputTextBlock.Text += "Выполнена операция вычитания.\n";
                                }
                                break;
                            case "*":
                                stack.Push(stack.Pop() * stack.Pop());
                                PostfixOutputTextBlock.Text += "Выполнена операция умножения.\n";
                                break;
                            case ":":
                            case "/":
                                {
                                    double b = stack.Pop();
                                    double a = stack.Pop();
                                    if (b == 0) throw new DivideByZeroException("Деление на ноль");
                                    stack.Push(a / b);
                                    PostfixOutputTextBlock.Text += "Выполнена операция деления.\n";
                                }
                                break;
                            case "^":
                                {
                                    double exponent = stack.Pop();
                                    double baseValue = stack.Pop();
                                    stack.Push(Math.Pow(baseValue, exponent));
                                    PostfixOutputTextBlock.Text += "Выполнена операция возведения в степень.\n";
                                }
                                break;
                            case "ln":
                                stack.Push(Math.Log(stack.Pop()));
                                PostfixOutputTextBlock.Text += "Выполнена операция логарифма.\n";
                                break;
                            case "cos":
                                stack.Push(Math.Cos(stack.Pop()));
                                PostfixOutputTextBlock.Text += "Выполнена операция косинуса.\n";
                                break;
                            case "sin":
                                stack.Push(Math.Sin(stack.Pop()));
                                PostfixOutputTextBlock.Text += "Выполнена операция синуса.\n";
                                break;
                            case "sqrt":
                                stack.Push(Math.Sqrt(stack.Pop()));
                                PostfixOutputTextBlock.Text += "Выполнена операция квадратного корня.\n";
                                break;
                            default:
                                throw new InvalidOperationException($"Неизвестный оператор или функция: {token}");
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        PostfixOutputTextBlock.Text += $"Ошибка выполнения: {ex.Message}\n";
                    }
                }
            }

            if (stack.Count != 1)
            {
                throw new InvalidOperationException("Неправильное выражение в постфиксной записи.");
            }

            return stack.Pop();
        }
    }
}
