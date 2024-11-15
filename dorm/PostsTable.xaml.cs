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
    /// Логика взаимодействия для PostsTable.xaml
    /// </summary>
    public partial class PostsTable : Window
    {
        public PostsTable()
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
                var post= context.Posts.ToList();
                postDataGrid.ItemsSource = post;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var newPost= postDataGrid.SelectedItem as Post;

            using (var _context = new DormContext())
            {
                try
                {
                    _context.Posts.Add(newPost);
                    _context.SaveChanges();
                    MessageBox.Show(" Новая должность была добавлена!");
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
                postDataGrid.ItemsSource = _context.Posts.ToList();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
   
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
           
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var selectedPost = postDataGrid.SelectedItem as Post;

            if (selectedPost != null)
            {
                using (var _context = new DormContext())
                {
                    try
                   {
                        var existingPost = _context.Posts.FirstOrDefault(c => c.Id == selectedPost.Id);

                        if (existingPost!= null)
                        {
                            existingPost.PostName = selectedPost.PostName;
                            _context.SaveChanges();
                            MessageBox.Show("Данные о должности успешно изменены!");
                        }
                        else
                        {
                            MessageBox.Show("Должности не найдено в базе данных.");
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
                MessageBox.Show("Выберите должность для изменения.");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selectedPost= postDataGrid.SelectedItem as Post;

            if (selectedPost!= null)
            {
                using (var _context = new DormContext())
                {
                    try
                    {
                        var existingPost= _context.SarcRooms.FirstOrDefault(c => c.Id == selectedPost.Id);

                        if (existingPost != null)
                        {
                            _context.Posts.Remove(selectedPost);
                            _context.SaveChanges();
                            MessageBox.Show("Должность успешно удалена!");
                        }
                        else
                        {
                            MessageBox.Show("Данная должность не найденв в базе данных.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении должности: {ex.Message}");
                    }
                }

                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите должность для удаления.");
            }
        }
    }
}
