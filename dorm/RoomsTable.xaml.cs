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
    /// Логика взаимодействия для RoomsTable.xaml
    /// </summary>
    public partial class RoomsTable : Window
    {
        DormContext context1 = new DormContext();
        public RoomsTable()
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
                var room = context.SarcRooms.ToList();
                roomDataGrid.ItemsSource = room;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
                var newRoom = roomDataGrid.SelectedItem as SarcRoom;

            using (var _context = new DormContext())
            {
                try
                {
                    _context.SarcRooms.Add(newRoom);
                    _context.SaveChanges();
                    MessageBox.Show("Комната успешно добавлена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении комнаты: {ex.Message}");
                }
            }

             RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            using (var _context = new DormContext())
            {
                roomDataGrid.ItemsSource = _context.SarcRooms.ToList();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var selectedRoom= roomDataGrid.SelectedItem as SarcRoom;

            if (selectedRoom != null)
            {
                using (var _context = new DormContext())
                {
                    try
                    {
                        var existingRoom = _context.SarcRooms.FirstOrDefault(c => c.Id == selectedRoom.Id);

                        if (existingRoom != null)
                        {
                            existingRoom.NumRoom = selectedRoom.NumRoom;
                            existingRoom.Capacity = selectedRoom.Capacity;
                            existingRoom.IdFloor = selectedRoom.IdFloor;
                            existingRoom.IdCampus = selectedRoom.IdCampus;

                            _context.SaveChanges();
                            MessageBox.Show("Комната успешно изменена!");
                        }
                        else
                        {
                            MessageBox.Show("Комната не найдена в базе данных.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при изменении комнаты: {ex.Message}");
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
            var selectedRoom= roomDataGrid.SelectedItem as SarcRoom;

            if (selectedRoom!= null)
            {
                using (var _context = new DormContext())
                {
                    try
                    {
                        var existingRoom= _context.SarcRooms.FirstOrDefault(c => c.Id == selectedRoom.Id);

                        if (existingRoom!= null)
                        {
                            _context.SarcRooms.Remove(existingRoom);
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
