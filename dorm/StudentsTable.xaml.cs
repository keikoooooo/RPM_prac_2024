    using Microsoft.EntityFrameworkCore;
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
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
using iText;
    using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Reflection.Metadata;
using iText.Kernel.Pdf;

namespace dorm
    {
        /// <summary>
        /// Логика взаимодействия для StudentsTable.xaml
        /// </summary>
        public partial class StudentsTable : Window
        {
            DormContext _context = new DormContext();

            public StudentsTable()
            {
            InitializeComponent();
            }
            

            public void Onload(object sender, RoutedEventArgs e)
            {
                LoadData();
            }
            private void studentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {


            }
            private void LoadData()
            {
                using (var context = new DormContext())
                {
                    var students = context.SarcStudents.ToList();
                    studentDataGrid.ItemsSource = students;
                }
            }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            LoadData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, object e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//добавить
        {
            var student = studentDataGrid.SelectedItem as SarcStudent;

            if (student != null)
            {
                using (var _context = new DormContext())
                {
                    try
                    {
                        // Проверяем, добавляется ли новая строка
                        if (!_context.SarcStudents.Any(s => s.Id == student.Id))
                        {
                            _context.SarcStudents.Add(student);
                            _context.SaveChanges();
                            MessageBox.Show("Студент успешно добавлен!");
                        }
                        else
                        {
                            MessageBox.Show("Студент с таким ID уже существует.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при добавлении студента: {ex.Message}");
                    }
                }

                // Обновляем DataGrid
                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите строку для добавления.");
            }

        }

        private void Button_Click_3(object sender, object e)
        {

        }
        private void studentDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            
        }

        private void RefreshDataGrid()
        {
            using (var _context = new DormContext())
            {
                studentDataGrid.ItemsSource = _context.SarcStudents.ToList();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var student = studentDataGrid.SelectedItem as SarcStudent;

            if (student != null)
            {
                using (var _context = new DormContext())
                {
                    try
                    {
                        var existingStudent = _context.SarcStudents.FirstOrDefault(s => s.Id == student.Id);

                        if (existingStudent != null)
                        {
                            existingStudent.StudLogin = student.StudLogin;
                            existingStudent.StudPassword = student.StudPassword;
                            existingStudent.FullName = student.FullName;
                            existingStudent.NumContract = student.NumContract;
                            existingStudent.NumCampus = student.NumCampus;
                            existingStudent.NumGroup = student.NumGroup;
                            existingStudent.NumRoom = student.NumRoom;
                            existingStudent.Specialization = student.Specialization;
                            existingStudent.StudentCard = student.StudentCard;
                            existingStudent.Grade = student.Grade;
                            existingStudent.EarnedPoints = student.EarnedPoints;

                            _context.SaveChanges();
                            MessageBox.Show("Запись успешно обновлена!");
                        }
                        else
                        {
                            MessageBox.Show("Студент не найден в базе данных.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при обновлении студента: {ex.Message}");
                    }
                }

                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите запись для обновления.");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var student = studentDataGrid.SelectedItem as SarcStudent;

            if (student != null)
            {
                using (var _context = new DormContext())
                {
                    try
                    {
                        var existingStudent = _context.SarcStudents.FirstOrDefault(s => s.Id == student.Id);

                        if (existingStudent != null)
                        {
                            _context.SarcStudents.Remove(existingStudent);
                            _context.SaveChanges();
                            MessageBox.Show("Запись успешно удалена!");
                        }
                        else
                        {
                            MessageBox.Show("Студент не найден в базе данных.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении студента: {ex.Message}");
                    }
                }

                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void ExportStudents_Click(object sender, RoutedEventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Введите ID общежития:", "ID общежития", "1");
            if (int.TryParse(input, out int campusId))
            {
                try
                {
                    using (var context = new DormContext())
                    {
                        var students = context.SarcStudents
                            .Where(s => s.NumCampus == campusId)
                            .Select(s => new SarcStudent
                            ((
                                s.FullName,
                                s.NumContract,
                                s.Specialization,
                                s.Grade,
                                s.EarnedPoints,
                                s.StudentCard,
                                s.NumRoomNavigation,
                                s.NumCampus
                            ))
                            .ToList();

                        if (!students.Any())
                        {
                            MessageBox.Show("Студенты не найдены.");
                            return;
                        }

                        SavePdf(students, campusId);
                        MessageBox.Show("PDF успешно создан!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Некорректный ввод ID.");
            }
        }

        private void SavePdf(System.Collections.Generic.List<var> students, int campusId)
        {
            string fileName = $"Students_Campus_{campusId}.pdf";
            using (var pdfWriter = new PdfWriter(fileName))
            {
                var pdfDoc = new PdfDocument(pdfWriter);
                Document document = new Document(pdfDoc);

                document.Add(new Paragraph($"Студенты общежития #{campusId}").SetBold().SetFontSize(16));

                foreach (var student in students)
                {
                    document.Add(new Paragraph($"Имя: {student.FullName}"));
                    document.Add(new Paragraph($"Контракт: {student.NumContract}"));
                    document.Add(new Paragraph($"Специальность: {student.Specialization}"));
                    document.Add(new Paragraph($"Курс: {student.Grade}"));
                    document.Add(new Paragraph("-----------------------------"));
                }

                document.Close();
            }
        }

    }
}
