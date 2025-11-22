using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Windowing;
using equipment_accounting_system.Services;
using equipment_accounting_system.Models;

namespace equipment_accounting_system.Windows
{
    public sealed partial class LoginWindow : Window
    {
        private readonly AuthService _authService;
        private readonly FileService _fileService;
        public User? AuthenticatedUser { get; private set; }

        public LoginWindow(AuthService authService, FileService fileService)
        {
            InitializeComponent();
            _authService = authService;
            _fileService = fileService;

            this.Activated += LoginWindow_Activated;
        }

        private void LoginWindow_Activated(object sender, WindowActivatedEventArgs args)
        {

            try
            {
                var appWindow = AppWindow.GetFromWindowId(this.AppWindow.Id);
                if (appWindow != null)
                {
                    appWindow.Resize(new global::Windows.Graphics.SizeInt32 { Width = 400, Height = 300 });
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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text.Trim();
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ShowError("Пожалуйста, введите имя пользователя и пароль");
                return;
            }

            try
            {
                if (_authService.Login(username, password))
                {
                    AuthenticatedUser = _authService.CurrentUser;
                    Close();
                }
                else
                {
                    ShowError("Неверное имя пользователя или пароль");
                    PasswordBox.Password = string.Empty;
                }
            }
            catch (System.Exception ex)
            {
                ShowError($"Ошибка авторизации: {ex.Message}");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var registerWindow = new RegisterWindow(_fileService);
                registerWindow.Activate();

                registerWindow.Closed += (s, args) =>
                {
                    if (registerWindow.UserRegistered)
                    {
                        ShowError("Регистрация успешна! Теперь вы можете войти.");
                    }
                };
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при открытии окна регистрации: {ex.Message}");
            }
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
