using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Lab_rab_1_1_Husainova_R.Z._BPI_23_02
{
    public partial class MainWindow : Window
    {
        private List<Worker> workers = new List<Worker>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TxtSurname_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

            if (!char.IsLetter(e.Text, 0) && e.Text != " " && !Regex.IsMatch(e.Text, @"[а-яА-ЯёЁa-zA-Z]"))
                e.Handled = true;
        }

        private void TxtSalary_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

            if (!char.IsDigit(e.Text, 0) && e.Text != "." && e.Text != ",")
                e.Handled = true;
        }

        private void TxtSalary_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == System.Windows.Input.Key.Space)
                e.Handled = true;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string surname = TxtSurname.Text?.Trim();
            string position = TxtPosition.Text?.Trim();
            string salaryStr = TxtSalary.Text?.Trim();

            if (string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(position) || string.IsNullOrEmpty(salaryStr))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(salaryStr.Replace(',', '.'), out double salary) || salary <= 0)
            {
                MessageBox.Show("Оклад должен быть положительным числом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            workers.Add(new Worker { Surname = surname, Position = position, Salary = salary });
            UpdateList();
            ClearInputs();
        }

        private void BtnUpp_Click(object sender, RoutedEventArgs e)
        {
            foreach (var worker in workers)
                worker.Salary *= 1.15;

            UpdateList();
            MessageBox.Show("Оклад работникам увеличен на 15%.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnEngineer_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach (var worker in workers)
            {
                if (worker.Surname.StartsWith("Иван", StringComparison.OrdinalIgnoreCase))
                {
                    worker.Position = "Инженер";
                    count++;
                }
            }

            UpdateList();
            MessageBox.Show($"Присвоена должность «Инженер» {count} работник(ам).", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateList()
        {
            LstWorkers.Items.Clear();
            foreach (var w in workers)
                LstWorkers.Items.Add($"{w.Surname} | {w.Position} | {w.Salary:F2} ₽");
        }

        private void ClearInputs()
        {
            TxtSurname.Clear();
            TxtPosition.Clear();
            TxtSalary.Clear();
            TxtSurname.Focus();
        }
    }

    public class Worker
    {
        public string Surname { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
    }
}