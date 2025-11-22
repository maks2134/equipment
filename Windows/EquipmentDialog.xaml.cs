using Microsoft.UI.Xaml.Controls;
using equipment_accounting_system.Models;
using System;

namespace equipment_accounting_system.Windows
{
    public sealed partial class EquipmentDialog : ContentDialog
    {
        public Equipment? ResultEquipment { get; private set; }

        public EquipmentDialog(Equipment? equipment = null)
        {
            InitializeComponent();

            PrimaryButtonClick += ContentDialog_PrimaryButtonClick;

            if (equipment != null)
            {
                LoadEquipment(equipment);
            }
            else
            {
                PurchaseDatePicker.Date = DateTimeOffset.Now;

                BaseTypeComboBox.SelectedIndex = 0;
                BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
            }
        }

        private void LoadEquipment(Equipment equipment)
        {
            NameTextBox.Text = equipment.Name;
            ManufacturerTextBox.Text = equipment.Manufacturer;
            PriceNumberBox.Value = (double)equipment.Price;
            PurchaseDatePicker.Date = new DateTimeOffset(equipment.PurchaseDate);
            LocationTextBox.Text = equipment.Location;

            foreach (ComboBoxItem item in StatusComboBox.Items)
            {
                if (item.Content.ToString() == equipment.Status)
                {
                    StatusComboBox.SelectedItem = item;
                    break;
                }
            }

            switch (equipment)
            {

                case DesktopEquipment de:
                    BaseTypeComboBox.SelectedIndex = 0;
                    BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
                    EquipmentTypeComboBox.SelectedIndex = 1;
                    LoadDesktopEquipment(de);
                    break;
                case LaptopEquipment le:
                    BaseTypeComboBox.SelectedIndex = 0;
                    BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
                    EquipmentTypeComboBox.SelectedIndex = 2;
                    LoadLaptopEquipment(le);
                    break;
                case WorkstationEquipment we:
                    BaseTypeComboBox.SelectedIndex = 0;
                    BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
                    EquipmentTypeComboBox.SelectedIndex = 3;
                    LoadWorkstationEquipment(we);
                    break;
                case ServerEquipment se:
                    BaseTypeComboBox.SelectedIndex = 0;
                    BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
                    EquipmentTypeComboBox.SelectedIndex = 4;
                    LoadServerEquipment(se);
                    break;
                case NetworkEquipment ne:
                    BaseTypeComboBox.SelectedIndex = 0;
                    BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
                    EquipmentTypeComboBox.SelectedIndex = 5;
                    LoadNetworkEquipment(ne);
                    break;

                case PrinterEquipment pe:
                    BaseTypeComboBox.SelectedIndex = 1;
                    BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
                    EquipmentTypeComboBox.SelectedIndex = 1;
                    LoadPrinterEquipment(pe);
                    break;
                case ScannerEquipment sce:
                    BaseTypeComboBox.SelectedIndex = 1;
                    BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
                    EquipmentTypeComboBox.SelectedIndex = 2;
                    LoadScannerEquipment(sce);
                    break;
                case CopierEquipment ce:
                    BaseTypeComboBox.SelectedIndex = 1;
                    BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
                    EquipmentTypeComboBox.SelectedIndex = 3;
                    LoadCopierEquipment(ce);
                    break;
                case FaxEquipment fe:
                    BaseTypeComboBox.SelectedIndex = 1;
                    BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
                    EquipmentTypeComboBox.SelectedIndex = 4;
                    LoadFaxEquipment(fe);
                    break;

                case TabletEquipment te:
                    BaseTypeComboBox.SelectedIndex = 2;
                    BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
                    EquipmentTypeComboBox.SelectedIndex = 1;
                    LoadTabletEquipment(te);
                    break;
                case SmartphoneEquipment spe:
                    BaseTypeComboBox.SelectedIndex = 2;
                    BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
                    EquipmentTypeComboBox.SelectedIndex = 2;
                    LoadSmartphoneEquipment(spe);
                    break;

                case ComputerEquipment comp:
                    BaseTypeComboBox.SelectedIndex = 0;
                    BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
                    EquipmentTypeComboBox.SelectedIndex = 0;
                    LoadComputerEquipment(comp);
                    break;
                case OfficeEquipment oe:
                    BaseTypeComboBox.SelectedIndex = 1;
                    BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
                    EquipmentTypeComboBox.SelectedIndex = 0;
                    LoadOfficeEquipment(oe);
                    break;
                case MobileEquipment me:
                    BaseTypeComboBox.SelectedIndex = 2;
                    BaseTypeComboBox_SelectionChanged(BaseTypeComboBox, null);
                    EquipmentTypeComboBox.SelectedIndex = 0;
                    LoadMobileEquipment(me);
                    break;
            }
        }

        private void LoadComputerEquipment(ComputerEquipment ce)
        {
            ProcessorTextBox.Text = ce.Processor;
            RAMNumberBox.Value = ce.RAM;
            StorageNumberBox.Value = ce.Storage;
            OSTextBox.Text = ce.OperatingSystem;
        }

        private void LoadOfficeEquipment(OfficeEquipment oe)
        {
            CategoryTextBox.Text = oe.EquipmentCategory;
            PrintSpeedNumberBox.Value = oe.PrintSpeed;
            IsColorCheckBox.IsChecked = oe.IsColor;
        }

        private void LoadNetworkEquipment(NetworkEquipment ne)
        {
            LoadComputerEquipment(ne);
            PortCountNumberBox.Value = ne.PortCount;
            ConnectionTypeTextBox.Text = ne.ConnectionType;
            MaxSpeedNumberBox.Value = ne.MaxSpeed;
        }

        private void LoadServerEquipment(ServerEquipment se)
        {
            LoadComputerEquipment(se);
            RackUnitNumberBox.Value = se.RackUnit;
            IsRedundantPowerSupplyCheckBox.IsChecked = se.IsRedundantPowerSupply;
            NetworkPortsNumberBox.Value = se.NetworkPorts;
            ServerTypeTextBox.Text = se.ServerType;
        }

        private void LoadMobileEquipment(MobileEquipment me)
        {
            ScreenSizeTextBox.Text = me.ScreenSize;
            BatteryCapacityNumberBox.Value = me.BatteryCapacity;
            HasSIMCardCheckBox.IsChecked = me.HasSIMCard;
            MobileOSTextBox.Text = me.OperatingSystem;
        }

        private void LoadPrinterEquipment(PrinterEquipment pe)
        {
            LoadOfficeEquipment(pe);
            PrinterTypeTextBox.Text = pe.PrinterType;
            MaxPaperSizeNumberBox.Value = pe.MaxPaperSize;
            HasDuplexCheckBox.IsChecked = pe.HasDuplex;
            CartridgeCountNumberBox.Value = pe.CartridgeCount;
        }

        private void LoadDesktopEquipment(DesktopEquipment de)
        {
            LoadComputerEquipment(de);
            FormFactorTextBox.Text = de.FormFactor;
            PowerSupplyNumberBox.Value = de.PowerSupply;
            ExpansionSlotsNumberBox.Value = de.ExpansionSlots;
            HasOpticalDriveCheckBox.IsChecked = de.HasOpticalDrive;
        }

        private void LoadLaptopEquipment(LaptopEquipment le)
        {
            LoadComputerEquipment(le);
            LaptopScreenSizeTextBox.Text = le.ScreenSize;
            BatteryLifeNumberBox.Value = le.BatteryLife;
            WeightNumberBox.Value = (double)le.Weight;
            HasTouchscreenCheckBox.IsChecked = le.HasTouchscreen;
        }

        private void LoadWorkstationEquipment(WorkstationEquipment we)
        {
            LoadComputerEquipment(we);
            GraphicsCardTextBox.Text = we.GraphicsCard;
            VRAMNumberBox.Value = we.VRAM;
            HasECCCheckBox.IsChecked = we.HasECC;
            MaxRAMNumberBox.Value = we.MaxRAM;
        }

        private void LoadScannerEquipment(ScannerEquipment sce)
        {
            LoadOfficeEquipment(sce);
            ResolutionNumberBox.Value = sce.Resolution;
            ScanTypeTextBox.Text = sce.ScanType;
            MaxDocumentSizeNumberBox.Value = sce.MaxDocumentSize;
            HasADFCheckBox.IsChecked = sce.HasADF;
        }

        private void LoadCopierEquipment(CopierEquipment ce)
        {
            LoadOfficeEquipment(ce);
            CopySpeedNumberBox.Value = ce.CopySpeed;
            CopierMaxPaperSizeNumberBox.Value = ce.MaxPaperSize;
            CopierHasDuplexCheckBox.IsChecked = ce.HasDuplex;
            PaperCapacityNumberBox.Value = ce.PaperCapacity;
        }

        private void LoadFaxEquipment(FaxEquipment fe)
        {
            LoadOfficeEquipment(fe);
            TransmissionSpeedTextBox.Text = fe.TransmissionSpeed;
            MemoryPagesNumberBox.Value = fe.MemoryPages;
            HasAnsweringMachineCheckBox.IsChecked = fe.HasAnsweringMachine;
            FaxConnectionTypeTextBox.Text = fe.ConnectionType;
        }

        private void LoadTabletEquipment(TabletEquipment te)
        {
            LoadMobileEquipment(te);
            HasKeyboardCheckBox.IsChecked = te.HasKeyboard;
            HasStylusCheckBox.IsChecked = te.HasStylus;
            ConnectivityTextBox.Text = te.Connectivity;
        }

        private void LoadSmartphoneEquipment(SmartphoneEquipment spe)
        {
            LoadMobileEquipment(spe);
            CameraMPNumberBox.Value = spe.CameraMP;
            StorageCapacityNumberBox.Value = spe.StorageCapacity;
            HasNFCCheckBox.IsChecked = spe.HasNFC;
            NetworkStandardTextBox.Text = spe.NetworkStandard;
        }

        private void BaseTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BaseTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                var baseTag = selectedItem.Tag?.ToString();
                EquipmentTypeComboBox.Items.Clear();
                EquipmentTypeComboBox.IsEnabled = true;

                switch (baseTag)
                {
                    case "Computer":
                        EquipmentTypeComboBox.Items.Add(new ComboBoxItem { Content = "Компьютерное оборудование", Tag = "Computer" });
                        EquipmentTypeComboBox.Items.Add(new ComboBoxItem { Content = "Настольный компьютер", Tag = "Desktop" });
                        EquipmentTypeComboBox.Items.Add(new ComboBoxItem { Content = "Ноутбук", Tag = "Laptop" });
                        EquipmentTypeComboBox.Items.Add(new ComboBoxItem { Content = "Рабочая станция", Tag = "Workstation" });
                        EquipmentTypeComboBox.Items.Add(new ComboBoxItem { Content = "Серверное оборудование", Tag = "Server" });
                        EquipmentTypeComboBox.Items.Add(new ComboBoxItem { Content = "Сетевое оборудование", Tag = "Network" });
                        EquipmentTypeComboBox.SelectedIndex = 0;
                        break;
                    case "Office":
                        EquipmentTypeComboBox.Items.Add(new ComboBoxItem { Content = "Офисное оборудование", Tag = "Office" });
                        EquipmentTypeComboBox.Items.Add(new ComboBoxItem { Content = "Принтер", Tag = "Printer" });
                        EquipmentTypeComboBox.Items.Add(new ComboBoxItem { Content = "Сканер", Tag = "Scanner" });
                        EquipmentTypeComboBox.Items.Add(new ComboBoxItem { Content = "Копир", Tag = "Copier" });
                        EquipmentTypeComboBox.Items.Add(new ComboBoxItem { Content = "Факс", Tag = "Fax" });
                        EquipmentTypeComboBox.SelectedIndex = 0;
                        break;
                    case "Mobile":
                        EquipmentTypeComboBox.Items.Add(new ComboBoxItem { Content = "Мобильное оборудование", Tag = "Mobile" });
                        EquipmentTypeComboBox.Items.Add(new ComboBoxItem { Content = "Планшет", Tag = "Tablet" });
                        EquipmentTypeComboBox.Items.Add(new ComboBoxItem { Content = "Смартфон", Tag = "Smartphone" });
                        EquipmentTypeComboBox.SelectedIndex = 0;
                        break;
                }

                EquipmentTypeComboBox_SelectionChanged(EquipmentTypeComboBox, null);
            }
        }

        private void EquipmentTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EquipmentTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                var tag = selectedItem.Tag?.ToString();

                bool isComputerType = tag == "Computer" || tag == "Desktop" || tag == "Laptop" || 
                                     tag == "Workstation" || tag == "Server" || tag == "Network";
                ComputerFieldsPanel.Visibility = isComputerType 
                    ? Microsoft.UI.Xaml.Visibility.Visible 
                    : Microsoft.UI.Xaml.Visibility.Collapsed;

                bool isOfficeType = tag == "Office" || tag == "Printer" || tag == "Scanner" || 
                                   tag == "Copier" || tag == "Fax";
                OfficeFieldsPanel.Visibility = isOfficeType 
                    ? Microsoft.UI.Xaml.Visibility.Visible 
                    : Microsoft.UI.Xaml.Visibility.Collapsed;

                bool isMobileType = tag == "Mobile" || tag == "Tablet" || tag == "Smartphone";
                MobileFieldsPanel.Visibility = isMobileType 
                    ? Microsoft.UI.Xaml.Visibility.Visible 
                    : Microsoft.UI.Xaml.Visibility.Collapsed;

                NetworkFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                ServerFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                PrinterFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                DesktopFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                LaptopFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                WorkstationFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                ScannerFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                CopierFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                FaxFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                TabletFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                SmartphoneFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;

                switch (tag)
                {
                    case "Network":
                        NetworkFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                        break;
                    case "Server":
                        ServerFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                        break;
                    case "Desktop":
                        DesktopFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                        break;
                    case "Laptop":
                        LaptopFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                        break;
                    case "Workstation":
                        WorkstationFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                        break;
                    case "Printer":
                        PrinterFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                        break;
                    case "Scanner":
                        ScannerFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                        break;
                    case "Copier":
                        CopierFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                        break;
                    case "Fax":
                        FaxFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                        break;
                    case "Tablet":
                        TabletFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                        break;
                    case "Smartphone":
                        SmartphoneFieldsPanel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                        break;
                }
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            try
            {
                var deferral = args.GetDeferral();

                if (ValidateInput())
                {
                    ResultEquipment = CreateEquipment();
                    deferral.Complete();
                }
                else
                {
                    args.Cancel = true;
                    deferral.Complete();
                }
            }
            catch (Exception ex)
            {
                args.Cancel = true;
                var errorDialog = new ContentDialog
                {
                    Title = "Ошибка",
                    Content = $"Ошибка при создании оборудования: {ex.Message}",
                    CloseButtonText = "OK"
                };
                _ = errorDialog.ShowAsync();
            }
        }

        private bool ValidateInput()
        {
            ErrorTextBlock.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                ShowError("Введите название оборудования");
                return false;
            }

            if (string.IsNullOrWhiteSpace(ManufacturerTextBox.Text))
            {
                ShowError("Введите производителя");
                return false;
            }

            if (PriceNumberBox.Value <= 0)
            {
                ShowError("Цена должна быть больше 0");
                return false;
            }

            if (BaseTypeComboBox.SelectedItem == null)
            {
                ShowError("Выберите категорию оборудования");
                return false;
            }

            if (EquipmentTypeComboBox.SelectedItem == null)
            {
                ShowError("Выберите тип оборудования");
                return false;
            }

            var selectedItem = EquipmentTypeComboBox.SelectedItem as ComboBoxItem;
            var tag = selectedItem?.Tag?.ToString();

            if (tag == "Computer" || tag == "Desktop" || tag == "Laptop" || tag == "Workstation" || 
                tag == "Server" || tag == "Network")
            {
                if (string.IsNullOrWhiteSpace(ProcessorTextBox.Text))
                {
                    ShowError("Введите процессор");
                    return false;
                }
                if (RAMNumberBox.Value <= 0)
                {
                    ShowError("ОЗУ должна быть больше 0");
                    return false;
                }
                if (StorageNumberBox.Value <= 0)
                {
                    ShowError("Объем накопителя должен быть больше 0");
                    return false;
                }
            }

            if (tag == "Office" || tag == "Printer" || tag == "Scanner" || tag == "Copier" || tag == "Fax")
            {
                if (string.IsNullOrWhiteSpace(CategoryTextBox.Text))
                {
                    ShowError("Введите категорию оборудования");
                    return false;
                }
            }

            if (tag == "Network")
            {
                if (PortCountNumberBox.Value <= 0)
                {
                    ShowError("Количество портов должно быть больше 0");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(ConnectionTypeTextBox.Text))
                {
                    ShowError("Введите тип подключения");
                    return false;
                }
            }

            if (tag == "Server")
            {
                if (RackUnitNumberBox.Value <= 0)
                {
                    ShowError("Высота в стойке должна быть больше 0");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(ServerTypeTextBox.Text))
                {
                    ShowError("Введите тип сервера");
                    return false;
                }
            }

            if (tag == "Mobile" || tag == "Tablet" || tag == "Smartphone")
            {
                if (string.IsNullOrWhiteSpace(ScreenSizeTextBox.Text))
                {
                    ShowError("Введите размер экрана");
                    return false;
                }
                if (BatteryCapacityNumberBox.Value <= 0)
                {
                    ShowError("Емкость батареи должна быть больше 0");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(MobileOSTextBox.Text))
                {
                    ShowError("Введите операционную систему");
                    return false;
                }
            }

            if (tag == "Printer")
            {
                if (string.IsNullOrWhiteSpace(PrinterTypeTextBox.Text))
                {
                    ShowError("Введите тип принтера");
                    return false;
                }
                if (CartridgeCountNumberBox.Value <= 0)
                {
                    ShowError("Количество картриджей должно быть больше 0");
                    return false;
                }
            }

            if (tag == "Desktop")
            {
                if (string.IsNullOrWhiteSpace(FormFactorTextBox.Text))
                {
                    ShowError("Введите форм-фактор");
                    return false;
                }
            }

            if (tag == "Laptop")
            {
                if (string.IsNullOrWhiteSpace(LaptopScreenSizeTextBox.Text))
                {
                    ShowError("Введите размер экрана");
                    return false;
                }
            }

            if (tag == "Workstation")
            {
                if (string.IsNullOrWhiteSpace(GraphicsCardTextBox.Text))
                {
                    ShowError("Введите модель видеокарты");
                    return false;
                }
            }

            if (tag == "Scanner")
            {
                if (ResolutionNumberBox.Value <= 0)
                {
                    ShowError("Разрешение должно быть больше 0");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(ScanTypeTextBox.Text))
                {
                    ShowError("Введите тип сканера");
                    return false;
                }
            }

            if (tag == "Copier")
            {
                if (CopySpeedNumberBox.Value <= 0)
                {
                    ShowError("Скорость копирования должна быть больше 0");
                    return false;
                }
            }

            if (tag == "Fax")
            {
                if (string.IsNullOrWhiteSpace(TransmissionSpeedTextBox.Text))
                {
                    ShowError("Введите скорость передачи");
                    return false;
                }
            }

            if (tag == "Tablet")
            {
                if (string.IsNullOrWhiteSpace(ConnectivityTextBox.Text))
                {
                    ShowError("Введите тип подключения");
                    return false;
                }
            }

            if (tag == "Smartphone")
            {
                if (StorageCapacityNumberBox.Value <= 0)
                {
                    ShowError("Встроенная память должна быть больше 0");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(NetworkStandardTextBox.Text))
                {
                    ShowError("Введите стандарт связи");
                    return false;
                }
            }

            return true;
        }

        private void ShowError(string message)
        {
            ErrorTextBlock.Text = message;
            ErrorTextBlock.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
        }

        private Equipment CreateEquipment()
        {
            var selectedItem = EquipmentTypeComboBox.SelectedItem as ComboBoxItem;
            var tag = selectedItem?.Tag?.ToString();
            var status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "В эксплуатации";

            Equipment equipment = tag switch
            {

                "Desktop" => new DesktopEquipment
                {
                    Processor = ProcessorTextBox.Text,
                    RAM = (int)RAMNumberBox.Value,
                    Storage = (int)StorageNumberBox.Value,
                    OperatingSystem = OSTextBox.Text,
                    FormFactor = FormFactorTextBox.Text,
                    PowerSupply = (int)PowerSupplyNumberBox.Value,
                    ExpansionSlots = (int)ExpansionSlotsNumberBox.Value,
                    HasOpticalDrive = HasOpticalDriveCheckBox.IsChecked ?? false
                },
                "Laptop" => new LaptopEquipment
                {
                    Processor = ProcessorTextBox.Text,
                    RAM = (int)RAMNumberBox.Value,
                    Storage = (int)StorageNumberBox.Value,
                    OperatingSystem = OSTextBox.Text,
                    ScreenSize = LaptopScreenSizeTextBox.Text,
                    BatteryLife = (int)BatteryLifeNumberBox.Value,
                    Weight = (decimal)WeightNumberBox.Value,
                    HasTouchscreen = HasTouchscreenCheckBox.IsChecked ?? false
                },
                "Workstation" => new WorkstationEquipment
                {
                    Processor = ProcessorTextBox.Text,
                    RAM = (int)RAMNumberBox.Value,
                    Storage = (int)StorageNumberBox.Value,
                    OperatingSystem = OSTextBox.Text,
                    GraphicsCard = GraphicsCardTextBox.Text,
                    VRAM = (int)VRAMNumberBox.Value,
                    HasECC = HasECCCheckBox.IsChecked ?? false,
                    MaxRAM = (int)MaxRAMNumberBox.Value
                },
                "Server" => new ServerEquipment
                {
                    Processor = ProcessorTextBox.Text,
                    RAM = (int)RAMNumberBox.Value,
                    Storage = (int)StorageNumberBox.Value,
                    OperatingSystem = OSTextBox.Text,
                    RackUnit = (int)RackUnitNumberBox.Value,
                    IsRedundantPowerSupply = IsRedundantPowerSupplyCheckBox.IsChecked ?? false,
                    NetworkPorts = (int)NetworkPortsNumberBox.Value,
                    ServerType = ServerTypeTextBox.Text
                },
                "Network" => new NetworkEquipment
                {
                    Processor = ProcessorTextBox.Text,
                    RAM = (int)RAMNumberBox.Value,
                    Storage = (int)StorageNumberBox.Value,
                    OperatingSystem = OSTextBox.Text,
                    PortCount = (int)PortCountNumberBox.Value,
                    ConnectionType = ConnectionTypeTextBox.Text,
                    MaxSpeed = (int)MaxSpeedNumberBox.Value
                },
                "Computer" => new ComputerEquipment
                {
                    Processor = ProcessorTextBox.Text,
                    RAM = (int)RAMNumberBox.Value,
                    Storage = (int)StorageNumberBox.Value,
                    OperatingSystem = OSTextBox.Text
                },

                "Printer" => new PrinterEquipment
                {
                    EquipmentCategory = CategoryTextBox.Text,
                    PrintSpeed = (int)PrintSpeedNumberBox.Value,
                    IsColor = IsColorCheckBox.IsChecked ?? false,
                    PrinterType = PrinterTypeTextBox.Text,
                    MaxPaperSize = (int)MaxPaperSizeNumberBox.Value,
                    HasDuplex = HasDuplexCheckBox.IsChecked ?? false,
                    CartridgeCount = (int)CartridgeCountNumberBox.Value
                },
                "Scanner" => new ScannerEquipment
                {
                    EquipmentCategory = CategoryTextBox.Text,
                    PrintSpeed = (int)PrintSpeedNumberBox.Value,
                    IsColor = IsColorCheckBox.IsChecked ?? false,
                    Resolution = (int)ResolutionNumberBox.Value,
                    ScanType = ScanTypeTextBox.Text,
                    MaxDocumentSize = (int)MaxDocumentSizeNumberBox.Value,
                    HasADF = HasADFCheckBox.IsChecked ?? false
                },
                "Copier" => new CopierEquipment
                {
                    EquipmentCategory = CategoryTextBox.Text,
                    PrintSpeed = (int)PrintSpeedNumberBox.Value,
                    IsColor = IsColorCheckBox.IsChecked ?? false,
                    CopySpeed = (int)CopySpeedNumberBox.Value,
                    MaxPaperSize = (int)CopierMaxPaperSizeNumberBox.Value,
                    HasDuplex = CopierHasDuplexCheckBox.IsChecked ?? false,
                    PaperCapacity = (int)PaperCapacityNumberBox.Value
                },
                "Fax" => new FaxEquipment
                {
                    EquipmentCategory = CategoryTextBox.Text,
                    PrintSpeed = (int)PrintSpeedNumberBox.Value,
                    IsColor = IsColorCheckBox.IsChecked ?? false,
                    TransmissionSpeed = TransmissionSpeedTextBox.Text,
                    MemoryPages = (int)MemoryPagesNumberBox.Value,
                    HasAnsweringMachine = HasAnsweringMachineCheckBox.IsChecked ?? false,
                    ConnectionType = FaxConnectionTypeTextBox.Text
                },
                "Office" => new OfficeEquipment
                {
                    EquipmentCategory = CategoryTextBox.Text,
                    PrintSpeed = (int)PrintSpeedNumberBox.Value,
                    IsColor = IsColorCheckBox.IsChecked ?? false
                },

                "Tablet" => new TabletEquipment
                {
                    ScreenSize = ScreenSizeTextBox.Text,
                    BatteryCapacity = (int)BatteryCapacityNumberBox.Value,
                    HasSIMCard = HasSIMCardCheckBox.IsChecked ?? false,
                    OperatingSystem = MobileOSTextBox.Text,
                    HasKeyboard = HasKeyboardCheckBox.IsChecked ?? false,
                    HasStylus = HasStylusCheckBox.IsChecked ?? false,
                    Connectivity = ConnectivityTextBox.Text
                },
                "Smartphone" => new SmartphoneEquipment
                {
                    ScreenSize = ScreenSizeTextBox.Text,
                    BatteryCapacity = (int)BatteryCapacityNumberBox.Value,
                    HasSIMCard = HasSIMCardCheckBox.IsChecked ?? false,
                    OperatingSystem = MobileOSTextBox.Text,
                    CameraMP = (int)CameraMPNumberBox.Value,
                    StorageCapacity = (int)StorageCapacityNumberBox.Value,
                    HasNFC = HasNFCCheckBox.IsChecked ?? false,
                    NetworkStandard = NetworkStandardTextBox.Text
                },
                "Mobile" => new MobileEquipment
                {
                    ScreenSize = ScreenSizeTextBox.Text,
                    BatteryCapacity = (int)BatteryCapacityNumberBox.Value,
                    HasSIMCard = HasSIMCardCheckBox.IsChecked ?? false,
                    OperatingSystem = MobileOSTextBox.Text
                },
                _ => new ComputerEquipment()
            };

            equipment.Name = NameTextBox.Text;
            equipment.Manufacturer = ManufacturerTextBox.Text;
            equipment.Price = (decimal)PriceNumberBox.Value;
            equipment.PurchaseDate = PurchaseDatePicker.Date.DateTime;
            equipment.Location = LocationTextBox.Text;
            equipment.Status = status;

            return equipment;
        }
    }
}
