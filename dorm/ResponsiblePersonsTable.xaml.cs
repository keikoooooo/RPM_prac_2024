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
    /// Логика взаимодействия для ResponsiblePersonsTable.xaml
    /// </summary>
    public partial class ResponsiblePersonsTable : Window
    {
        public ResponsiblePersonsTable()
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
                var responsiblePerson = context.ResponsiblePersons.ToList();
                responsiblePersonsDataGrid.ItemsSource = responsiblePerson;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var newResponsiblePerson = responsiblePersonsDataGrid.SelectedItem as ResponsiblePerson;

            using (var _context = new DormContext())
            {
                try
                {
                    _context.ResponsiblePersons.Add(newResponsiblePerson);
                    _context.SaveChanges();
                    MessageBox.Show(" Ответсвенное лицо было добавлено!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении лица: {ex.Message}");
                }
            }

            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            using (var _context = new DormContext())
            {
                responsiblePersonsDataGrid.ItemsSource = _context.ResponsiblePersons.ToList();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var selectedResponsiblePerson = responsiblePersonsDataGrid.SelectedItem as ResponsiblePerson;

            if (selectedResponsiblePerson!= null)
            {
                using (var _context = new DormContext())
                {
                    try
                    {
                        var existingResponsiblePerson= _context.ResponsiblePersons.FirstOrDefault(c => c.Id == selectedResponsiblePerson.Id);

                        if (existingResponsiblePerson!= null)
                        {
                            existingResponsiblePerson.FirstName = selectedResponsiblePerson.FirstName;
                            existingResponsiblePerson.LastName= selectedResponsiblePerson.LastName;
                            existingResponsiblePerson.Patronymic= selectedResponsiblePerson.Patronymic;
                            existingResponsiblePerson.IdPost = selectedResponsiblePerson.IdPost;
                            existingResponsiblePerson.IdCampus= selectedResponsiblePerson.IdCampus;



                            _context.SaveChanges();
                            MessageBox.Show("Данные о лице успешно изменены!");
                        }
                        else
                        {
                            MessageBox.Show("Ответственное лицо не найдено в базе данных.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при изменении данных: {ex.Message}");
                    }
                }
                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите лицо для изменения.");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selectedResponsiblePerson = responsiblePersonsDataGrid.SelectedItem as ResponsiblePerson;

            if (selectedResponsiblePerson != null)
            {
                using (var _context = new DormContext())
                {
                    try
                    {
                        var existingResponsiblePerson= _context.ResponsiblePersons.FirstOrDefault(c => c.Id == selectedResponsiblePerson.Id);

                        if (existingResponsiblePerson!= null)
                        {
                            _context.ResponsiblePersons.Remove(existingResponsiblePerson);
                            _context.SaveChanges();
                            MessageBox.Show("Лицо успешно удалена!");
                        }
                        else
                        {
                            MessageBox.Show("Данное лицо не найдено в базе данных.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении лица: {ex.Message}");
                    }
                }

                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите лицо для удаления.");
            }
        }
    }
}

