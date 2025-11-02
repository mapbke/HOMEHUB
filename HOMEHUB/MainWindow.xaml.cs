using System;
using System.Windows;
using System.Windows.Controls;

namespace HOMEHUB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInput(Ingredient1Weight.Text, "Вес первого ингредиента") ||
                    !ValidateInput(Ingredient1Calories.Text, "Калорийность первого ингредиента"))
                    return;

                double weight1 = double.Parse(Ingredient1Weight.Text);
                double calories1 = double.Parse(Ingredient1Calories.Text);

                if (!ValidateInput(Ingredient2Weight.Text, "Вес второго ингредиента") ||
                    !ValidateInput(Ingredient2Calories.Text, "Калорийность второго ингредиента"))
                    return;

                double weight2 = double.Parse(Ingredient2Weight.Text);
                double calories2 = double.Parse(Ingredient2Calories.Text);

                if (!ValidateInput(Ingredient3Weight.Text, "Вес третьего ингредиента") ||
                    !ValidateInput(Ingredient3Calories.Text, "Калорийность третьего ингредиента"))
                    return;

                double weight3 = double.Parse(Ingredient3Weight.Text);
                double calories3 = double.Parse(Ingredient3Calories.Text);

                double totalWeight = CalculateTotalWeight(weight1, weight2, weight3);
                double totalCalories = CalculateTotalCalories(weight1, calories1, weight2, calories2, weight3, calories3);
                double caloriesPer100g = CalculateCaloriesPer100g(totalCalories, totalWeight);

                UpdateResults(totalWeight, totalCalories, caloriesPer100g);
                ShowSuccessMessage(totalWeight, totalCalories, caloriesPer100g);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Ingredient1Weight.Text = "";
            Ingredient1Calories.Text = "";
            Ingredient2Weight.Text = "";
            Ingredient2Calories.Text = "";
            Ingredient3Weight.Text = "";
            Ingredient3Calories.Text = "";

            ResetResults();

            Ingredient1Weight.Focus();
        }

        private bool ValidateInput(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show($"Поле '{fieldName}' не может быть пустым.", "Внимание",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!double.TryParse(input, out double result))
            {
                MessageBox.Show($"Поле '{fieldName}' должно содержать числовое значение.", "Ошибка ввода",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (result < 0)
            {
                MessageBox.Show($"Поле '{fieldName}' не может быть отрицательным.", "Ошибка ввода",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private double CalculateTotalWeight(double w1, double w2, double w3)
        {
            return w1 + w2 + w3;
        }

        private double CalculateTotalCalories(double w1, double c1, double w2, double c2, double w3, double c3)
        {
            double calories1 = (w1 * c1) / 100.0;
            double calories2 = (w2 * c2) / 100.0;
            double calories3 = (w3 * c3) / 100.0;

            return calories1 + calories2 + calories3;
        }

        private double CalculateCaloriesPer100g(double totalCalories, double totalWeight)
        {
            if (totalWeight == 0) return 0;
            return (totalCalories / totalWeight) * 100;
        }

        private void UpdateResults(double totalWeight, double totalCalories, double caloriesPer100g)
        {
            TotalWeightResult.Text = $"{totalWeight:F1} г";
            TotalCaloriesResult.Text = $"{totalCalories:F1} ккал";
            CaloriesPer100gResult.Text = $"{caloriesPer100g:F1} ккал/100г";
        }

        private void ResetResults()
        {
            TotalWeightResult.Text = "0 г";
            TotalCaloriesResult.Text = "0 ккал";
            CaloriesPer100gResult.Text = "0 ккал/100г";
        }

        private void ShowSuccessMessage(double totalWeight, double totalCalories, double caloriesPer100g)
        {
            MessageBox.Show(
                $"✅ Расчет завершен!\n\n" +
                $"🍽️ Общий вес: {totalWeight:F1} г\n" +
                $"🔥 Общая калорийность: {totalCalories:F1} ккал\n" +
                $"📊 Калорийность на 100г: {caloriesPer100g:F1} ккал",
                "Расчет завершен",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }
    }
}