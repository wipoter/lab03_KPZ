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
using System.Xml.Linq;

namespace DB_First
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StudentAccountEntities studentAccount;
        public MainWindow()
        {
            InitializeComponent();
            studentAccount = new StudentAccountEntities();
            AcTab.ItemsSource = studentAccount.Account.ToList();
            StTab.ItemsSource = studentAccount.Student.ToList();
            AchTab.ItemsSource = studentAccount.Achievement.ToList();
        }

        private void Student_Open(object sender, RoutedEventArgs e)
        {
            AchievementPage.Visibility = Visibility.Hidden;
            AccountPage.Visibility = Visibility.Hidden;
            StudentPage.Visibility = Visibility.Visible;
        }

        private void Account_Open(object sender, RoutedEventArgs e)
        {
            AchievementPage.Visibility = Visibility.Hidden;
            StudentPage.Visibility = Visibility.Hidden;
            AccountPage.Visibility = Visibility.Visible;
        }

        private void Achievement_Open(object sender, RoutedEventArgs e)
        {
            StudentPage.Visibility = Visibility.Hidden;
            AccountPage.Visibility = Visibility.Hidden;
            AchievementPage.Visibility = Visibility.Visible;
        }

        #region Account Page
        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            studentAccount.Account.Add(new Account() { Loggin = Login.Text, Password = Password.Password });
            Login.Text = "";
            Password.Password = "";
            studentAccount.SaveChanges();
            AcTab.ItemsSource = studentAccount.Account.ToList();
        }

        private void DeleteAccount_Button(object sender, RoutedEventArgs e)
        {
            if (AcTab.SelectedItem is Account)
            {
                studentAccount.Account.Remove((Account)AcTab.SelectedItem);
                studentAccount.SaveChanges();
                AcTab.ItemsSource = studentAccount.Account.ToList();
            }
        }


        private void ChangeAccount_Button(object sender, RoutedEventArgs e)
        {
            if (AcTab.SelectedItem is Account)
            {
                var item = AcTab.SelectedItem as Account;
                item.Loggin = Login.Text.Trim();
                item.Password = Password.Password.Trim();
                studentAccount.SaveChanges();
                AcTab.ItemsSource = studentAccount.Account.ToList();
            }
        }

        private void AcTab_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (AcTab.SelectedItem is Account)
            {
                var item = AcTab.SelectedItem as Account;
                Login.Text = item.Loggin;
                Password.Password = item.Password;
            }
        }

        #endregion

        #region Student Page
        private void DeleteStudent_Button(object sender, RoutedEventArgs e)
        {
            if (StTab.SelectedItem is Student)
            {
                studentAccount.Student.Remove((Student)StTab.SelectedItem);
                studentAccount.SaveChanges();
                StTab.ItemsSource = studentAccount.Student.ToList();
            }
        }

        private void AddStudent_Button(object sender, RoutedEventArgs e)
        {
            studentAccount.Student.Add(new Student()
            {
                Name = Name.Text,
                Surname = Surname.Text,
                Age = int.Parse(Age.Text),
                Institute = Institute.Text,
                Account_Id = int.Parse(Id_Acc.Text),
                Group = Group.Text,
                Num = int.Parse(Number.Text)
            });
            studentAccount.SaveChanges();
            StTab.ItemsSource = studentAccount.Student.ToList();
            Name.Text = "";
            Surname.Text = "";
            Age.Text = "";
            Institute.Text = "";
            Id_Acc.Text = "";
            Group.Text = "";
            Number.Text = "";
        }

        private void ChangeStudent_Button(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(Id_Acc.Text);
            var item = StTab.SelectedItem as Student;
            item.Name = Name.Text;
            item.Surname = Surname.Text;
            item.Age = int.Parse(Age.Text);
            item.Institute = Institute.Text;
            item.Group = Group.Text;
            item.Num = int.Parse(Number.Text);
            item.Account_Id = id;
            item.Account = studentAccount.Account.Where(a => a.Id == id).FirstOrDefault();
            studentAccount.SaveChanges();
            StTab.ItemsSource = studentAccount.Student.ToList();
        }

        private void StTab_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (StTab.SelectedItem is Student)
            {
                var item = StTab.SelectedItem as Student;
                Name.Text = item.Name;
                Surname.Text = item.Surname;
                Age.Text = item.Age.ToString();
                Institute.Text = item.Institute;
                Group.Text = item.Group;
                Number.Text = item.Num.ToString();
                Id_Acc.Text = item.Account_Id.ToString();
            }
        }

        #endregion

        #region Achievement Page
        private void DeleteAchievement_Button(object sender, RoutedEventArgs e)
        {
            if (AchTab.SelectedItem is Achievement)
            {
                studentAccount.Achievement.Remove((Achievement)AchTab.SelectedItem);
                studentAccount.SaveChanges();
                AchTab.ItemsSource = studentAccount.Achievement.ToList();
            }
        }

        private void AddAchievement_Button(object sender, RoutedEventArgs e)
        {
            studentAccount.Achievement.Add(new Achievement()
            {
                Name = Name.Text,
                Describtion = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd).Text,
                Student_Id = int.Parse(Id_Stud.Text)
            });
            Name.Text = "";
            Description = new RichTextBox();
            studentAccount.SaveChanges();
            Id_Stud.Text = "";
            AchTab.ItemsSource = studentAccount.Achievement.ToList();
        }

        private void ChangeAchievement_Button(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(Id_Stud.Text);
            var item = AchTab.SelectedItem as Achievement;
            item.Name = NameA.Text;
            item.Describtion = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd).Text;
            item.Student = studentAccount.Student.Where(a => a.Id == id).FirstOrDefault();
            studentAccount.SaveChanges();
            AchTab.ItemsSource = studentAccount.Achievement.ToList();
        }

        private void AchTab_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (AchTab.SelectedItem is Achievement)
            {
                var item = AchTab.SelectedItem as Achievement;

                FlowDocument mcFlowDoc = new FlowDocument();
                Paragraph para = new Paragraph();
                para.Inlines.Add(item.Describtion);
                mcFlowDoc.Blocks.Add(para);

                NameA.Text = item.Name;
                Description.Document = mcFlowDoc;
                Id_Stud.Text = item.Student_Id.ToString();
            }
        }

        #endregion
    }
}
