using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Windowing;
using equipment_accounting_system.Models;
using equipment_accounting_system.Services;
using equipment_accounting_system.Windows;
using System.Reflection;

namespace equipment_accounting_system
{
    public sealed partial class MainWindow : Window
    {
        private readonly EquipmentManager _equipmentManager;
        private readonly FileService _fileService;
        private readonly AuthService _authService;
        private List<Equipment> _currentEquipmentList;

        public MainWindow(EquipmentManager equipmentManager, FileService fileService, AuthService authService)
        {
            InitializeComponent();
            _equipmentManager = equipmentManager;
            _fileService = fileService;
            _authService = authService;

            _currentEquipmentList = new List<Equipment>();

            this.Activated += MainWindow_Activated;
            this.Closed += MainWindow_Closed;

            InitializeWindow();
        }

        private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            try
            {
                var appWindow = AppWindow.GetFromWindowId(this.AppWindow.Id);
                if (appWindow != null)
                {
                    appWindow.Resize(new global::Windows.Graphics.SizeInt32 { Width = 1200, Height = 800 });
                }
            }
            catch
            {

            }
        }

        private void InitializeWindow()
        {
            DispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
            {
                LoadEquipment();
                UpdateUserInfo();
                UpdateButtonsState();
            });
        }

        private void LoadEquipment()
        {
            try
            {
                if (_fileService == null || _equipmentManager == null)
                {
                    UpdateStatus("Ошибка: сервисы не инициализированы");
                    return;
                }

                var equipmentList = _fileService.LoadEquipment();
                _equipmentManager.SetEquipmentList(equipmentList);
                RefreshEquipmentList();
                UpdateStatus($"Данные загружены успешно. Записей: {equipmentList.Count}");
            }
            catch (Exception ex)
            {
                UpdateStatus($"Ошибка при загрузке данных: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LoadEquipment error: {ex}");
            }
        }

        private void SaveEquipment()
        {
            try
            {
                if (_equipmentManager == null || _fileService == null)
                {
                    System.Diagnostics.Debug.WriteLine("SaveEquipment: сервисы не инициализированы");
                    return;
                }

                var equipmentList = _equipmentManager.GetAllEquipment();
                _fileService.SaveEquipment(equipmentList);
                UpdateStatus($"Данные сохранены успешно. Записей: {equipmentList.Count}");
                System.Diagnostics.Debug.WriteLine($"SaveEquipment: сохранено {equipmentList.Count} записей");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Ошибка при сохранении данных: {ex.Message}";
                UpdateStatus(errorMsg);
                System.Diagnostics.Debug.WriteLine($"SaveEquipment error: {ex}");
                ShowError(errorMsg);
            }
        }

        private void RefreshEquipmentList()
        {
            _currentEquipmentList = _equipmentManager.GetAllEquipment();
            UpdateDataGrid(_currentEquipmentList);
            UpdateCount();
        }

        private void UpdateDataGrid(List<Equipment> equipmentList)
        {
            try
            {
                if (EquipmentListView == null) return;

                var displayList = equipmentList.Select(e => new
                {
                    e.Id,
                    e.Name,
                    EquipmentType = e.GetEquipmentType(),
                    e.Manufacturer,
                    e.Price,
                    PriceFormatted = e.Price.ToString("C"),
                    e.PurchaseDate,
                    PurchaseDateFormatted = e.PurchaseDate.ToString("dd.MM.yyyy"),
                    e.Location,
                    e.Status,
                    Equipment = e
                }).ToList();

                EquipmentListView.ItemsSource = displayList;
            }
            catch (Exception ex)
            {
                UpdateStatus($"Ошибка при обновлении списка: {ex.Message}");
            }
        }

        private void UpdateCount()
        {
            try
            {
                if (CountTextBlock != null && _equipmentManager != null)
                {
                    CountTextBlock.Text = $"Всего: {_equipmentManager.GetCount()}";
                }
            }
            catch
            {

            }
        }

        private void UpdateUserInfo()
        {
            try
            {
                if (_authService?.CurrentUser != null && UserInfoTextBlock != null)
                {
                    var roleText = _authService.IsAdministrator ? "Администратор" : "Пользователь";
                    UserInfoTextBlock.Text = $"Пользователь: {_authService.CurrentUser.Username} ({roleText})";
                }
            }
            catch
            {

            }
        }

        private void UpdateButtonsState()
        {
            try
            {
                if (_authService == null) return;

                var isAdmin = _authService.IsAdministrator;
                var visibility = isAdmin ? Microsoft.UI.Xaml.Visibility.Visible : Microsoft.UI.Xaml.Visibility.Collapsed;
                var hasSelection = EquipmentListView?.SelectedItem != null;

                if (AddButton != null)
                {
                    AddButton.Visibility = visibility;
                    AddButton.IsEnabled = isAdmin;
                }
                if (EditButton != null)
                {
                    EditButton.Visibility = visibility;
                    EditButton.IsEnabled = isAdmin && hasSelection;
                }
                if (DeleteButton != null)
                {
                    DeleteButton.Visibility = visibility;
                    DeleteButton.IsEnabled = isAdmin && hasSelection;
                }
                if (UsersManagementButton != null)
                {
                    UsersManagementButton.Visibility = visibility;
                    UsersManagementButton.IsEnabled = isAdmin;
                }

                if (ViewButton != null)
                {
                    ViewButton.IsEnabled = hasSelection;
                }

                if (AdminSeparator1 != null)
                {
                    AdminSeparator1.Visibility = visibility;
                }
                if (AdminSeparator2 != null)
                {
                    AdminSeparator2.Visibility = visibility;
                }
            }
            catch
            {

            }
        }

        private void UpdateStatus(string message)
        {
            try
            {
                if (StatusTextBlock != null)
                {
                    StatusTextBlock.Text = $"{DateTime.Now:HH:mm:ss} - {message}";
                }
            }
            catch
            {

            }
        }

        private async void ShowError(string message)
        {
            try
            {
                UpdateStatus($"ОШИБКА: {message}");

                if (this.Content?.XamlRoot == null)
                {
                    await Task.Delay(100);
                }

                var dialog = new ContentDialog
                {
                    Title = "Ошибка",
                    Content = message,
                    CloseButtonText = "OK",
                    XamlRoot = this.Content?.XamlRoot
                };

                if (dialog.XamlRoot != null)
                {
                    await dialog.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ShowError exception: {ex}");

            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new EquipmentDialog();

                dialog.XamlRoot = this.Content?.XamlRoot;

                if (dialog.XamlRoot == null)
                {
                    UpdateStatus("Ошибка: окно не готово");
                    return;
                }

                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary && dialog.ResultEquipment != null)
                {
                    _equipmentManager.AddEquipment(dialog.ResultEquipment);
                    RefreshEquipmentList();
                    SaveEquipment();
                    UpdateStatus("Оборудование добавлено");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при добавлении: {ex.Message}");
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EquipmentListView.SelectedItem == null) return;

                var selectedItem = EquipmentListView.SelectedItem;
                var equipmentProperty = selectedItem.GetType().GetProperty("Equipment");
                var equipment = equipmentProperty?.GetValue(selectedItem) as Equipment;

                if (equipment == null) return;

                var dialog = new EquipmentDialog(equipment);

                dialog.XamlRoot = this.Content?.XamlRoot;

                if (dialog.XamlRoot == null)
                {
                    UpdateStatus("Ошибка: окно не готово");
                    return;
                }

                var dialogResult = await dialog.ShowAsync();
                if (dialogResult == ContentDialogResult.Primary && dialog.ResultEquipment != null)
                {
                    dialog.ResultEquipment.Id = equipment.Id;
                    if (_equipmentManager.UpdateEquipment(dialog.ResultEquipment))
                    {
                        RefreshEquipmentList();
                        SaveEquipment();
                        UpdateStatus("Оборудование обновлено");
                    }
                    else
                    {
                        ShowError("Не удалось обновить оборудование");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при редактировании: {ex.Message}");
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EquipmentListView.SelectedItem == null) return;

                var selectedItem = EquipmentListView.SelectedItem;
                var idProperty = selectedItem.GetType().GetProperty("Id");
                var id = (int)(idProperty?.GetValue(selectedItem) ?? 0);

                var confirmDialog = new ContentDialog
                {
                    Title = "Подтверждение",
                    Content = "Вы уверены, что хотите удалить это оборудование?",
                    PrimaryButtonText = "Да",
                    CloseButtonText = "Нет",
                    XamlRoot = this.Content?.XamlRoot
                };

                if (confirmDialog.XamlRoot == null)
                {
                    UpdateStatus("Ошибка: окно не готово");
                    return;
                }

                var result = await confirmDialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    if (_equipmentManager.RemoveEquipment(id))
                    {
                        RefreshEquipmentList();
                        SaveEquipment();
                        UpdateStatus("Оборудование удалено");
                    }
                    else
                    {
                        ShowError("Не удалось удалить оборудование");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при удалении: {ex.Message}");
            }
        }

        private void EquipmentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonsState();
        }

        private void SearchTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            PerformSearch();
        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchNameTextBox != null) SearchNameTextBox.Text = string.Empty;
            if (SearchManufacturerTextBox != null) SearchManufacturerTextBox.Text = string.Empty;
            if (SearchLocationTextBox != null) SearchLocationTextBox.Text = string.Empty;
            if (StatusFilterComboBox != null) StatusFilterComboBox.SelectedIndex = 0;
            PerformSearch();
        }

        private void PerformSearch()
        {
            try
            {
                if (_equipmentManager == null)
                {
                    return;
                }

                string? status = null;
                if (StatusFilterComboBox?.SelectedItem is ComboBoxItem selectedStatus && 
                    selectedStatus.Content?.ToString() != "Все")
                {
                    status = selectedStatus.Content.ToString();
                }

                var results = _equipmentManager.Search(
                    name: string.IsNullOrWhiteSpace(SearchNameTextBox?.Text) ? null : SearchNameTextBox.Text,
                    manufacturer: string.IsNullOrWhiteSpace(SearchManufacturerTextBox?.Text) ? null : SearchManufacturerTextBox.Text,
                    location: string.IsNullOrWhiteSpace(SearchLocationTextBox?.Text) ? null : SearchLocationTextBox.Text,
                    status: status
                );

                ApplySorting(results);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"PerformSearch exception: {ex}");
                UpdateStatus($"Ошибка при поиске: {ex.Message}");
            }
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EquipmentListView.ItemsSource != null)
            {
                var currentList = _currentEquipmentList.ToList();
                ApplySorting(currentList);
            }
        }

        private void ApplySorting(List<Equipment> equipmentList)
        {
            if (SortComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                var tag = selectedItem.Tag?.ToString();
                var sortedList = tag switch
                {
                    "NameAsc" => _equipmentManager.SortByName(true),
                    "NameDesc" => _equipmentManager.SortByName(false),
                    "PriceAsc" => _equipmentManager.SortByPriceAscending(),
                    "PriceDesc" => _equipmentManager.SortByPriceDescending(),
                    "DateAsc" => _equipmentManager.SortByPurchaseDate(true),
                    "DateDesc" => _equipmentManager.SortByPurchaseDate(false),
                    _ => equipmentList
                };

                if (tag != "None")
                {
                    var searchResults = PerformSearchOnList(sortedList);
                    UpdateDataGrid(searchResults);
                }
                else
                {
                    UpdateDataGrid(equipmentList);
                }
            }
            else
            {
                UpdateDataGrid(equipmentList);
            }
        }

        private List<Equipment> PerformSearchOnList(List<Equipment> list)
        {
            try
            {
                string? status = null;
                if (StatusFilterComboBox?.SelectedItem is ComboBoxItem selectedStatus && 
                    selectedStatus.Content?.ToString() != "Все")
                {
                    status = selectedStatus.Content.ToString();
                }

                var nameFilter = SearchNameTextBox?.Text ?? string.Empty;
                var manufacturerFilter = SearchManufacturerTextBox?.Text ?? string.Empty;
                var locationFilter = SearchLocationTextBox?.Text ?? string.Empty;

                return list.Where(e =>
                    (string.IsNullOrWhiteSpace(nameFilter) || 
                     e.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(manufacturerFilter) || 
                     e.Manufacturer.Contains(manufacturerFilter, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(locationFilter) || 
                     e.Location.Contains(locationFilter, StringComparison.OrdinalIgnoreCase)) &&
                    (status == null || e.Status == status)
                ).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"PerformSearchOnList exception: {ex}");
                return list;
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveEquipment();
                _authService?.Logout();
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при выходе: {ex.Message}");
            }
        }

        private async void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EquipmentListView?.SelectedItem == null)
                {
                    UpdateStatus("Выберите оборудование для просмотра");
                    return;
                }

                var selectedItem = EquipmentListView.SelectedItem;
                var equipmentProperty = selectedItem.GetType().GetProperty("Equipment");
                Equipment? equipment = null;

                if (equipmentProperty != null)
                {
                    equipment = equipmentProperty.GetValue(selectedItem) as Equipment;
                }

                if (equipment == null)
                {

                    var idProperty = selectedItem.GetType().GetProperty("Id");
                    if (idProperty != null && idProperty.GetValue(selectedItem) is int id)
                    {
                        equipment = _equipmentManager?.GetEquipmentById(id);
                    }
                }

                if (equipment == null)
                {
                    UpdateStatus("Ошибка: не удалось получить данные оборудования");
                    return;
                }

                var viewDialog = new ContentDialog
                {
                    Title = "Просмотр оборудования",
                    Content = new ScrollViewer
                    {
                        Content = new TextBlock
                        {
                            Text = equipment.GetFullInfo(),
                            TextWrapping = Microsoft.UI.Xaml.TextWrapping.Wrap,
                            FontSize = 14,
                            Margin = new Microsoft.UI.Xaml.Thickness(10)
                        },
                        MaxHeight = 500
                    },
                    CloseButtonText = "Закрыть",
                    XamlRoot = this.Content?.XamlRoot
                };

                if (viewDialog.XamlRoot == null)
                {
                    UpdateStatus("Ошибка: окно не готово");
                    return;
                }

                await viewDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при просмотре оборудования: {ex.Message}");
            }
        }

        private void UsersManagementButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!_authService.IsAdministrator)
                {
                    UpdateStatus("Только администратор может управлять пользователями");
                    return;
                }

                var usersWindow = new UsersManagementWindow(_fileService);
                usersWindow.Activate();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при открытии окна управления пользователями: {ex.Message}");
            }
        }

        private void MainWindow_Closed(object sender, WindowEventArgs args)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("MainWindow_Closed: сохраняем данные перед закрытием");
                SaveEquipment();
                System.Diagnostics.Debug.WriteLine("MainWindow_Closed: данные сохранены");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"MainWindow_Closed exception: {ex}");
            }
        }
    }
}
