using System;
using System.Collections.Generic;
using System.Windows.Controls;

public class QueueHandler
{
    private List<string> customQueue;
    private Queue<string> standardQueue;

    public QueueHandler()
    {
        customQueue = new List<string>();
        standardQueue = new Queue<string>();
    }

    // Вставка элемента в пользовательскую очередь
    public void EnqueueCustom(string element, TextBlock outputTextBlock)
    {
        customQueue.Add(element);
        outputTextBlock.Text += $"Добавлено в пользовательскую очередь: {element}\n";
    }

    // Удаление элемента из пользовательской очереди
    public void DequeueCustom(TextBlock outputTextBlock)
    {
        if (customQueue.Count == 0)
        {
            outputTextBlock.Text += "Пользовательская очередь пуста. Удаление невозможно.\n";
            return;
        }
        string removedElement = customQueue[0];
        customQueue.RemoveAt(0);
        outputTextBlock.Text += $"Удалено из пользовательской очереди: {removedElement}\n";
    }

    // Просмотр первого элемента пользовательской очереди
    public void PeekCustom(TextBlock outputTextBlock)
    {
        if (customQueue.Count == 0)
        {
            outputTextBlock.Text += "Пользовательская очередь пуста.\n";
            return;
        }
        outputTextBlock.Text += $"Первый элемент пользовательской очереди: {customQueue[0]}\n";
    }

    // Проверка на пустоту пользовательской очереди
    public void IsEmptyCustom(TextBlock outputTextBlock)
    {
        bool isEmpty = customQueue.Count == 0;
        outputTextBlock.Text += $"Пользовательская очередь пуста? {isEmpty}\n";
    }

    // Печать элементов пользовательской очереди
    public void PrintCustom(TextBlock outputTextBlock)
    {
        if (customQueue.Count == 0)
        {
            outputTextBlock.Text += "Пользовательская очередь пуста.\n";
            return;
        }
        outputTextBlock.Text += "Элементы пользовательской очереди:\n";
        foreach (var element in customQueue)
        {
            outputTextBlock.Text += element + "\n";
        }
    }

    // Вставка элемента в стандартную очередь
    public void EnqueueStandard(string element, TextBlock outputTextBlock)
    {
        standardQueue.Enqueue(element);
        outputTextBlock.Text += $"Добавлено в стандартную очередь: {element}\n";
    }

    // Удаление элемента из стандартной очереди
    public void DequeueStandard(TextBlock outputTextBlock)
    {
        if (standardQueue.Count == 0)
        {
            outputTextBlock.Text += "Стандартная очередь пуста. Удаление невозможно.\n";
            return;
        }
        string removedElement = standardQueue.Dequeue();
        outputTextBlock.Text += $"Удалено из стандартной очереди: {removedElement}\n";
    }

    // Просмотр первого элемента стандартной очереди
    public void PeekStandard(TextBlock outputTextBlock)
    {
        if (standardQueue.Count == 0)
        {
            outputTextBlock.Text += "Стандартная очередь пуста.\n";
            return;
        }
        outputTextBlock.Text += $"Первый элемент стандартной очереди: {standardQueue.Peek()}\n";
    }

    // Проверка на пустоту стандартной очереди
    public void IsEmptyStandard(TextBlock outputTextBlock)
    {
        bool isEmpty = standardQueue.Count == 0;
        outputTextBlock.Text += $"Стандартная очередь пуста? {isEmpty}\n";
    }

    // Печать элементов стандартной очереди
    public void PrintStandard(TextBlock outputTextBlock)
    {
        if (standardQueue.Count == 0)
        {
            outputTextBlock.Text += "Стандартная очередь пуста.\n";
            return;
        }
        outputTextBlock.Text += "Элементы стандартной очереди:\n";
        foreach (var element in standardQueue)
        {
            outputTextBlock.Text += element + "\n";
        }
    }

    // Метод для обработки операций из файла
    public void ProcessCommands(string[] commands, TextBlock outputTextBlock)
    {
        foreach (string command in commands)
        {
            if (command.StartsWith("1,"))
            {
                string value = command.Substring(2);
                EnqueueCustom(value, outputTextBlock);
                EnqueueStandard(value, outputTextBlock);
            }
            else if (command == "2")
            {
                DequeueCustom(outputTextBlock);
                DequeueStandard(outputTextBlock);
            }
            else if (command == "3")
            {
                PeekCustom(outputTextBlock);
                PeekStandard(outputTextBlock);
            }
            else if (command == "4")
            {
                IsEmptyCustom(outputTextBlock);
                IsEmptyStandard(outputTextBlock);
            }
            else if (command == "5")
            {
                PrintCustom(outputTextBlock);
                PrintStandard(outputTextBlock);
            }
            else
            {
                outputTextBlock.Text += $"Неизвестная команда: {command}\n";
            }
        }
    }
}
