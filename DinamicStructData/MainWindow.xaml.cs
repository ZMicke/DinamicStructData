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
        private CustomLinkedList<int> additionalList;

        public MainWindow()
        {
            InitializeComponent(); // Обязательно вызываем инициализацию компонентов
            queueHandler = new QueueHandler(); // Инициализация обработчика очереди
            additionalList = new CustomLinkedList<int>();
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
        // Count distinct elements
        private void CountDistinctButton_Click(object sender, RoutedEventArgs e)
        {
            int count = additionalList.CountDistinct();
            CustomLinkedListOutputTextBlock.Text += $"Количество уникальных элементов: {count}\n";
        }
        // Remove non-unique elements
        private void RemoveNonUniqueButton_Click(object sender, RoutedEventArgs e)
        {
            additionalList.RemoveNonUnique();
            CustomLinkedListOutputTextBlock.Text += "Неуникальные элементы удалены.\n";
            additionalList.PrintList(CustomLinkedListOutputTextBlock);
        }
        // Insert another list after the first occurrence of a value
        private void InsertListButton_Click(object sender, RoutedEventArgs e)
        {
            // Add example elements to the additional list
            additionalList.AddToFront(99);
            additionalList.AddToFront(98);

            if (int.TryParse(CustomLinkedListInputTextBox.Text, out int value))
            {
                additionalList.InsertListAfterFirstOccurrence(additionalList, value);
                CustomLinkedListOutputTextBlock.Text += $"Список вставлен после первого вхождения {value}.\n";
                additionalList.PrintList(CustomLinkedListOutputTextBlock);
            }
            else
            {
                CustomLinkedListOutputTextBlock.Text += "Ошибка: Введите корректное число.\n";
            }
        }
        // Insert an element in a sorted list
        private void InsertSortedButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CustomLinkedListInputTextBox.Text, out int value))
            {
                additionalList.InsertSorted(value);
                CustomLinkedListOutputTextBlock.Text += $"Элемент {value} вставлен в упорядоченный список.\n";
                additionalList.PrintList(CustomLinkedListOutputTextBlock);
            }
            else
            {
                CustomLinkedListOutputTextBlock.Text += "Ошибка: Введите корректное число.\n";
            }
        }
        // Remove all occurrences of a value
        private void RemoveAllOccurrencesButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CustomLinkedListInputTextBox.Text, out int value))
            {
                additionalList.RemoveAll(value);
                CustomLinkedListOutputTextBlock.Text += $"Все вхождения {value} удалены.\n";
                additionalList.PrintList(CustomLinkedListOutputTextBlock);
            }
            else
            {
                CustomLinkedListOutputTextBlock.Text += "Ошибка: Введите корректное число.\n";
            }
        }
        // Insert a value before the first occurrence of another value
        private void InsertBeforeButton_Click(object sender, RoutedEventArgs e)
        {
            // Example values for insertion
            int newValue = 100; // New value to insert
            if (int.TryParse(CustomLinkedListInputTextBox.Text, out int existingValue))
            {
                additionalList.InsertBefore(newValue, existingValue);
                CustomLinkedListOutputTextBlock.Text += $"Значение {newValue} вставлено перед первым вхождением {existingValue}.\n";
                additionalList.PrintList(CustomLinkedListOutputTextBlock);
            }
            else
            {
                CustomLinkedListOutputTextBlock.Text += "Ошибка: Введите корректное число.\n";
            }
        }
        // Append another list to the custom list
        private void AppendListButton_Click(object sender, RoutedEventArgs e)
        {
            // Add example elements to the additional list
            additionalList.AddToFront(50);
            additionalList.AddToFront(60);

            additionalList.AppendList(additionalList);
            CustomLinkedListOutputTextBlock.Text += "Список дописан.\n";
            additionalList.PrintList(CustomLinkedListOutputTextBlock);
        }
        // Split the list into two by the first occurrence of a value
        private void SplitListButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CustomLinkedListInputTextBox.Text, out int value))
            {
                additionalList.SplitByFirstOccurrence(value, out CustomLinkedList<int> list1, out CustomLinkedList<int> list2);
                CustomLinkedListOutputTextBlock.Text += $"Список разделен по первому вхождению {value}.\n";
                CustomLinkedListOutputTextBlock.Text += "Первая часть:\n";
                list1.PrintList(CustomLinkedListOutputTextBlock);
                CustomLinkedListOutputTextBlock.Text += "Вторая часть:\n";
                list2.PrintList(CustomLinkedListOutputTextBlock);
            }
            else
            {
                CustomLinkedListOutputTextBlock.Text += "Ошибка: Введите корректное число.\n";
            }
        }
        // Double the list
        private void DoubleListButton_Click(object sender, RoutedEventArgs e)
        {
            additionalList.DoubleList();
            CustomLinkedListOutputTextBlock.Text += "Список удвоен.\n";
            additionalList.PrintList(CustomLinkedListOutputTextBlock);
        }
        // Swap two elements
        private void SwapElementsButton_Click(object sender, RoutedEventArgs e)
        {
            // Example values for swapping
            int elem1 = 10;
            int elem2 = 20;

            additionalList.SwapElements(elem1, elem2);
            CustomLinkedListOutputTextBlock.Text += $"Элементы {elem1} и {elem2} поменяны местами.\n";
            additionalList.PrintList(CustomLinkedListOutputTextBlock);
        }

        // Event handler for adding an element to the custom linked list
        private void AddToCustomListButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CustomLinkedListInputTextBox.Text, out int value))
            {
                additionalList.AddToFront(value);
                CustomLinkedListOutputTextBlock.Text += $"Добавлено: {value}\n";
                additionalList.PrintList(CustomLinkedListOutputTextBlock);
            }
            else
            {
                CustomLinkedListOutputTextBlock.Text += "Ошибка: Введите корректное число.\n";
            }
        }

        // Event handler for reversing the custom linked list
        private void ReverseCustomListButton_Click(object sender, RoutedEventArgs e)
        {
            additionalList.Reverse();
            CustomLinkedListOutputTextBlock.Text += "Список перевернут.\n";
            additionalList.PrintList(CustomLinkedListOutputTextBlock);
        }

        // Event handler for moving the last element to the front
        private void MoveLastToFirstButton_Click(object sender, RoutedEventArgs e)
        {
            additionalList.MoveLastToFirst();
            CustomLinkedListOutputTextBlock.Text += "Последний элемент перемещен в начало.\n";
            additionalList.PrintList(CustomLinkedListOutputTextBlock);
        }

        // Event handler for moving the first element to the end
        private void MoveFirstToLastButton_Click(object sender, RoutedEventArgs e)
        {
            additionalList.MoveFirstToLast();
            CustomLinkedListOutputTextBlock.Text += "Первый элемент перемещен в конец.\n";
            additionalList.PrintList(CustomLinkedListOutputTextBlock);
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
