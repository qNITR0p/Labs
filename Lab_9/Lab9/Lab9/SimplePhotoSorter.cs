using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lab9
{
    public partial class SimplePhotoSorter : Form
    {
        FolderBrowserDialog folderBrowserDialog;
        public SimplePhotoSorter()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
            folderBrowserDialog = new FolderBrowserDialog();
        }

        private string selectedPath;
        private string targetPath;
        private string sortingPath;

        private void LogAction(string action)
        {
            // Создание файла журнала
            using (StreamWriter logFile = new StreamWriter("log.txt", true))
            {
                // Запись в журнал
                logFile.WriteLine($"{DateTime.Now}: {action}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = folderBrowserDialog.SelectedPath;

                    // Заполнение TreeView с каталогом в формате дерева
                    TreeNode rootNode = treeView1.Nodes.Add(selectedPath, Path.GetFileName(selectedPath));
                    PopulateTreeView(selectedPath, rootNode);

                    // Заполнение ListView
                    AddFiles(selectedPath, listView1);
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateTreeView(string path, TreeNode node)
        {
            try
            {
                foreach (var directory in Directory.GetDirectories(path))
                {
                    TreeNode childNode = node.Nodes.Add(directory, Path.GetFileName(directory));
                    PopulateTreeView(directory, childNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddFiles(string path, ListView listView)
        {
            try
            {
                foreach (var item in new DirectoryInfo(path).GetFileSystemInfos())
                {
                    if (item is DirectoryInfo)
                    {
                        AddFiles(item.FullName, listView);
                    }
                    else
                    {
                        listView.Items.Add(item.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CopyFiles(string sourcePath, string destinationPath)
        {
            try
            {
                // Получение списка файлов в исходном каталоге и подкаталогах
                var files = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);

                // Проверка, что в исходном каталоге есть файлы
                if (files.Length == 0)
                {
                    throw new Exception($"В исходном каталоге нет файлов: {sourcePath}");
                }

                // Копирование всех файлов в новую папку
                foreach (var file in files)
                {
                    // Создание пути для файла в целевой папке
                    string destinationFilePath = Path.Combine(destinationPath, Path.GetFileName(file));

                    // Если файл уже существует в целевой папке, пропустите его
                    if (File.Exists(destinationFilePath))
                    {
                        Console.WriteLine($"Файл уже существует: {destinationFilePath}");
                        continue;
                    }

                    // Копирование файла
                    File.Copy(file, destinationFilePath);
                    Console.WriteLine($"Копирование файла: {file}");
                    LogAction($"Копирование файла: {file}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.ToString()}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogAction($"Произошла ошибка: {ex.ToString()}");
            }
        }

        private void searchAndDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Открытие диалогового окна выбора каталога
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Обновление targetPath новым выбранным путем
                    targetPath = folderBrowserDialog.SelectedPath;

                    // Создание новой папки в выбранном каталоге
                    string newFolderPath = Path.Combine(targetPath, "Результат выполнения программы");

                    // Удаление старой папки, если она существует
                    if (Directory.Exists(newFolderPath))
                    {
                        Directory.Delete(newFolderPath, true);
                        LogAction($"Удалена старая папка: {newFolderPath}");
                    }

                    // Создание новой папки
                    Directory.CreateDirectory(newFolderPath);
                    LogAction($"Создана новая папка: {newFolderPath}");

                    // Копирование всех файлов в новую папку
                    CopyFiles(selectedPath, newFolderPath);

                    // Поиск и удаление дубликатов
                    var files = Directory.GetFiles(newFolderPath, "*.*", SearchOption.AllDirectories);
                    var duplicates = files.Select(x => new FileInfo(x))
                                  .GroupBy(x => new { x.Name, x.Length })
                                  .Where(g => g.Count() > 1)
                                  .SelectMany(g => g.Skip(1));

                    foreach (var file in duplicates)
                    {
                        File.Delete(file.FullName);
                        LogAction($"Удален дубликат файла: {file.FullName}");
                    }

                    // Показать сообщение после выполнения всех операций
                    MessageBox.Show("Работа программы завершена успешно.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogAction("Работа программы завершена успешно.");
                }
                else
                {
                    MessageBox.Show("Вы не выбрали каталог. Пожалуйста, выберите каталог и попробуйте снова.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogAction($"Произошла ошибка: {ex.Message}");
            }
        }

        private void SortFiles(string sourcePath, string destinationPath, string period)
        {
            // Получение списка файлов в исходном каталоге и подкаталогах
            var files = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);

            // Проверка, что в исходном каталоге есть файлы
            if (files.Length == 0)
            {
                throw new Exception($"В исходном каталоге нет файлов: {sourcePath}");
            }

            // Сортировка файлов по дате создания
            var sortedFiles = files.OrderBy(f => File.GetCreationTime(f));

            // Перемещение всех файлов в новую папку, создавая папки по периодам
            foreach (var file in sortedFiles)
            {
                // Получение даты создания файла
                var creationTime = File.GetCreationTime(file);

                // Создание имени папки на основе периода
                string folderName;
                switch (period)
                {
                    case "День":
                        folderName = creationTime.ToString("yyyy-MM-dd");
                        break;
                    case "Неделя":
                        folderName = creationTime.ToString("yyyy-MM-dd");
                        break;
                    case "Месяц":
                        folderName = creationTime.ToString("yyyy-MM");
                        break;
                    default:
                        throw new Exception($"Неизвестный период: {period}");
                }

                // Создание пути для папки в целевом каталоге
                string destinationFolderPath = Path.Combine(destinationPath, folderName);

                // Создание папки, если она еще не существует
                if (!Directory.Exists(destinationFolderPath))
                {
                    Directory.CreateDirectory(destinationFolderPath);
                    LogAction($"Создана новая папка: {destinationFolderPath}");
                }

                // Создание пути для файла в целевой папке
                string destinationFilePath = Path.Combine(destinationFolderPath, Path.GetFileName(file));

                // Перемещение файла
                File.Move(file, destinationFilePath);
                Console.WriteLine($"Перемещение файла: {file}");
                LogAction($"Перемещено файло: {file}");
            }
        }

        private void DeleteEmptyFolders(string path)
        {
            foreach (var directory in Directory.GetDirectories(path))
            {
                DeleteEmptyFolders(directory);

                if (!Directory.EnumerateFileSystemEntries(directory).Any())
                {
                    Directory.Delete(directory, false);
                    LogAction($"Удалена пустая папка: {directory}");
                }
            }
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Открытие диалогового окна выбора каталога
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Обновление sortingPath новым выбранным путем
                    sortingPath = folderBrowserDialog.SelectedPath;

                    // Открытие формы выбора параметров сортировки
                    SortingForm sortParametersForm = new SortingForm();
                    if (sortParametersForm.ShowDialog() == DialogResult.OK)
                    {
                        // Получение выбранного периода от пользователя
                        string period = sortParametersForm.Period;

                        // Вызов функции SortFiles с исходным путем, путем назначения и выбранным периодом
                        SortFiles(sortingPath, sortingPath, period);
                        LogAction($"Сортировка файлов завершена успешно с периодом: {period}");

                        // Удаление пустых папок после сортировки
                        DeleteEmptyFolders(sortingPath);
                        LogAction($"Удалены пустые папки после сортировки");

                        // Показать сообщение после выполнения всех операций
                        MessageBox.Show("Сортировка файлов завершена успешно.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogAction("Сортировка файлов завершена успешно.");
                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали каталог. Пожалуйста, выберите каталог и попробуйте снова.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogAction($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}

