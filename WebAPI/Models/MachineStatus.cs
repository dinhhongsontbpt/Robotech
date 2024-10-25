namespace WebAPI.Models
{
    public class MachineStatus
    {
        public bool Ready { get; set; }
        public bool HomeMachine { get; set; }
        public bool AutoMode { get; set; }
        public bool ManualMode { get; set; }
        public bool AutoRunning { get; set; }
        public bool Pause { get; set; }
        public bool OriginRunning { get; set; }
        public bool InitialRunning { get; set; }
        public bool HeavyAlarm { get; set; }
        public bool LightAlarm { get; set; }
        public bool DoorOpen { get; set; }
    }
}
