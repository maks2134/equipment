using Microsoft.UI.Xaml;
using Windows.ApplicationModel.Activation;
using equipment_accounting_system.Services;
using equipment_accounting_system.Windows;

namespace equipment_accounting_system
{
    public partial class App : Application
    {
        private Window? _window;
        private FileService? _fileService;
        private AuthService? _authService;
        private EquipmentManager? _equipmentManager;

        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _fileService = new FileService();
            _authService = new AuthService(_fileService);
            _equipmentManager = new EquipmentManager();

            ShowLoginWindow();
        }

        private void ShowLoginWindow()
        {
            if (_authService == null || _fileService == null) return;
            var loginWindow = new LoginWindow(_authService, _fileService);
            loginWindow.Closed += (sender, e) =>
            {
                if (_authService?.IsAuthenticated == true)
                {
                    ShowMainWindow();
                }
                else
                {
                    Exit();
                }
            };
            loginWindow.Activate();
        }

        private void ShowMainWindow()
        {
            if (_equipmentManager != null && _fileService != null && _authService != null)
            {
                _window = new MainWindow(_equipmentManager, _fileService, _authService);
                _window.Closed += (sender, e) =>
                {
                    ShowLoginWindow();
                };
                _window.Activate();
            }
        }
    }
}
