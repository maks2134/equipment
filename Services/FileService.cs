using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using equipment_accounting_system.Models;

namespace equipment_accounting_system.Services
{
    public class FileService
    {
        private readonly string _equipmentFilePath;
        private readonly string _usersFilePath;

        public FileService()
        {
            var projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var dataFolder = Path.Combine(projectDirectory, "data");

            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            _equipmentFilePath = Path.Combine(dataFolder, "equipment.json");
            _usersFilePath = Path.Combine(dataFolder, "users.json");

            System.Diagnostics.Debug.WriteLine($"FileService: путь к файлам - {dataFolder}");
        }

        private JsonSerializerOptions GetJsonOptions()
        {
            return new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public void SaveEquipment(List<Equipment> equipmentList)
        {
            try
            {
                if (equipmentList == null)
                {
                    equipmentList = new List<Equipment>();
                }

                var options = GetJsonOptions();

                var jsonElements = new List<string>();
                foreach (var equipment in equipmentList)
                {
                    string jsonElement;
                    switch (equipment)
                    {

                        case DesktopEquipment de:
                            jsonElement = JsonSerializer.Serialize(de, typeof(DesktopEquipment), options);
                            break;
                        case LaptopEquipment le:
                            jsonElement = JsonSerializer.Serialize(le, typeof(LaptopEquipment), options);
                            break;
                        case WorkstationEquipment we:
                            jsonElement = JsonSerializer.Serialize(we, typeof(WorkstationEquipment), options);
                            break;
                        case ServerEquipment se:
                            jsonElement = JsonSerializer.Serialize(se, typeof(ServerEquipment), options);
                            break;
                        case NetworkEquipment ne:
                            jsonElement = JsonSerializer.Serialize(ne, typeof(NetworkEquipment), options);
                            break;

                        case PrinterEquipment pe:
                            jsonElement = JsonSerializer.Serialize(pe, typeof(PrinterEquipment), options);
                            break;
                        case ScannerEquipment sce:
                            jsonElement = JsonSerializer.Serialize(sce, typeof(ScannerEquipment), options);
                            break;
                        case CopierEquipment ce:
                            jsonElement = JsonSerializer.Serialize(ce, typeof(CopierEquipment), options);
                            break;
                        case FaxEquipment fe:
                            jsonElement = JsonSerializer.Serialize(fe, typeof(FaxEquipment), options);
                            break;

                        case TabletEquipment te:
                            jsonElement = JsonSerializer.Serialize(te, typeof(TabletEquipment), options);
                            break;
                        case SmartphoneEquipment spe:
                            jsonElement = JsonSerializer.Serialize(spe, typeof(SmartphoneEquipment), options);
                            break;

                        case ComputerEquipment comp:
                            jsonElement = JsonSerializer.Serialize(comp, typeof(ComputerEquipment), options);
                            break;
                        case OfficeEquipment oe:
                            jsonElement = JsonSerializer.Serialize(oe, typeof(OfficeEquipment), options);
                            break;
                        case MobileEquipment me:
                            jsonElement = JsonSerializer.Serialize(me, typeof(MobileEquipment), options);
                            break;
                        default:
                            jsonElement = JsonSerializer.Serialize(equipment, equipment.GetType(), options);
                            break;
                    }
                    jsonElements.Add(jsonElement);
                }

                var json = "[" + string.Join(",", jsonElements) + "]";

                var directory = Path.GetDirectoryName(_equipmentFilePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(_equipmentFilePath, json);
                System.Diagnostics.Debug.WriteLine($"SaveEquipment: сохранено в {_equipmentFilePath}, записей: {equipmentList.Count}");
                System.Diagnostics.Debug.WriteLine($"SaveEquipment: размер JSON: {json.Length} символов");
                if (json.Length > 0 && json.Length <= 500)
                {
                    System.Diagnostics.Debug.WriteLine($"SaveEquipment: содержимое JSON: {json}");
                }
                else if (json.Length > 500)
                {
                    System.Diagnostics.Debug.WriteLine($"SaveEquipment: первые 500 символов JSON: {json.Substring(0, 500)}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SaveEquipment exception: {ex}");
                throw new Exception($"Ошибка при сохранении данных: {ex.Message}", ex);
            }
        }

        public List<Equipment> LoadEquipment()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"LoadEquipment: путь к файлу: {_equipmentFilePath}");
                System.Diagnostics.Debug.WriteLine($"LoadEquipment: файл существует: {File.Exists(_equipmentFilePath)}");

                if (!File.Exists(_equipmentFilePath))
                {
                    System.Diagnostics.Debug.WriteLine("LoadEquipment: файл не существует, возвращаем пустой список");
                    return new List<Equipment>();
                }

                var json = File.ReadAllText(_equipmentFilePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    System.Diagnostics.Debug.WriteLine("LoadEquipment: файл пуст");
                    return new List<Equipment>();
                }

                System.Diagnostics.Debug.WriteLine($"LoadEquipment: размер файла: {json.Length} символов");
                System.Diagnostics.Debug.WriteLine($"LoadEquipment: содержимое файла (первые 500 символов): {json.Substring(0, Math.Min(500, json.Length))}");

                using var doc = JsonDocument.Parse(json);

                if (doc.RootElement.ValueKind != JsonValueKind.Array)
                {
                    System.Diagnostics.Debug.WriteLine("LoadEquipment: корневой элемент не является массивом");
                    return new List<Equipment>();
                }

                int arrayLength = 0;
                foreach (var _ in doc.RootElement.EnumerateArray())
                {
                    arrayLength++;
                }
                System.Diagnostics.Debug.WriteLine($"LoadEquipment: размер массива в JSON: {arrayLength} элементов");

                var restoredList = new List<Equipment>();
                int elementIndex = 0;

                foreach (var element in doc.RootElement.EnumerateArray())
                {
                    elementIndex++;
                    Equipment? equipment = null;

                    var elementJson = element.GetRawText();
                    System.Diagnostics.Debug.WriteLine($"LoadEquipment: обработка элемента {elementIndex}");
                    System.Diagnostics.Debug.WriteLine($"LoadEquipment: JSON элемента: {elementJson.Substring(0, Math.Min(200, elementJson.Length))}");

                    bool hasFormFactor = element.TryGetProperty("formFactor", out _);
                    bool hasPowerSupply = element.TryGetProperty("powerSupply", out _);
                    bool hasBatteryLife = element.TryGetProperty("batteryLife", out _);
                    bool hasWeight = element.TryGetProperty("weight", out _);
                    bool hasGraphicsCard = element.TryGetProperty("graphicsCard", out _);
                    bool hasVRAM = element.TryGetProperty("vram", out _);
                    bool hasRackUnit = element.TryGetProperty("rackUnit", out _);
                    bool hasServerType = element.TryGetProperty("serverType", out _);
                    bool hasPortCount = element.TryGetProperty("portCount", out _);
                    bool hasConnectionType = element.TryGetProperty("connectionType", out _);

                    bool hasPrinterType = element.TryGetProperty("printerType", out _);
                    bool hasMaxPaperSize = element.TryGetProperty("maxPaperSize", out _);
                    bool hasResolution = element.TryGetProperty("resolution", out _);
                    bool hasScanType = element.TryGetProperty("scanType", out _);
                    bool hasCopySpeed = element.TryGetProperty("copySpeed", out _);
                    bool hasTransmissionSpeed = element.TryGetProperty("transmissionSpeed", out _);

                    bool hasScreenSize = element.TryGetProperty("screenSize", out _);
                    bool hasBatteryCapacity = element.TryGetProperty("batteryCapacity", out _);
                    bool hasKeyboard = element.TryGetProperty("hasKeyboard", out _);
                    bool hasStylus = element.TryGetProperty("hasStylus", out _);
                    bool hasCameraMP = element.TryGetProperty("cameraMP", out _);
                    bool hasStorageCapacity = element.TryGetProperty("storageCapacity", out _);

                    bool hasProcessor = element.TryGetProperty("processor", out _);
                    bool hasRam = element.TryGetProperty("ram", out _);
                    bool hasEquipmentCategory = element.TryGetProperty("equipmentCategory", out _);
                    bool hasPrintSpeed = element.TryGetProperty("printSpeed", out _);

                    try
                    {

                        if (hasFormFactor || hasPowerSupply)
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - DesktopEquipment");
                            equipment = JsonSerializer.Deserialize<DesktopEquipment>(element.GetRawText(), GetJsonOptions());
                        }
                        else if ((hasBatteryLife || hasWeight) && hasProcessor)
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - LaptopEquipment");
                            equipment = JsonSerializer.Deserialize<LaptopEquipment>(element.GetRawText(), GetJsonOptions());
                        }
                        else if (hasGraphicsCard || hasVRAM)
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - WorkstationEquipment");
                            equipment = JsonSerializer.Deserialize<WorkstationEquipment>(element.GetRawText(), GetJsonOptions());
                        }
                        else if (hasRackUnit || hasServerType)
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - ServerEquipment");
                            equipment = JsonSerializer.Deserialize<ServerEquipment>(element.GetRawText(), GetJsonOptions());
                        }
                        else if (hasPortCount || hasConnectionType)
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - NetworkEquipment");
                            equipment = JsonSerializer.Deserialize<NetworkEquipment>(element.GetRawText(), GetJsonOptions());
                        }

                        else if (hasPrinterType)
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - PrinterEquipment");
                            equipment = JsonSerializer.Deserialize<PrinterEquipment>(element.GetRawText(), GetJsonOptions());
                        }
                        else if (hasResolution || hasScanType)
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - ScannerEquipment");
                            equipment = JsonSerializer.Deserialize<ScannerEquipment>(element.GetRawText(), GetJsonOptions());
                        }
                        else if (hasCopySpeed)
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - CopierEquipment");
                            equipment = JsonSerializer.Deserialize<CopierEquipment>(element.GetRawText(), GetJsonOptions());
                        }
                        else if (hasTransmissionSpeed)
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - FaxEquipment");
                            equipment = JsonSerializer.Deserialize<FaxEquipment>(element.GetRawText(), GetJsonOptions());
                        }

                        else if (hasKeyboard || hasStylus)
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - TabletEquipment");
                            equipment = JsonSerializer.Deserialize<TabletEquipment>(element.GetRawText(), GetJsonOptions());
                        }
                        else if (hasCameraMP || hasStorageCapacity)
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - SmartphoneEquipment");
                            equipment = JsonSerializer.Deserialize<SmartphoneEquipment>(element.GetRawText(), GetJsonOptions());
                        }

                        else if (hasProcessor || hasRam)
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - ComputerEquipment");
                            equipment = JsonSerializer.Deserialize<ComputerEquipment>(element.GetRawText(), GetJsonOptions());
                        }
                        else if (hasEquipmentCategory || hasPrintSpeed)
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - OfficeEquipment");
                            equipment = JsonSerializer.Deserialize<OfficeEquipment>(element.GetRawText(), GetJsonOptions());
                        }
                        else if (hasScreenSize || hasBatteryCapacity)
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - MobileEquipment");
                            equipment = JsonSerializer.Deserialize<MobileEquipment>(element.GetRawText(), GetJsonOptions());
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - не удалось определить тип, пробуем ComputerEquipment");
                            equipment = JsonSerializer.Deserialize<ComputerEquipment>(element.GetRawText(), GetJsonOptions());
                        }

                        if (equipment != null)
                        {
                            restoredList.Add(equipment);
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} успешно загружен - {equipment.GetEquipmentType()}: {equipment.Name}");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"LoadEquipment: элемент {elementIndex} - десериализация вернула null");
                        }
                    }
                    catch (Exception deserializeEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"LoadEquipment: ошибка десериализации элемента {elementIndex}: {deserializeEx.Message}");
                        System.Diagnostics.Debug.WriteLine($"LoadEquipment: стек вызовов: {deserializeEx.StackTrace}");
                        System.Diagnostics.Debug.WriteLine($"LoadEquipment: содержимое элемента: {element.GetRawText()}");
                    }
                }

                System.Diagnostics.Debug.WriteLine($"LoadEquipment: всего обработано элементов в массиве: {elementIndex}, успешно загружено: {restoredList.Count}");

                foreach (var eq in restoredList)
                {
                    System.Diagnostics.Debug.WriteLine($"  - {eq.GetEquipmentType()}: {eq.Name} (ID: {eq.Id})");
                }

                return restoredList;
            }
            catch (JsonException jsonEx)
            {
                System.Diagnostics.Debug.WriteLine($"LoadEquipment JSON error: {jsonEx.Message}");

                return new List<Equipment>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadEquipment exception: {ex}");
                throw new Exception($"Ошибка при загрузке данных: {ex.Message}", ex);
            }
        }

        public void SaveUsers(List<User> users)
        {
            try
            {
                var options = GetJsonOptions();
                var json = JsonSerializer.Serialize(users, options);
                File.WriteAllText(_usersFilePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при сохранении пользователей: {ex.Message}", ex);
            }
        }

        public List<User> LoadUsers()
        {
            try
            {
                if (!File.Exists(_usersFilePath))
                {

                    var defaultUsers = new List<User>
                    {
                        new User("admin", "admin", UserRole.Administrator),
                        new User("user", "user", UserRole.User)
                    };
                    SaveUsers(defaultUsers);
                    return defaultUsers;
                }

                var json = File.ReadAllText(_usersFilePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<User>();
                }

                var options = GetJsonOptions();
                return JsonSerializer.Deserialize<List<User>>(json, options) ?? new List<User>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при загрузке пользователей: {ex.Message}", ex);
            }
        }
    }
}
