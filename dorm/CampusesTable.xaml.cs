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
using System.Windows.Shapes;

namespace dorm
{
    /// <summary>
    /// Логика взаимодействия для CampusesTable.xaml
    /// </summary>
    public partial class CampusesTable : Window
    {

        DormContext _context = new DormContext();

        public CampusesTable()
        {
            InitializeComponent();
        }
        public void Onload(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            using (var context = new DormContext())
            {
                var campus = context.SarcCampuses.ToList();
                campusDataGrid.ItemsSource = campus;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var newCampus = campusDataGrid.SelectedItem as SarcCampus;

            using (var _context = new DormContext())
            {
                try
                {
                    _context.SarcCampuses.Add(newCampus);
                    _context.SaveChanges();
                    MessageBox.Show("Кампус успешно добавлен!");
                }
                catch (Exception ex)    
                {
                    MessageBox.Show($"Ошибка при добавлении кампуса: {ex.Message}");
                }
            }

            RefreshDataGrid();

        }
        private void RefreshDataGrid()
        {
            using (var _context = new DormContext())
            {
                campusDataGrid.ItemsSource = _context.SarcCampuses.ToList();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var selectedCampus = campusDataGrid.SelectedItem as SarcCampus;

            if (selectedCampus != null)
            {
                using (var _context = new DormContext())
                {
                    try
                    {
                        var existingCampus = _context.SarcCampuses.FirstOrDefault(c => c.Id == selectedCampus.Id);

                        if (existingCampus != null)
                        {
                            existingCampus.Name = selectedCampus.Name;
                            existingCampus.Address = selectedCampus.Address;
                            existingCampus.Description = selectedCampus.Description;
                            existingCampus.FloorsQuantity = selectedCampus.FloorsQuantity;

                            _context.SaveChanges();
                            MessageBox.Show("Кампус успешно изменён!");
                        }
                        else
                        {
                            MessageBox.Show("Кампус не найден в базе данных.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при изменении кампуса: {ex.Message}");
                    }
                }

                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите кампус для изменения.");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selectedCampus = campusDataGrid.SelectedItem as SarcCampus;

            if (selectedCampus != null)
            {
                using (var _context = new DormContext())
                {
                    try
                    {
                        var existingCampus = _context.SarcCampuses.FirstOrDefault(c => c.Id == selectedCampus.Id);

                        if (existingCampus != null)
                        {
                            _context.SarcCampuses.Remove(existingCampus);
                            _context.SaveChanges();
                            MessageBox.Show("Кампус успешно удалён!");
                        }
                        else
                        {
                            MessageBox.Show("Кампус не найден в базе данных.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении кампуса: {ex.Message}");
                    }
                }

                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите кампус для удаления.");
            }

        }
    }
}
