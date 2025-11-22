namespace equipment_accounting_system.Models
{

    public class WorkstationEquipment : ComputerEquipment
    {
        public string GraphicsCard { get; set; } = string.Empty;
        public int VRAM { get; set; }
        public bool HasECC { get; set; }
        public int MaxRAM { get; set; }

        public override string GetEquipmentType()
        {
            return "Рабочая станция";
        }

        public override string GetSpecificCharacteristics()
        {
            return base.GetSpecificCharacteristics() + "\n" +
                   $"Видеокарта: {GraphicsCard}\n" +
                   $"Видеопамять: {VRAM} ГБ\n" +
                   $"ECC память: {(HasECC ? "Да" : "Нет")}\n" +
                   $"Макс. ОЗУ: {MaxRAM} ГБ";
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + "\n" +
                   $"Видеокарта: {GraphicsCard}\n" +
                   $"Видеопамять: {VRAM} ГБ\n" +
                   $"ECC память: {(HasECC ? "Да" : "Нет")}\n" +
                   $"Макс. ОЗУ: {MaxRAM} ГБ";
        }
    }
}
