using System;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Windowing;
using equipment_accounting_system.Models;
using equipment_accounting_system.Services;

namespace equipment_accounting_system.Windows
{
    public sealed partial class RegisterWindow : Window
    {
        private readonly FileService _fileService;
        public bool UserRegistered { get; private set; } = false;

        public RegisterWindow(FileService fileService)
        {
            InitializeComponent();
            _fileService = fileService;

            this.Activated += RegisterWindow_Activated;

            RoleComboBox.SelectedIndex = 0;
        }

        private void RegisterWindow_Activated(object sender, WindowActivatedEventArgs args)
        {

            try
            {
                var appWindow = AppWindow.GetFromWindowId(this.AppWindow.Id);
                if (appWindow != null)
                {
                    appWindow.Resize(new global::Windows.Graphics.SizeInt32 { Width = 400, Height = 450 });
                }
            }
            catch
            {

            }

            try
            {
                UsernameTextBox?.Focus(FocusState.Programmatic);
            }
            catch
            {

            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ErrorTextBlock.Visibility = Visibility.Collapsed;

                var username = UsernameTextBox.Text.Trim();
                var password = PasswordBox.Password;
                var confirmPassword = ConfirmPasswordBox.Password;
                var selectedRole = RoleComboBox.SelectedItem as ComboBoxItem;

                if (string.IsNullOrWhiteSpace(username))
                {
                    ShowError("Введите имя пользователя");
                    return;
                }

                if (username.Length < 3)
                {
                    ShowError("Имя пользователя должно содержать минимум 3 символа");
                    return;
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    ShowError("Введите пароль");
                    return;
                }

                if (password.Length < 3)
                {
                    ShowError("Пароль должен содержать минимум 3 символа");
                    return;
                }

                if (password != confirmPassword)
                {
                    ShowError("Пароли не совпадают");
                    return;
                }

                if (selectedRole == null)
                {
                    ShowError("Выберите роль");
                    return;
                }

                var existingUsers = _fileService.LoadUsers();
                if (existingUsers.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                {
                    ShowError("Пользователь с таким именем уже существует");
                    return;
                }

                var role = selectedRole.Tag?.ToString() == "Administrator" 
                    ? UserRole.Administrator 
                    : UserRole.User;

                var newUser = new User(username, password, role);
                existingUsers.Add(newUser);
                _fileService.SaveUsers(existingUsers);

                UserRegistered = true;
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при регистрации: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowError(string message)
        {
            try
            {
                if (ErrorTextBlock != null)
                {
                    ErrorTextBlock.Text = message;
                    ErrorTextBlock.Visibility = Visibility.Visible;
                }
            }
            catch
            {

            }
        }
    }
}
