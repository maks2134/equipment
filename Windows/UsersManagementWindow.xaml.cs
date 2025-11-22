using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Windowing;
using equipment_accounting_system.Models;
using equipment_accounting_system.Services;

namespace equipment_accounting_system.Windows
{
    public sealed partial class UsersManagementWindow : Window
    {
        private readonly FileService _fileService;
        private List<User> _users;

        public UsersManagementWindow(FileService fileService)
        {
            InitializeComponent();
            _fileService = fileService;
            _users = new List<User>();

            this.Activated += UsersManagementWindow_Activated;
        }

        private void UsersManagementWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            try
            {
                var appWindow = AppWindow.GetFromWindowId(this.AppWindow.Id);
                if (appWindow != null)
                {
                    appWindow.Resize(new global::Windows.Graphics.SizeInt32 { Width = 700, Height = 500 });
                }
            }
            catch
            {

            }

            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                _users = _fileService.LoadUsers();

                var displayList = _users.Select(u => new
                {
                    u.Username,
                    RoleText = u.Role == UserRole.Administrator ? "Администратор" : "Пользователь",
                    u.Role,
                    User = u
                }).ToList();

                UsersListView.ItemsSource = displayList;
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке пользователей: {ex.Message}");
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var registerWindow = new RegisterWindow(_fileService);
                registerWindow.Activate();

                registerWindow.Closed += (s, args) =>
                {
                    if (registerWindow.UserRegistered)
                    {
                        LoadUsers();
                    }
                };
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при открытии окна регистрации: {ex.Message}");
            }
        }

        private async void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag is string username)
                {

                    var adminCount = _users.Count(u => u.Role == UserRole.Administrator);
                    var userToDelete = _users.FirstOrDefault(u => u.Username == username);

                    if (userToDelete != null && userToDelete.Role == UserRole.Administrator && adminCount <= 1)
                    {
                        ShowError("Нельзя удалить последнего администратора!");
                        return;
                    }

                    var confirmDialog = new ContentDialog
                    {
                        Title = "Подтверждение",
                        Content = $"Вы уверены, что хотите удалить пользователя '{username}'?",
                        PrimaryButtonText = "Да",
                        CloseButtonText = "Нет",
                        XamlRoot = this.Content?.XamlRoot
                    };

                    if (confirmDialog.XamlRoot == null)
                    {
                        ShowError("Ошибка: окно не готово");
                        return;
                    }

                    var result = await confirmDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        _users.RemoveAll(u => u.Username == username);
                        _fileService.SaveUsers(_users);
                        LoadUsers();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при удалении пользователя: {ex.Message}");
            }
        }

        private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowError(string message)
        {
            try
            {
                var dialog = new ContentDialog
                {
                    Title = "Ошибка",
                    Content = message,
                    CloseButtonText = "OK",
                    XamlRoot = this.Content?.XamlRoot
                };
                _ = dialog.ShowAsync();
            }
            catch
            {

            }
        }
    }
}
