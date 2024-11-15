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
    /// Логика взаимодействия для FloorsTable.xaml
    /// </summary>
    public partial class FloorsTable : Window
    {
        public FloorsTable()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        public void Onload(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            using (var context = new DormContext())
            {
                var floor = context.Floors.ToList();
                floorDataGrid.ItemsSource = floor;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var newFloor = floorDataGrid.SelectedItem as Floor;

            using (var _context = new DormContext())
            {
                try
                {
                    _context.Floors.Add(newFloor);
                    _context.SaveChanges();
                    MessageBox.Show("Этаж успешно добавлен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении этажа: {ex.Message}");
                }
            }

            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            using (var _context = new DormContext())
            {
                floorDataGrid.ItemsSource = _context.Floors.ToList();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var selectedFloor = floorDataGrid.SelectedItem as Floor;

            if (selectedFloor != null)
            {
                using (var _context = new DormContext())
                {
                    try
                    {
                        var existingFloor= _context.Floors.FirstOrDefault(c => c.Id == selectedFloor.Id);

                        if (existingFloor!= null)
                        {
                            existingFloor.FloorNumber= selectedFloor.FloorNumber;
                            existingFloor.IdCampus = selectedFloor.IdCampus;

                            _context.SaveChanges();
                            MessageBox.Show("Этаж успешно изменен!");
                        }
                        else
                        {
                            MessageBox.Show("этаж не найден в базе данных.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при изменении этажа: {ex.Message}");
                    }
                }
                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите этаж для изменения.");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selectedFloor = floorDataGrid.SelectedItem as Floor;

            if (selectedFloor != null)
            {
                using (var _context = new DormContext())
                {
                    try
                    {
                        var existingFloor = _context.SarcRooms.FirstOrDefault(c => c.Id == selectedFloor.Id);

                        if (existingFloor!= null)
                        {
                            _context.SarcRooms.Remove(existingFloor);
                            _context.SaveChanges();
                            MessageBox.Show("Комната успешно удалена!");
                        }
                        else
                        {
                            MessageBox.Show("Комната не найдена в базе данных.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении комнаты: {ex.Message}");
                    }
                }

                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите комнату для удаления.");
            }
        }
    }
    }


